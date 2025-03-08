using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextTabulator
{
    public delegate string TableValue();

    public class Tabulator
    {
        public string Tabulate(IEnumerable<IEnumerable<TableValue>> rowValues, TabulatorOptions? options = null)
        {
            var rowValueStrings = rowValues.Select(i => i.Select(j => j.Invoke()));

            return Tabulate(Array.Empty<string>(), rowValueStrings, options);
        }

        public string Tabulate(IEnumerable<TableValue> headers, IEnumerable<IEnumerable<TableValue>> rowValues, TabulatorOptions? options = null)
        {
            var headerStrings = headers.Select(i => i.Invoke());
            var rowValueStrings = rowValues.Select(i => i.Select(j => j.Invoke()));

            return Tabulate(headerStrings, rowValueStrings, options);
        }

        public string Tabulate(IEnumerable<IEnumerable<object>> rowValues, TabulatorOptions? options = null)
        {
            var rowValueStrings = rowValues.Select(i => i.Select(j => j.ToString()));

            return Tabulate(Array.Empty<string>(), rowValueStrings, options);
        }

        public string Tabulate(IEnumerable<object> headers, IEnumerable<IEnumerable<object>> rowValues, TabulatorOptions? options = null)
        {
            var headerStrings = headers.Select(i => i.ToString());
            var rowValueStrings = rowValues.Select(i => i.Select(j => j.ToString()));

            return Tabulate(headerStrings, rowValueStrings, options);
        }

        public string Tabulate(IEnumerable<IEnumerable<string>> rowValues, TabulatorOptions? options = null)
        {
            return Tabulate(Array.Empty<string>(), rowValues, options);
        }

        public string Tabulate(IEnumerable<string> headers, IEnumerable<IEnumerable<string>> rowValues, TabulatorOptions? options = null)
        {
            options ??= new TabulatorOptions();

            var maxColumnWidths = new List<int>();

            GetRowAndColumnData(new List<IEnumerable<string>> { headers }, ref maxColumnWidths);

            var headerCount = maxColumnWidths.Count;

            var rowCount = GetRowAndColumnData(rowValues, ref maxColumnWidths);

            if (headers.Any())
            {
                rowCount++;
            }

            if (headerCount != 0 && headerCount != maxColumnWidths.Count)
            {
                throw new Exception($"The number of headers ({headerCount}) does not match the number of values in each row ({maxColumnWidths.Count}).");
            }

            return TabulateData(headers, rowValues, maxColumnWidths.ToArray(), rowCount, options);
        }

        private int GetRowAndColumnData(IEnumerable<IEnumerable<string>> rowValues, ref List<int> maxColumnWidths)
        {
            var row = 0;
            int col;

            foreach (var rowValue in rowValues)
            {
                col = 0;

                foreach (var value in rowValue)
                {
                    if (maxColumnWidths.Count <= col)
                    {
                        maxColumnWidths.Add(0);
                    }

                    if (maxColumnWidths[col] < value.Length)
                    {
                        maxColumnWidths[col] = value.Length;
                    }

                    col++;
                }

                row++;
            }

            return row;
        }

        private string TabulateData(IEnumerable<string> headers, IEnumerable<IEnumerable<string>> rowValues, int[] maxColumnWidths, int rowCount, TabulatorOptions options)
        {
            var table = new StringBuilder();

            // Start with the top edge of the table.
            var topEdge = BuildTopEdge(options, maxColumnWidths);
            table.AppendLine(topEdge);

            var middleRowSeparator = BuildRowSeparator(options, maxColumnWidths, false);

            var row = 0;

            if (headers.Any())
            {
                // Add the header row.
                var headerRow = BuildRowValues(headers, maxColumnWidths, 0, options);
                table.AppendLine(headerRow);

                var headerRowSeparator = BuildRowSeparator(options, maxColumnWidths, true);
                table.AppendLine(headerRowSeparator);

                row++;
            }

            foreach (var rowValue in rowValues)
            {
                // Add the row values.
                var rowString = BuildRowValues(rowValue, maxColumnWidths, row, options);
                table.AppendLine(rowString);

                if (row < rowCount - 1)
                {
                    // Add the row separator.
                    table.AppendLine(middleRowSeparator);
                }

                row++;
            }

            var bottomEdge = BuildBottomEdge(options, maxColumnWidths);
            table.AppendLine(bottomEdge);

            return table.ToString();
        }

        private string BuildTopEdge(TabulatorOptions options, int[] maxColumnWidths)
        {
            var sb = new StringBuilder();

            sb.Append(options.TopLeftCorner);

            for (var i = 0; i < maxColumnWidths.Length; i++)
            {
                sb.Append(options.ValueRowSeparator, options.ColumnLeftPadding.Length);
                sb.Append(options.ValueRowSeparator, maxColumnWidths[i]);
                sb.Append(options.ValueRowSeparator, options.ColumnRightPadding.Length);

                if (i < maxColumnWidths.Length - 1)
                {
                    sb.Append(options.TopEdgeJoint);
                }
            }

            sb.Append(options.TopRightCorner);

            return sb.ToString();
        }

        private string BuildRowSeparator(TabulatorOptions options, int[] maxColumnWidths, bool isHeaderRow)
        {
            var sb = new StringBuilder();

            sb.Append(isHeaderRow ? options.HeaderLeftEdgeJoint : options.ValueLeftEdgeJoint);

            var rowSeparator = isHeaderRow ? options.HeaderRowSeparator : options.ValueRowSeparator;
            var middleJoint = isHeaderRow ? options.HeaderMiddleJoint : options.ValueMiddleJoint;

            for (var i = 0; i < maxColumnWidths.Length; i++)
            {
                sb.Append(rowSeparator, options.ColumnLeftPadding.Length);
                sb.Append(rowSeparator, maxColumnWidths[i]);
                sb.Append(rowSeparator, options.ColumnRightPadding.Length);

                if (i < maxColumnWidths.Length - 1)
                {
                    sb.Append(middleJoint);
                }
            }

            sb.Append(isHeaderRow ? options.HeaderRightEdgeJoint : options.ValueRightEdgeJoint);

            return sb.ToString();
        }

        private string BuildBottomEdge(TabulatorOptions options, int[] maxColumnWidths)
        {
            var sb = new StringBuilder();

            sb.Append(options.BottomLeftCorner);

            for (var i = 0; i < maxColumnWidths.Length; i++)
            {
                sb.Append(options.ValueRowSeparator, options.ColumnLeftPadding.Length);
                sb.Append(options.ValueRowSeparator, maxColumnWidths[i]);
                sb.Append(options.ValueRowSeparator, options.ColumnRightPadding.Length);

                if (i < maxColumnWidths.Length - 1)
                {
                    sb.Append(options.BottomEdgeJoint);
                }
            }

            sb.Append(options.BottomRightCorner);

            return sb.ToString();
        }

        private static string BuildRowValues(IEnumerable<string> values, int[] maxColumnWidths, int row, TabulatorOptions options)
        {
            var rowString = new StringBuilder();
            var col = 0;

            // Account for the left edge of the table.
            rowString.Append(options.ColumnSeparator);

            // Add the value, padding, and the right column separator for each column.
            // Note that this will also account for the right edge of the table.
            foreach (var value in values)
            {
                var cellAlignment = options.CellAlignmentProvider.GetColumnAlignment(col, row);

                rowString.Append(options.ColumnLeftPadding);

                if (cellAlignment == CellAlignment.Right)
                {
                    rowString.Append(' ', maxColumnWidths[col] - value.Length);
                }

                rowString.Append(value);

                if (cellAlignment == CellAlignment.Left)
                {
                    rowString.Append(' ', maxColumnWidths[col] - value.Length);
                }

                rowString.Append(options.ColumnRightPadding + options.ColumnSeparator);

                col++;
            }

            return rowString.ToString();
        }
    }
}
