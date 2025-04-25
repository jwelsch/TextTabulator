using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using YamlDotNet.Serialization;

namespace TextTabulator.Adapters.YamlDotNet
{
    /// <summary>
    /// Public interface for IYamlDotNetTabulatorAdapter.
    /// </summary>
    public interface IYamlDotNetTabulatorAdapter : ITabulatorAdapter, IDisposable
    {
    }

    /// <summary>
    /// The adapter class that accepts YAML data and presents the data that it reads in a format that TextTabulator.Tabulate can consume.
    /// 
    /// The data should be in the following format:
    /// 
    /// - field1: value1A
    ///   field2: value2A
    /// - field1: value1B
    ///   field2: value2B
    /// - field1: value1C
    ///   field2: value2C
    ///   
    /// </summary>
    public class YamlDotNetTabulatorAdapter : IYamlDotNetTabulatorAdapter
    {
        private readonly Deserializer _yamlDotNetDeserializer;
        private readonly TextReader _data;
        private readonly bool _shouldDispose;
        private readonly YamlDotNetTabulatorAdapterOptions _options;
        private readonly Dictionary<string, TableHeader> _headers = new();
        private readonly IValueNormalizer _valueNormalizer = new ValueNormalizer();

        private IList<object>? _deserialized;
        private bool _disposed;

        public YamlDotNetTabulatorAdapter(Deserializer yamlDotNetDeserializer, StringProvider dataProvider, YamlDotNetTabulatorAdapterOptions? options = null)
            : this(yamlDotNetDeserializer, new StringReader(dataProvider.Invoke()), options)
        {
            _shouldDispose = true;
        }

        public YamlDotNetTabulatorAdapter(Deserializer yamlDotNetDeserializer, TextReader dataProvider, YamlDotNetTabulatorAdapterOptions? options = null)
        {
            _yamlDotNetDeserializer = yamlDotNetDeserializer;
            _data = dataProvider;
            _options = options ?? new YamlDotNetTabulatorAdapterOptions();
        }

        public YamlDotNetTabulatorAdapter(Deserializer yamlDotNetDeserializer, string inputData, YamlDotNetTabulatorAdapterOptions? options = null)
            : this(yamlDotNetDeserializer, () => inputData, options)
        {
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose managed here.

                    if (_shouldDispose)
                    {
                        _data.Dispose();
                    }
                }

                // Dispose unmanaged here.

                _disposed = true;
            }
        }

        public IEnumerable<string>? GetHeaderStrings()
        {
            _headers.Clear();
            _deserialized?.Clear();

            var headers = new List<string>();

            _deserialized = (IList<object>?)_yamlDotNetDeserializer.Deserialize(_data);

            if (_deserialized == null)
            {
                return null;
            }

            if (_deserialized.Count < 1)
            {
                return Array.Empty<string>();
            }

            var items = (IDictionary<object, object>?)_deserialized[0];

            if (items == null)
            {
                return Array.Empty<string>();
            }

            var column = 0;

            foreach (var item in items)
            {
                _headers.Add(item.Key?.ToString() ?? string.Empty, new TableHeader(_options.NodeNameTransform.Apply(item.Value?.ToString() ?? string.Empty), column++));
            }

            return items?.Select(i => i.Key.ToString() ?? string.Empty);
        }

        public IEnumerable<IEnumerable<string>> GetValueStrings()
        {
            if (_deserialized == null || _deserialized.Count < 1)
            {
                return Array.Empty<IEnumerable<string>>();
            }

            var rowValues = new List<string[]>();

            for (var i = 0; i < _deserialized.Count; i++)
            {
                var row = (IDictionary<object, object>?)_deserialized[i] ?? throw new InvalidOperationException($"Deserialized row with index '{i}' was null.");

                if (row.Count > _headers.Count)
                {
                    throw new InvalidOperationException($"The number of values '{row.Count}' in row with index '{i}' is greater than the number of headers '{_headers.Count}'.");
                }

                var col = 0;
                var values = new string[_headers.Count];
                Array.Fill(values, "");

                foreach (var value in row)
                {
                    var header = value.Key.ToString() ?? throw new InvalidOperationException($"Deserialized row value with row index '{i}' and column index '{col}' was null.");

                    if (!_headers.TryGetValue(header, out var tableHeader))
                    {
                        throw new InvalidOperationException($"Unknown header '{header}' encountered while parsing values.");
                    }

                    var type = value.Value?.GetType();

                    if ((type?.IsClass ?? false) && type != typeof(string))
                    {
                        if (type == typeof(List<object>))
                        {
                            values[tableHeader.Index] = "<YAML Array>";
                        }
                        else if (type == typeof(Dictionary<object, object>))
                        {
                            values[tableHeader.Index] = "<YAML Object>";
                        }
                        else
                        {
                            values[tableHeader.Index] = $"<Unknown data type '{type}'>";
                        }
                    }
                    else
                    {
                        values[tableHeader.Index] = _valueNormalizer.Normalize($"{value.Value}");
                    }

                    col++;
                }

                rowValues.Add(values);
            }

            return rowValues;
        }
    }
}
