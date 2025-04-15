using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace TextTabulator.Adapters.Json
{
    /// <summary>
    /// Public interface for IJsonTabulatorAdapter.
    /// </summary>
    public interface IJsonTabulatorAdapter : ITabulatorAdapter
    {
    }

    /// <summary>
    /// The adapter class that accepts JSON data and presents the data that it reads in a format that TextTabulator.Tabulate can consume.
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
    public class JsonTabulatorAdapter : IJsonTabulatorAdapter
    {
        private class Header
        {
            public string TransformedName { get; }

            public int Index { get; }

            public Header(string transformedName, int index)
            {
                TransformedName = transformedName;
                Index = index;
            }   
        }

        private const int BufferSize = 4096;

        private readonly Func<Stream> _jsonStreamProvider;
        private readonly JsonTabulatorAdapterOptions _options;
        private readonly Dictionary<string, Header> _headers = new Dictionary<string, Header>();

        /// <summary>
        /// Creates an object of type JsonTabulatorAdapter.
        /// In order for the adpater to function correctly, make sure the JSON data only contains an array of homogeneous JSON objects.
        /// </summary>
        /// <param name="jsonStreamProvider">Function that provides a stream containing an array containing homogeneous objects of UTF-8 encoded JSON data.</param>
        /// <param name="options">Options for the adapter.</param>
        public JsonTabulatorAdapter(Func<Stream> jsonStreamProvider, JsonTabulatorAdapterOptions? options = null)
        {
            _jsonStreamProvider = jsonStreamProvider;
            _options = options ?? new JsonTabulatorAdapterOptions();
        }

        /// <summary>
        /// Creates an object of type JsonTabulatorAdapter.
        /// In order for the adpater to function correctly, make sure the JSON data only contains an array of homogeneous JSON objects.
        /// </summary>
        /// <param name="jsonStream">Stream containing UTF-8 encoded JSON data.</param>
        /// <param name="options">Options for the adapter.</param>
        public JsonTabulatorAdapter(Stream jsonStream, JsonTabulatorAdapterOptions? options = null)
            : this(() => jsonStream, options)
        {
        }

        /// <summary>
        /// Creates an object of type JsonTabulatorAdapter.
        /// In order for the adpater to function correctly, make sure the JSON data only contains an array of homogeneous JSON objects.
        /// </summary>
        /// <param name="json">String containing raw JSON data.</param>
        /// <param name="options">Options for the adapter.</param>
        public JsonTabulatorAdapter(string json, JsonTabulatorAdapterOptions? options = null)
            : this(() => new MemoryStream(UTF8Encoding.UTF8.GetBytes(json)), options)
        {
        }

        /// <summary>
        /// Called to return the header strings, if any, of the data. If the data does not contain headers, then null should be returned.
        /// </summary>
        /// <returns>An enumerable containing the header strings for the table, or null if the data contains no header strings.</returns>
        public IEnumerable<string>? GetHeaderStrings()
        {
            var buffer = new byte[BufferSize];

            var stream = _jsonStreamProvider.Invoke();

            var bytesRead = stream.Read(buffer, 0, buffer.Length);

            if (bytesRead <= 0)
            {
                return null;
            }

            var jsonReader = new Utf8JsonReader(buffer, false, new JsonReaderState(_options.JsonReaderOptions));

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

            _headers.Clear();

            var transformedHeaders = new List<string>();

            // Since the ITabulatorAdapter interface reads the data in two steps - headers then values - state
            // needs to be maintained between the calls to GetHeaderStrings() and GetValueStrings(). Because
            // Utf8JsonReader is a ref struct, it can only exist on the stack. It is also only forward reading.
            // All this means that this loop only reads the properties of the first JSON object, which it
            // assumes are the headers. These are then returned as the headers. The reading of the actual
            // property values is done in GetValueStrings(). An entirely new Utf8JsonReader will be created,
            // and the first JSON object will be read again - this time recording the values.
            while (loop)
            {
                if (!jsonReader.Read())
                {
                    GetMoreBytesFromStream(stream, ref buffer, ref jsonReader);

                    if (!jsonReader.Read())
                    {
                        throw new InvalidOperationException($"Failed to read additional JSON after refreshing buffer.");
                    }
                }

                if (jsonReader.TokenType == JsonTokenType.PropertyName && jsonReader.CurrentDepth == targetDepth)
                {
                    var rawHeader = jsonReader.GetString() ?? string.Empty;
                    var transformed = _options.JsonPropertyNameTransform.Apply(rawHeader);
                    transformedHeaders.Add(transformed);
                    _headers.Add(rawHeader, new Header(transformed, transformedHeaders.Count - 1));
                }
                else if (jsonReader.TokenType == JsonTokenType.EndObject)
                {
                    loop = jsonReader.CurrentDepth != targetDepth - 1;
                }
                else if (jsonReader.TokenType == JsonTokenType.EndArray)
                {
                    loop = jsonReader.CurrentDepth != startDepth;
                }
            }

            return transformedHeaders;
        }

        /// <summary>
        /// Called to return the row values. The outer enumeration is the rows, while the inner enumeration contains the values in each row.
        /// Can be an empty enumeration if the data contains no rows.
        /// </summary>
        /// <returns>An enumerable containing the rows and the values within each row.</returns>
        public IEnumerable<IEnumerable<string>> GetValueStrings()
        {
            var buffer = new byte[BufferSize];

            var stream = _jsonStreamProvider.Invoke();

            stream.Seek(0, SeekOrigin.Begin);
            var bytesRead = stream.Read(buffer, 0, buffer.Length);

            if (bytesRead <= 0)
            {
                return Array.Empty<string[]>();
            }

            var jsonReader = new Utf8JsonReader(buffer, false, new JsonReaderState(_options.JsonReaderOptions));

            var loop = true;
            var startDepth = jsonReader.CurrentDepth;
            var depthDelta = 2;
            var targetDepth = startDepth + depthDelta;
            var index = 0;
            var rowValues = new List<string[]>();
            var values = new string[_headers.Count];

            // This loop will read the values of the properties in JSON objects. It uses the property names as
            // headers, that were read in GetHeaderStrings().
            while (loop)
            {
                if (!jsonReader.Read())
                {
                    GetMoreBytesFromStream(stream, ref buffer, ref jsonReader);

                    if (!jsonReader.Read())
                    {
                        throw new InvalidOperationException($"Failed to read additional JSON after refreshing buffer.");
                    }
                }

                if (jsonReader.TokenType == JsonTokenType.PropertyName && jsonReader.CurrentDepth == targetDepth)
                {
                    var rawHeader = jsonReader.GetString() ?? string.Empty;

                    if (string.IsNullOrEmpty(rawHeader))
                    {
                        throw new InvalidOperationException("Empty header value.");
                    }

                    if (!_headers.TryGetValue(rawHeader, out Header header))
                    {
                        throw new InvalidOperationException($"Unknown header '{rawHeader}' encountered while parsing values.");
                    }

                    index = header.Index;
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
                        Array.Fill(values, "");
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
