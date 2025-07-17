using Microsoft.ML;
using System;
using System.Collections.Generic;

namespace TextTabulator.Adapters.MLDotNet
{
    /// <summary>
    /// Class that implements the ITabulatorAdapter interface in order to adapt data contained
    /// in an IDataView to be consumed by Tabulator.Tabulate.
    /// </summary>
    public class DataViewTabulatorAdapter : ITabulatorAdapter
    {
        private readonly IDataView _dataView;
        private readonly DataViewTabulatorAdapterOptions _options;

        /// <summary>
        /// Creates an object of type DataViewTabulatorAdapter.
        /// </summary>
        /// <param name="dataView">IDataView object containing data to be consumed by Tabulator.Tabulate.</param>
        /// <param name="options">Options for the adapter.</param>
        public DataViewTabulatorAdapter(IDataView dataView, DataViewTabulatorAdapterOptions? options = null)
        {
            _dataView = dataView;
            _options = options ?? new DataViewTabulatorAdapterOptions();
        }

        /// <summary>
        /// Called to return the header strings, if any, of the data. If the data does not contain headers, then null should be returned.
        /// </summary>
        /// <returns>An enumerable containing the header strings for the table, or null if the data contains no header strings.</returns>
        public IEnumerable<string>? GetHeaderStrings()
        {
            foreach (var column in _dataView.Schema)
            {
                yield return _options.ColumnNameTransform.Apply(column.Name);
            }
        }

        /// <summary>
        /// Called to return the row values. The outer enumeration is the rows, while the inner enumeration contains the values in each row.
        /// Can be an empty enumeration if the data contains no rows.
        /// </summary>
        /// <returns>An enumerable containing the rows and the values within each row.</returns>
        public IEnumerable<IEnumerable<string>> GetValueStrings()
        {
            var columns = new DataViewSchema.Column[_dataView.Schema.Count];
            var i = 0;

            foreach (var column in _dataView.Schema)
            {
                columns[i++] = column;
            }

            var cursor = _dataView.GetRowCursor(columns);

            while (cursor.MoveNext())
            {
                var rowValues = new List<string>();
                foreach (var column in columns)
                {
                    var value = ColumnValueGetter(cursor, column);
                    rowValues.Add(value?.ToString() ?? string.Empty);
                }
                yield return rowValues;
            }
        }

        private static object? ColumnValueGetter(DataViewRowCursor cursor, DataViewSchema.Column column)
        {
            if (column.Type.RawType == typeof(bool))
            {
                bool value = false;
                cursor.GetGetter<bool>(column)(ref value);
                return value;
            }
            if (column.Type.RawType == typeof(char))
            {
                char value = default;
                cursor.GetGetter<char>(column)(ref value);
                return value;
            }
            if (column.Type.RawType == typeof(string))
            {
                var value = string.Empty;
                cursor.GetGetter<string>(column)(ref value);
                return value;
            }
            if (column.Type.RawType == typeof(ReadOnlyMemory<char>))
            {
                var value = new ReadOnlyMemory<char>();
                cursor.GetGetter<ReadOnlyMemory<char>>(column)(ref value);
                return value;
            }
            if (column.Type.RawType == typeof(byte))
            {
                byte value = 0;
                cursor.GetGetter<byte>(column)(ref value);
                return value;
            }
            if (column.Type.RawType == typeof(sbyte))
            {
                sbyte value = 0;
                cursor.GetGetter<sbyte>(column)(ref value);
                return value;
            }
            if (column.Type.RawType == typeof(short))
            {
                short value = 0;
                cursor.GetGetter<short>(column)(ref value);
                return value;
            }
            if (column.Type.RawType == typeof(ushort))
            {
                ushort value = 0;
                cursor.GetGetter<ushort>(column)(ref value);
                return value;
            }
            if (column.Type.RawType == typeof(int))
            {
                int value = 0;
                cursor.GetGetter<int>(column)(ref value);
                return value;
            }
            if (column.Type.RawType == typeof(uint))
            {
                uint value = 0;
                cursor.GetGetter<uint>(column)(ref value);
                return value;
            }
            if (column.Type.RawType == typeof(long))
            {
                long value = 0;
                cursor.GetGetter<long>(column)(ref value);
                return value;
            }
            if (column.Type.RawType == typeof(ulong))
            {
                ulong value = 0;
                cursor.GetGetter<ulong>(column)(ref value);
                return value;
            }
            if (column.Type.RawType == typeof(float))
            {
                float value = 0;
                cursor.GetGetter<float>(column)(ref value);
                return value;
            }
            if (column.Type.RawType == typeof(double))
            {
                double value = 0;
                cursor.GetGetter<double>(column)(ref value);
                return value;
            }
            if (column.Type.RawType == typeof(decimal))
            {
                decimal value = 0;
                cursor.GetGetter<decimal>(column)(ref value);
                return value;
            }
            if (column.Type.RawType == typeof(System.DateTime))
            {
                System.DateTime value = default;
                cursor.GetGetter<System.DateTime>(column)(ref value);
                return value;
            }
            if (column.Type.RawType == typeof(System.DateTimeOffset))
            {
                System.DateTimeOffset value = default;
                cursor.GetGetter<System.DateTimeOffset>(column)(ref value);
                return value;
            }
            if (column.Type.RawType == typeof(System.TimeSpan))
            {
                System.TimeSpan value = default;
                cursor.GetGetter<System.TimeSpan>(column)(ref value);
                return value;
            }
            if (column.Type.RawType == typeof(System.Guid))
            {
                System.Guid value = default;
                cursor.GetGetter<System.Guid>(column)(ref value);
                return value;
            }

            // If type is not handled, return null
            return null;
        }
    }
}
