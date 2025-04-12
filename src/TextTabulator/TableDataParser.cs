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
        private readonly IRowDataParser _rowDataParser = new RowDataParser();

        public ITableData Parse(IEnumerable<string>? headers, IEnumerable<IEnumerable<string>>? rows)
        {
            IRowData? tableHeaders = null;
            List<IRowData>? tableRows = null;
            var rowIndex = 0;
            var maxWidths = new List<int>();
            var columnCount = -1;

            if (headers != null)
            {
                tableHeaders = _rowDataParser.Parse(rowIndex, headers, ref maxWidths);

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
                    var tableRow = _rowDataParser.Parse(rowIndex, row, ref maxWidths);

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

            if (tableHeaders?.Cells != null)
            {
                var headerDataList = new ICellData[tableHeaders.Cells.Count];

                for (var i = 0; i < tableHeaders.Cells.Count; i++)
                {
                    headerDataList[i] = new CellData(tableHeaders.Cells[i], maxWidths[i]);
                }

                tableHeaders = new RowData(tableHeaders.Row, headerDataList, tableHeaders.MaxHeight);
            }

            if (tableRows != null)
            {
                for (var i = 0; i < tableRows.Count; i++)
                {
                    var cells = tableRows[i].Cells;

                    if (cells == null)
                    {
                        continue;
                    }

                    var rowDataList = new ICellData[cells.Count];

                    for (var j = 0; j < cells.Count; j++)
                    {
                        rowDataList[j] = new CellData(cells[j], maxWidths[j]);
                    }

                    tableRows[i] = new RowData(tableRows[i].Row, rowDataList, tableRows[i].MaxHeight);
                }
            }

            return new TableData(tableHeaders, tableRows);
        }
    }
}
