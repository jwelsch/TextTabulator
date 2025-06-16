using System;
using System.Collections.Generic;
using System.Linq;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;

namespace TextTabulator.Adapters.YamlDotNet
{
    /// <summary>
    /// Public interface for IYamlDotNetTabulatorAdapter.
    /// </summary>
    public interface IYamlDotNetTabulatorAdapter : ITabulatorAdapter
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
        private enum NodeStatus
        {
            None,
            Start,
            End
        }

        private enum KeyValueStatus
        {
            None,
            Key,
            Value
        }

        private readonly Parser _parser;
        private readonly YamlDotNetTabulatorAdapterOptions _options;
        private readonly Dictionary<string, TableHeader> _headers = new Dictionary<string, TableHeader>();
        private readonly List<string> _firstRow = new List<string>();
        private readonly IValueNormalizer _valueNormalizer = new ValueNormalizer();
        private readonly int _targetDepth = 4; // Target depth for the key/value pairs that will be included in the table.

        private NodeStatus _streamStatus = NodeStatus.None;
        private NodeStatus _documentStatus = NodeStatus.None;
        private NodeStatus _sequenceStatus = NodeStatus.None;
        private NodeStatus _mappingStatus = NodeStatus.None;
        private int _row = 0;
        private int _column = 0;
        private int _currentDepth = 0;

        /// <summary>
        /// Creates an object of type YamlDotNetTabulatorAdapter.
        /// </summary>
        /// <param name="parser">A YamlDotNet.Core.Parser object with the YAML data to process.</param>
        /// <param name="options">Options for the adapter.</param>
        public YamlDotNetTabulatorAdapter(Parser parser, YamlDotNetTabulatorAdapterOptions? options = null)
        {
            _parser = parser;
            _options = options ?? new YamlDotNetTabulatorAdapterOptions();
        }

        /// <summary>
        /// Called to return the header strings, if any, of the data. If the data does not contain headers, then null should be returned.
        /// </summary>
        /// <returns>An enumerable containing the header strings for the table, or null if the data contains no header strings.</returns>
        public IEnumerable<string>? GetHeaderStrings()
        {
            _headers.Clear();
            _firstRow.Clear();
            _row = 0;
            _column = 0;
            _currentDepth = 0;

            ParseLoop(
                null,
                m => false,
                k => _headers.Add(k, new TableHeader(_options.NodeNameTransform.Apply(k), _column)),
                v => _firstRow.Add(v)
            );

            return _headers.Select(i => i.Value.TransformedName).ToArray();
        }

        /// <summary>
        /// Called to return the row values. The outer enumeration is the rows, while the inner enumeration contains the values in each row.
        /// Can be an empty enumeration if the data contains no rows.
        /// </summary>
        /// <returns>An enumerable containing the rows and the values within each row.</returns>
        public IEnumerable<IEnumerable<string>> GetValueStrings()
        {
            var rowValues = new List<string[]>(new string[][] { _firstRow.ToArray() });
            string[]? values = null;
            var index = 0;

            ParseLoop(
                e =>
                {
                    values = new string[_headers.Count];
                    Array.Fill(values, string.Empty);
                },
                e =>
                {
                    if (values == null)
                    {
                        throw new InvalidOperationException($"Values array was null while trying to store values - row: '{_row}'.");
                    }

                    rowValues.Add(values);

                    return true;
                },
                k =>
                {
                    if (!_headers.TryGetValue(k, out var tableHeader))
                    {
                        throw new InvalidOperationException($"Unknown header '{k}' encountered while parsing values - row: '{_row}', column: '{_column}'.");
                    }

                    index = tableHeader.Index;
                },
                v =>
                {
                    if (values == null)
                    {
                        throw new InvalidOperationException($"Values array was null while trying to store value - row: '{_row}', column: '{_column}'.");
                    }

                    values[index] = v;
                }
            );

            return rowValues;
        }

        private void ParseLoop(Action<MappingStart>? mappingStartAction, Func<MappingEnd, bool>? mappingEndAction, Action<string>? keyAction, Action<string>? valueAction)
        {
            var keyValueStatus = KeyValueStatus.None;

            while (_parser.MoveNext())
            {
                var parsingEvent = _parser.Current;

                //System.Diagnostics.Trace.WriteLine($"Parsing event: {parsingEvent?.ToString()}");

                if (parsingEvent == null)
                {
                    continue;
                }

                _currentDepth += parsingEvent.NestingIncrease;

                if (parsingEvent is StreamStart Start)
                {
                    _streamStatus = NodeStatus.Start;
                }
                else if (parsingEvent is DocumentStart documentStart)
                {
                    if (_streamStatus != NodeStatus.Start)
                    {
                        throw new InvalidOperationException($"Invalid stream status - expected '{NodeStatus.Start}', but found '{_streamStatus}'.");
                    }

                    _documentStatus = NodeStatus.Start;
                }
                else if (parsingEvent is SequenceStart sequenceStart)
                {
                    if (_currentDepth == _targetDepth - 1)
                    {
                        if (_documentStatus != NodeStatus.Start)
                        {
                            throw new InvalidOperationException($"Invalid document status - expected '{NodeStatus.Start}', but found '{_documentStatus}'.");
                        }

                        _sequenceStatus = NodeStatus.Start;
                    }
                    else if (_currentDepth == _targetDepth + 1)
                    {
                        keyValueStatus = KeyValueStatus.Value;
                        valueAction?.Invoke("<YAML Array>");
                        _column++;
                    }
                }
                else if (parsingEvent is MappingStart mappingStart)
                {
                    if (_currentDepth == _targetDepth)
                    {
                        if (_sequenceStatus != NodeStatus.Start)
                        {
                            throw new InvalidOperationException($"Invalid sequence status - expected '{NodeStatus.Start}', but found '{_sequenceStatus}' at row '{_row}'.");
                        }

                        _mappingStatus = NodeStatus.Start;

                        mappingStartAction?.Invoke(mappingStart);
                    }
                    else if (_currentDepth == _targetDepth + 1)
                    {
                        keyValueStatus = KeyValueStatus.Value;
                        valueAction?.Invoke("<YAML Object>");
                        _column++;
                    }
                }
                else if (parsingEvent is StreamEnd streamEnd)
                {
                    if (_streamStatus != NodeStatus.Start)
                    {
                        throw new InvalidOperationException($"Invalid stream status - expected '{NodeStatus.Start}', but found '{_streamStatus}'.");
                    }

                    _streamStatus = NodeStatus.End;
                }
                else if (parsingEvent is DocumentEnd documentEnd)
                {
                    if (_documentStatus != NodeStatus.Start)
                    {
                        throw new InvalidOperationException($"Invalid document status - expected '{NodeStatus.Start}', but found '{_documentStatus}'.");
                    }

                    _documentStatus = NodeStatus.End;
                }
                else if (parsingEvent is SequenceEnd sequenceEnd)
                {
                    if (_currentDepth == _targetDepth - 1)
                    {
                        if (_sequenceStatus != NodeStatus.Start)
                        {
                            throw new InvalidOperationException($"Invalid sequence status - expected '{NodeStatus.Start}', but found '{_sequenceStatus}'.");
                        }

                        _sequenceStatus = NodeStatus.End;
                    }
                }
                else if (parsingEvent is MappingEnd mappingEnd)
                {
                    if (_currentDepth == _targetDepth - 1)
                    {
                        if (_mappingStatus != NodeStatus.Start)
                        {
                            throw new InvalidOperationException($"Invalid mapping status - expected '{NodeStatus.Start}', but found '{_mappingStatus}' at row '{_row}'.");
                        }

                        _mappingStatus = NodeStatus.End;
                        var loop = mappingEndAction?.Invoke(mappingEnd) ?? true;

                        _row++;

                        if (!loop)
                        {
                            break;
                        }
                    }
                }
                else if (parsingEvent is Scalar scalar
                    && _currentDepth == _targetDepth)
                {
                    if (_mappingStatus != NodeStatus.Start)
                    {
                        throw new InvalidOperationException($"Invalid mapping status - expected '{NodeStatus.Start}', but found '{_mappingStatus}' at row '{_row}' and col '{_column}'.");
                    }

                    if (keyValueStatus == KeyValueStatus.None || keyValueStatus == KeyValueStatus.Value)
                    {
                        keyValueStatus = KeyValueStatus.Key;

                        keyAction?.Invoke(scalar.Value);
                    }
                    else if (keyValueStatus == KeyValueStatus.Key)
                    {
                        keyValueStatus = KeyValueStatus.Value;

                        valueAction?.Invoke(_valueNormalizer.Normalize(scalar.Value));

                        _column++;
                    }
                    else
                    {
                        throw new InvalidOperationException($"Unknown {nameof(KeyValueStatus)} value '{keyValueStatus}'.");
                    }
                }

                // Ignore unknown event types.
            }
        }
    }
}
