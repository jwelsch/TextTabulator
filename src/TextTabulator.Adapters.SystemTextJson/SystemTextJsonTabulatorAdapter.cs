using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Text.Json;

namespace TextTabulator.Adapters.SystemTextJson
{
    /// <summary>
    /// Public interface for ISystemTextJsonTabulatorAdapter.
    /// </summary>
    public interface ISystemTextJsonTabulatorAdapter : ITabulatorAdapter
    {
    }

    /// <summary>
    /// Class that implements the ITabulatorAdapter interface in order to adapt JSON data ready by
    /// System.Text.Json to be consumed by Tabulator.Tabulate.
    /// 
    /// The data should be in the following format:
    /// 
    /// [
    ///   {
    ///     "field1": value1A,
    ///     "field2": "value2A"
    ///   },
    ///   {
    ///     "field1": value1B,
    ///     "field2": "value2B"
    ///   },
    ///   {
    ///     "field1": value1C,
    ///     "field2": "value2C"
    ///   }
    /// ]
    /// </summary>
    public class SystemTextJsonTabulatorAdapter : ISystemTextJsonTabulatorAdapter
    {
        private readonly Func<Stream> _jsonStreamProvider;
        private readonly JsonReaderOptions _options;
        private readonly List<string> _headers = new List<string>();

        /// <summary>
        /// Creates an object of type SystemTextJsonTabulatorAdapter.
        /// In order for the adpater to function correctly, make sure the JSON data only contains an array of homogeneous JSON objects.
        /// </summary>
        /// <param name="jsonStreamProvider">Function that provides a stream containing an array containing homogeneous objects of UTF-8 encoded JSON data.</param>
        /// <param name="options">Options that define customized behavior of the Utf8JsonReader that differs from the JSON RFC (for example, how to handle comments or maximum depth allowed when reading). By default, the Utf8JsonReader follows the JSON RFC strictly; comments within the JSON are invalid, and the maximum depth is 64.</param>
        public SystemTextJsonTabulatorAdapter(Func<Stream> jsonStreamProvider, JsonReaderOptions options = default)
        {
            _jsonStreamProvider = jsonStreamProvider;
            _options = options;
        }

        /// <summary>
        /// Creates an object of type SystemTextJsonTabulatorAdapter.
        /// In order for the adpater to function correctly, make sure the JSON data only contains an array of homogeneous JSON objects.
        /// </summary>
        /// <param name="jsonStream">Stream containing UTF-8 encoded JSON data.</param>
        /// <param name="options">Options that define customized behavior of the Utf8JsonReader that differs from the JSON RFC (for example, how to handle comments or maximum depth allowed when reading). By default, the Utf8JsonReader follows the JSON RFC strictly; comments within the JSON are invalid, and the maximum depth is 64.</param>
        public SystemTextJsonTabulatorAdapter(Stream jsonStream, JsonReaderOptions options = default)
        {
            _jsonStreamProvider = () => jsonStream;
            _options = options;
        }

        /// <summary>
        /// Called to return the header strings, if any, of the data. If the data does not contain headers, then null should be returned.
        /// </summary>
        /// <returns>An enumerable containing the header strings for the table, or null if the data contains no header strings.</returns>
        public IEnumerable<string>? GetHeaderStrings()
        {
            var buffer = new byte[4096];

            var stream = _jsonStreamProvider.Invoke();

            var bytesRead = stream.Read(buffer, 0, buffer.Length);

            if (bytesRead <= 0)
            {
                return null;
            }

            var jsonReader = new Utf8JsonReader(buffer, _options);

            // Need to read the first JSON object in order to get the headers.

            if (!jsonReader.Read())
            {
                return null;
            }

            // Expecting a start array token, since the JSON data should be an array.
            if (jsonReader.TokenType != JsonTokenType.StartArray)
            {
                throw new InvalidOperationException($"Expected the first token to be '{JsonTokenType.StartArray}'.");
            }

            var loop = true;
            var startDepth = jsonReader.CurrentDepth;
            var depthDelta = 2;
            var targetDepth = startDepth + depthDelta;

            while (loop && jsonReader.Read())
            {
                if (jsonReader.TokenType == JsonTokenType.PropertyName && jsonReader.CurrentDepth == targetDepth)
                {
                    var header = jsonReader.GetString() ?? string.Empty;
                    _headers.Add(header);
                }
                else if (jsonReader.TokenType == JsonTokenType.EndObject && jsonReader.CurrentDepth == targetDepth)
                {
                    loop = jsonReader.CurrentDepth != startDepth;
                }
                else if (jsonReader.TokenType == JsonTokenType.EndArray)
                {
                    loop = jsonReader.CurrentDepth != startDepth;
                }
            }

            return _headers;
        }

        /// <summary>
        /// Called to return the row values. The outer enumeration is the rows, while the inner enumeration contains the values in each row.
        /// Can be an empty enumeration if the data contains no rows.
        /// </summary>
        /// <returns>An enumerable containing the rows and the values within each row.</returns>
        public IEnumerable<IEnumerable<string>> GetValueStrings()
        {
            var buffer = new byte[4096];

            var stream = _jsonStreamProvider.Invoke();

            stream.Seek(0, SeekOrigin.Begin);
            var bytesRead = stream.Read(buffer, 0, buffer.Length);

            if (bytesRead <= 0)
            {
                return Array.Empty<string[]>();
            }

            var jsonReader = new Utf8JsonReader(buffer, _options);

            var loop = true;
            var startDepth = jsonReader.CurrentDepth;
            var depthDelta = 2;
            var targetDepth = startDepth + depthDelta;
            var index = 0;
            var rowValues = new List<string[]>();
            var values = new string[_headers.Count];

            while (loop && jsonReader.Read())
            {
                if (jsonReader.TokenType == JsonTokenType.PropertyName && jsonReader.CurrentDepth == targetDepth)
                {
                    var header = jsonReader.GetString() ?? string.Empty;

                    if (string.IsNullOrEmpty(header))
                    {
                        throw new InvalidOperationException("Empty header value.");
                    }

                    index = _headers.IndexOf(header);

                    if (index == -1)
                    {
                        throw new InvalidOperationException($"Unknown header '{header}' encountered while parsing values.");
                    }
                }
                else if (jsonReader.TokenType == JsonTokenType.String && jsonReader.CurrentDepth == targetDepth)
                {
                    if (values == null)
                    {
                        throw new InvalidOperationException("Value array is null.");
                    }

                    var value = jsonReader.GetString() ?? string.Empty;
                    values[index] = value;
                }
                else if (jsonReader.TokenType == JsonTokenType.Number && jsonReader.CurrentDepth == targetDepth)
                {
                    if (values == null)
                    {
                        throw new InvalidOperationException("Value array is null.");
                    }

                    var value = jsonReader.GetDecimal();
                    values[index] = value.ToString();
                }
                else if ((jsonReader.TokenType == JsonTokenType.True || jsonReader.TokenType == JsonTokenType.False)
                     && jsonReader.CurrentDepth == targetDepth)
                {
                    if (values == null)
                    {
                        throw new InvalidOperationException("Value array is null.");
                    }

                    var value = jsonReader.GetBoolean();
                    values[index] = value.ToString();
                }
                else if (jsonReader.TokenType == JsonTokenType.Null && jsonReader.CurrentDepth == targetDepth)
                {
                    if (values == null)
                    {
                        throw new InvalidOperationException("Value array is null.");
                    }

                    values[index] = string.Empty;
                }
                else if (jsonReader.TokenType == JsonTokenType.StartObject)
                {
                    
                    if (jsonReader.CurrentDepth == targetDepth - 1)
                    {
                        values = new string[_headers.Count];
                    }
                    else if (jsonReader.CurrentDepth == targetDepth)
                    {
                        values[index] = "<JSON Object>";
                    }
                }
                else if (jsonReader.TokenType == JsonTokenType.EndObject && jsonReader.CurrentDepth == targetDepth - 1)
                {
                    rowValues.Add(values ?? throw new InvalidOperationException("Value array is null."));
                }
                else if (jsonReader.TokenType == JsonTokenType.StartArray && jsonReader.CurrentDepth == targetDepth)
                {
                    if (values == null)
                    {
                        throw new InvalidOperationException("Value array is null.");
                    }

                    values[index] = "<JSON Array>";
                }
                else if (jsonReader.TokenType == JsonTokenType.EndArray)
                {
                    loop = jsonReader.CurrentDepth != startDepth;
                }
            }

            return rowValues;
        }

        private static void GetMoreBytesFromStream(Stream stream, ref byte[] buffer, ref Utf8JsonReader reader)
        {
            int bytesRead;

            if (reader.BytesConsumed < buffer.Length)
            {
                ReadOnlySpan<byte> leftover = buffer.AsSpan((int)reader.BytesConsumed);

                if (leftover.Length == buffer.Length)
                {
                    Array.Resize(ref buffer, buffer.Length * 2);
                }

                leftover.CopyTo(buffer);
                bytesRead = stream.Read(buffer.AsSpan(leftover.Length));
            }
            else
            {
                bytesRead = stream.Read(buffer);
            }

            reader = new Utf8JsonReader(buffer, isFinalBlock: bytesRead == 0, reader.CurrentState);
        }
    }
}
