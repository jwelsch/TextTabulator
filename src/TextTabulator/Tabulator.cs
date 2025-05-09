using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextTabulator.Adapters;

namespace TextTabulator
{
    /// <summary>
    /// Main class that performs the tabulation of data.
    /// </summary>
    public class Tabulator
    {
        //private readonly ITableDataParser _tableDataParser = new TableDataParser();

        /// <summary>
        /// Tabulates data and makes callbacks with elements of the table.
        /// </summary>
        /// <param name="adapter">Adapter object that the method can get data from.</param>
        /// <param name="callback">Callback received when an element of the table is constructed.</param>
        /// <param name="options">Options specifying how the table should be constructed.</param>
        public void Tabulate(ITabulatorAdapter adapter, TableCallback callback, TabulatorOptions? options = null)
        {
            var headers = adapter.GetHeaderStrings() ?? Array.Empty<string>();
            var values = adapter.GetValueStrings();

            Tabulate(headers, values, callback, options);
        }

        /// <summary>
        /// Tabulates data and makes callbacks with elements of the table.
        /// </summary>
        /// <param name="rowValues">Enumeration containing CellValues delegates for each value in each row.</param>
        /// <param name="callback">Callback received when an element of the table is constructed.</param>
        /// <param name="options">Options specifying how the table should be constructed.</param>
        public void Tabulate(IEnumerable<IEnumerable<CellValue>> rowValues, TableCallback callback, TabulatorOptions? options = null)
        {
            var rowValueStrings = rowValues.Select(i => i.Select(j => j.Invoke()));

            Tabulate(Array.Empty<string>(), rowValueStrings, callback, options);
        }

        /// <summary>
        /// Tabulates data and makes callbacks with elements of the table.
        /// </summary>
        /// <param name="headers">Enumeration containing CellValues delegates for each header.</param>
        /// <param name="rowValues">Enumeration containing CellValues delegates for each value in each row.</param>
        /// <param name="callback">Callback received when an element of the table is constructed.</param>
        /// <param name="options">Options specifying how the table should be constructed.</param>
        public void Tabulate(IEnumerable<CellValue> headers, IEnumerable<IEnumerable<CellValue>> rowValues, TableCallback callback, TabulatorOptions? options = null)
        {
            var headerStrings = headers.Select(i => i.Invoke());
            var rowValueStrings = rowValues.Select(i => i.Select(j => j.Invoke()));

            Tabulate(headerStrings, rowValueStrings, callback, options);
        }

        /// <summary>
        /// Tabulates data and makes callbacks with elements of the table.
        /// </summary>
        /// <param name="rowValues">Enumeration containing objects for each value in each row. Each object's ToString() method will be called to generate the value displayed in the table.</param>
        /// <param name="callback">Callback received when an element of the table is constructed.</param>
        /// <param name="options">Options specifying how the table should be constructed.</param>
        public void Tabulate(IEnumerable<IEnumerable<object>> rowValues, TableCallback callback, TabulatorOptions? options = null)
        {
            var rowValueStrings = rowValues.Select(i => i.Select(j => j.ToString()));

            Tabulate(Array.Empty<string>(), rowValueStrings, callback, options);
        }

        /// <summary>
        /// Tabulates data and makes callbacks with elements of the table.
        /// </summary>
        /// <param name="headers">Enumeration containing objects for each header. Each object's ToString() method will be called to generate the value displayed in the table.</param>
        /// <param name="rowValues">Enumeration containing objects for each value in each row. Each object's ToString() method will be called to generate the value displayed in the table.</param>
        /// <param name="callback">Callback received when an element of the table is constructed.</param>
        /// <param name="options">Options specifying how the table should be constructed.</param>
        public void Tabulate(IEnumerable<object> headers, IEnumerable<IEnumerable<object>> rowValues, TableCallback callback, TabulatorOptions? options = null)
        {
            var headerStrings = headers.Select(i => i.ToString());
            var rowValueStrings = rowValues.Select(i => i.Select(j => j.ToString()));

            Tabulate(headerStrings, rowValueStrings, callback, options);
        }

        /// <summary>
        /// Tabulates data and makes callbacks with elements of the table.
        /// </summary>
        /// <param name="rowValues">Enumeration containing strings for each value in each row.</param>
        /// <param name="callback">Callback received when an element of the table is constructed.</param>
        /// <param name="options">Options specifying how the table should be constructed.</param>
        public void Tabulate(IEnumerable<IEnumerable<string>> rowValues, TableCallback callback, TabulatorOptions? options = null)
        {
            Tabulate(Array.Empty<string>(), rowValues, callback, options);
        }

        /// <summary>
        /// Tabulates data and makes callbacks with elements of the table.
        /// </summary>
        /// <param name="headers">Enumeration containing strings for each header.</param>
        /// <param name="rowValues"></param>
        /// <param name="callback">Callback received when an element of the table is constructed.</param>
        /// <param name="options">Options specifying how the table should be constructed.</param>
        public void Tabulate(IEnumerable<string> headers, IEnumerable<IEnumerable<string>> rowValues, TableCallback callback, TabulatorOptions? options = null)
        {
            options ??= new TabulatorOptions();

            var tableDataParser = new TableDataParser(options);

            var tableData = tableDataParser.Parse(headers, rowValues);

            TabulateData(tableData, callback, options);
        }

        /// <summary>
        /// Tabulates data and outputs a string representation of a table.
        /// </summary>
        /// <param name="adapter">Adapter object that the method can get data from.</param>
        /// <param name="options">Options specifying how the table should be constructed.</param>
        /// <returns>String representation of a table.</returns>
        public string Tabulate(ITabulatorAdapter adapter, TabulatorOptions? options = null)
        {
            var headers = adapter.GetHeaderStrings() ?? Array.Empty<string>();
            var values = adapter.GetValueStrings();

            return Tabulate(headers, values, options);
        }

        /// <summary>
        /// Tabulates data and outputs a string representation of a table.
        /// </summary>
        /// <param name="rowValues">Enumeration containing CellValues delegates for each value in each row.</param>
        /// <param name="options">Options specifying how the table should be constructed.</param>
        /// <returns>String representation of a table.</returns>
        public string Tabulate(IEnumerable<IEnumerable<CellValue>> rowValues, TabulatorOptions? options = null)
        {
            var rowValueStrings = rowValues.Select(i => i.Select(j => j.Invoke()));

            return Tabulate(Array.Empty<string>(), rowValueStrings, options);
        }

        /// <summary>
        /// Tabulates data and outputs a string representation of a table.
        /// </summary>
        /// <param name="headers">Enumeration containing CellValues delegates for each header.</param>
        /// <param name="rowValues">Enumeration containing CellValues delegates for each value in each row.</param>
        /// <param name="options">Options specifying how the table should be constructed.</param>
        /// <returns>String representation of a table.</returns>
        public string Tabulate(IEnumerable<CellValue> headers, IEnumerable<IEnumerable<CellValue>> rowValues, TabulatorOptions? options = null)
        {
            var headerStrings = headers.Select(i => i.Invoke());
            var rowValueStrings = rowValues.Select(i => i.Select(j => j.Invoke()));

            return Tabulate(headerStrings, rowValueStrings, options);
        }

        /// <summary>
        /// Tabulates data and outputs a string representation of a table.
        /// </summary>
        /// <param name="rowValues">Enumeration containing objects for each value in each row. Each object's ToString() method will be called to generate the value displayed in the table.</param>
        /// <param name="options">Options specifying how the table should be constructed.</param>
        /// <returns>String representation of a table.</returns>
        public string Tabulate(IEnumerable<IEnumerable<object>> rowValues, TabulatorOptions? options = null)
        {
            var rowValueStrings = rowValues.Select(i => i.Select(j => j.ToString()));

            return Tabulate(Array.Empty<string>(), rowValueStrings, options);
        }

        /// <summary>
        /// Tabulates data and outputs a string representation of a table.
        /// </summary>
        /// <param name="headers">Enumeration containing objects for each header. Each object's ToString() method will be called to generate the value displayed in the table.</param>
        /// <param name="rowValues">Enumeration containing objects for each value in each row. Each object's ToString() method will be called to generate the value displayed in the table.</param>
        /// <param name="options">Options specifying how the table should be constructed.</param>
        /// <returns>String representation of a table.</returns>
        public string Tabulate(IEnumerable<object> headers, IEnumerable<IEnumerable<object>> rowValues, TabulatorOptions? options = null)
        {
            var headerStrings = headers.Select(i => i.ToString());
            var rowValueStrings = rowValues.Select(i => i.Select(j => j.ToString()));

            return Tabulate(headerStrings, rowValueStrings, options);
        }

        /// <summary>
        /// Tabulates data and outputs a string representation of a table.
        /// </summary>
        /// <param name="rowValues">Enumeration containing strings for each value in each row.</param>
        /// <param name="options">Options specifying how the table should be constructed.</param>
        /// <returns>String representation of a table.</returns>
        public string Tabulate(IEnumerable<IEnumerable<string>> rowValues, TabulatorOptions? options = null)
        {
            return Tabulate(Array.Empty<string>(), rowValues, options);
        }

        /// <summary>
        /// Tabulates data and outputs a string representation of a table.
        /// </summary>
        /// <param name="headers">Enumeration containing strings for each header.</param>
        /// <param name="rowValues"></param>
        /// <param name="options">Options specifying how the table should be constructed.</param>
        /// <returns>String representation of a table.</returns>
        /// <exception cref="Exception">Thrown when the number of headers does not match the number of values in each row.</exception>
        public string Tabulate(IEnumerable<string> headers, IEnumerable<IEnumerable<string>> rowValues, TabulatorOptions? options = null)
        {
            options ??= new TabulatorOptions();

            var tableDataParser = new TableDataParser(options);

            var tableData = tableDataParser.Parse(headers, rowValues);

            var table = new StringBuilder();

            TabulateData(tableData, t => table.Append(t), options);

            return table.ToString();
        }

        private void TabulateData(ITableData tableData, TableCallback callback, TabulatorOptions options)
        {
            var hasHeaders = !(tableData.Headers == null || tableData.Headers.Cells == null || tableData.Headers.Cells.Count == 0);
            var hasRowValues = !(tableData.ValueRows == null || tableData.ValueRows.Count == 0);

            if (!hasHeaders && !hasRowValues)
            {
                return;
            }

            // Start with the top edge of the table.
            var topEdge = BuildTopEdge(tableData, options);
            callback.Invoke(topEdge + options.NewLine);

            var middleRowSeparator = BuildRowSeparator(tableData, options, false);

            if (hasHeaders)
            {
                // Add the header row.
                var headerRow = BuildRowHeaders(tableData, options);
                callback.Invoke(headerRow + options.NewLine);

                if (hasRowValues)
                {
                    var headerRowSeparator = BuildRowSeparator(tableData, options, true);
                    callback.Invoke(headerRowSeparator + options.NewLine);
                }
            }

            for (var i = 0; tableData.ValueRows != null && i < tableData.ValueRows.Count; i++)
            {
                // Add the row values.
                var rowString = BuildRowValues(tableData.ValueRows[i], tableData.MaxColumnWidths, options);
                callback.Invoke(rowString + options.NewLine);

                if (i < tableData.ValueRows.Count - 1)
                {
                    // Add the row separator.
                    callback.Invoke(middleRowSeparator + options.NewLine);
                }
            }

            var bottomEdge = BuildBottomEdge(tableData, options);
            callback.Invoke(bottomEdge + options.NewLine);

            return;
        }

        private string BuildTopEdge(ITableData tableData, TabulatorOptions options)
        {
            var sb = new StringBuilder();
            IRowData? rowData = tableData.Headers;

            if (rowData == null || (rowData.Cells?.Count ?? 0) == 0)
            {
                if (tableData.ValueRows == null || tableData.ValueRows.Count == 0 || (tableData.ValueRows[0].Cells?.Count ?? 0) == 0)
                {
                    return sb.ToString();
                }

                rowData = tableData.ValueRows[0];
            }

            sb.Append(options.Styling.TopLeftCorner);

            if (rowData != null && rowData.Cells != null)
            {
                for (var i = 0; i < rowData.Cells.Count; i++)
                {
                    sb.Append(options.Styling.TopEdge, options.Styling.ColumnLeftPadding.Length);
                    sb.Append(options.Styling.TopEdge, tableData.MaxColumnWidths[rowData.Cells[i].Column]);
                    sb.Append(options.Styling.TopEdge, options.Styling.ColumnRightPadding.Length);

                    if (i < rowData.Cells.Count - 1)
                    {
                        sb.Append(options.Styling.TopEdgeJoint);
                    }
                }

                sb.Append(options.Styling.TopRightCorner);
            }

            return sb.ToString();
        }

        private string BuildRowSeparator(ITableData tableData, TabulatorOptions options, bool isHeaderRow)
        {
            var sb = new StringBuilder();

            sb.Append(isHeaderRow ? options.Styling.HeaderLeftEdgeJoint : options.Styling.ValueLeftEdgeJoint);

            var rowSeparator = isHeaderRow ? options.Styling.HeaderRowSeparator : options.Styling.ValueRowSeparator;
            var middleJoint = isHeaderRow ? options.Styling.HeaderMiddleJoint : options.Styling.ValueMiddleJoint;

            for (var i = 0; i < tableData.MaxColumnWidths.Count; i++)
            {
                sb.Append(rowSeparator, options.Styling.ColumnLeftPadding.Length);
                sb.Append(rowSeparator, tableData.MaxColumnWidths[i]);
                sb.Append(rowSeparator, options.Styling.ColumnRightPadding.Length);

                if (i < tableData.MaxColumnWidths.Count - 1)
                {
                    sb.Append(middleJoint);
                }
            }

            sb.Append(isHeaderRow ? options.Styling.HeaderRightEdgeJoint : options.Styling.ValueRightEdgeJoint);

            return sb.ToString();
        }

        private string BuildBottomEdge(ITableData tableData, TabulatorOptions options)
        {
            var sb = new StringBuilder();

            sb.Append(options.Styling.BottomLeftCorner);

            for (var i = 0; i < tableData.MaxColumnWidths.Count; i++)
            {
                sb.Append(options.Styling.BottomEdge, options.Styling.ColumnLeftPadding.Length);
                sb.Append(options.Styling.BottomEdge, tableData.MaxColumnWidths[i]);
                sb.Append(options.Styling.BottomEdge, options.Styling.ColumnRightPadding.Length);

                if (i < tableData.MaxColumnWidths.Count - 1)
                {
                    sb.Append(options.Styling.BottomEdgeJoint);
                }
            }

            sb.Append(options.Styling.BottomRightCorner);

            return sb.ToString();
        }

        private static string BuildRowHeaders(ITableData tableData, TabulatorOptions options)
        {
            return BuildRow(tableData.Headers, tableData.MaxColumnWidths, options, (c, r) => options.CellAlignment.GetHeaderAlignment(c));
        }

        private static string BuildRowValues(IRowData? rowData, IReadOnlyList<int> maxColumnWidths, TabulatorOptions options)
        {
            return BuildRow(rowData, maxColumnWidths, options, (c, r) => options.CellAlignment.GetValueAlignment(c, r));
        }

        private static string BuildRow(IRowData? rowData, IReadOnlyList<int> maxColumnWidths, TabulatorOptions options, Func<int, int, CellAlignment> alignmentProvider)
        {
            var rowString = new StringBuilder();

            if (rowData == null || rowData.Cells == null || rowData.Cells.Count == 0)
            {
                return rowString.ToString();
            }

            for (var i = 0; i < rowData.MaxHeight; i++)
            {
                for (var j = 0; j < rowData.Cells.Count; j++)
                {
                    var cell = rowData.Cells[j];

                    if (cell.Lines == null)
                    {
                        continue;
                    }

                    // Account for the left edge of the table.
                    if (cell.Column == 0)
                    {
                        rowString.Append(options.Styling.LeftEdge);
                    }

                    // If the cell has less lines that the tallest cell in the row substitute an empty string.
                    var line = i >= cell.Height ? string.Empty : cell.Lines[i];

                    var cellAlignment = alignmentProvider(cell.Column, cell.Row);

                    rowString.Append(options.Styling.ColumnLeftPadding);

                    var leftOffset = 0;

                    if (cellAlignment == CellAlignment.Right)
                    {
                        leftOffset = maxColumnWidths[cell.Column] - line.Length;
                    }
                    else if (cellAlignment == CellAlignment.CenterLeftBias)
                    {
                        leftOffset = (maxColumnWidths[cell.Column] - line.Length) / 2;
                    }
                    else if (cellAlignment == CellAlignment.CenterRightBias)
                    {
                        leftOffset = ((maxColumnWidths[cell.Column] - line.Length) / 2) + ((maxColumnWidths[cell.Column] - line.Length) % 2 == 0 ? 0 : 1);
                    }

                    rowString.Append(' ', leftOffset);

                    rowString.Append(line);

                    var rightOffset = 0;

                    if (cellAlignment == CellAlignment.Left)
                    {
                        rightOffset = maxColumnWidths[cell.Column] - line.Length;
                    }
                    else if (cellAlignment == CellAlignment.CenterLeftBias)
                    {
                        rightOffset = ((maxColumnWidths[cell.Column] - line.Length) / 2) + ((maxColumnWidths[cell.Column] - line.Length) % 2 == 0 ? 0 : 1);
                    }
                    else if (cellAlignment == CellAlignment.CenterRightBias)
                    {
                        rightOffset = (maxColumnWidths[cell.Column] - line.Length) / 2;
                    }

                    rowString.Append(' ', rightOffset);

                    // Add the right padding for the column.
                    rowString.Append(options.Styling.ColumnRightPadding);

                    // If the cell is not the last cell in the row add a column separator.
                    // If the cell is the last cell in the roww add a right edge.
                    rowString.Append(cell.Column < maxColumnWidths.Count - 1 ? options.Styling.ColumnSeparator : options.Styling.RightEdge);
                }

                // If there are more lines add a new line.
                if (i < rowData.MaxHeight - 1)
                {
                    rowString.Append(options.NewLine);
                }
            }

            return rowString.ToString();
        }
    }
}
