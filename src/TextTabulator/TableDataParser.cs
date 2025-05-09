using System;
using System.Collections.Generic;

namespace TextTabulator
{
    public interface ITableDataParser
    {
        ITableData Parse(IEnumerable<string>? headers, IEnumerable<IEnumerable<string>>? rows);
    }

    public class TableDataParser : ITableDataParser
    {
        private readonly ITabulatorOptions _options;

        public TableDataParser(ITabulatorOptions options)
        {
            _options = options;
        }

        public ITableData Parse(IEnumerable<string>? headers, IEnumerable<IEnumerable<string>>? rows)
        {
            var rowDataParser = new RowDataParser(_options);
            IRowData? tableHeaders = null;
            List<IRowData>? tableRows = null;
            var rowIndex = 0;
            var maxWidths = new List<int>();
            var columnCount = -1;

            if (headers != null)
            {
                tableHeaders = rowDataParser.Parse(rowIndex, headers, ref maxWidths);

                if (tableHeaders.Cells == null)
                {
                    throw new InvalidOperationException($"Null cell collection in header row.");
                }

                columnCount = tableHeaders.Cells == null || tableHeaders.Cells.Count == 0 ? -1 : tableHeaders.Cells.Count;

                rowIndex++;
            }

            if (rows != null)
            {
                tableRows = new List<IRowData>();

                var valueRowCount = 0;

                foreach (var row in rows)
                {
                    var tableRow = rowDataParser.Parse(rowIndex, row, ref maxWidths);

                    if (tableRow.Cells == null)
                    {
                        throw new InvalidOperationException($"Null cell collection in row '{rowIndex}'.");
                    }

                    if (columnCount < 0)
                    {
                        columnCount = tableRow.Cells.Count;
                    }
                    else if (tableRow.Cells.Count != columnCount)
                    {
                        throw new InvalidOperationException($"The number of columns in row '{rowIndex}' did not match the number of columns in previous rows (including the header row).");
                    }

                    tableRows.Add(tableRow);

                    rowIndex++;
                    valueRowCount++;
                }
            }

            return new TableData(tableHeaders, tableRows, maxWidths);
        }
    }
}
