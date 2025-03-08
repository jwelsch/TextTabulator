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
            var entireRowSeparator = BuildEntireRowSeparator(options, maxColumnWidths);

            var table = new StringBuilder();

            // Start with the top edge of the table.
            table.AppendLine(entireRowSeparator);

            var row = 0;

            if (headers.Any())
            {
                // Add the header row.
                var headerRow = BuildRowValues(headers, maxColumnWidths, row, options);
                table.AppendLine(headerRow);
                table.AppendLine(entireRowSeparator);

                row++;
            }

            foreach (var rowValue in rowValues)
            {
                // Add the row values.
                var rowString = BuildRowValues(rowValue, maxColumnWidths, row, options);
                table.AppendLine(rowString);

                // Add the row separator.
                // Note that this will also account for the bottom edge of the table.
                table.AppendLine(entireRowSeparator);

                row++;
            }

            return table.ToString();
        }

        private static string BuildEntireRowSeparator(TabulatorOptions options, int[] maxColumnWidths)
        {
            var entireRowSeparator = new StringBuilder();

            // Account for the left edge of the table.
            entireRowSeparator.Append(options.RowSeparator);

            // Add enough row separators so that the entire row separator takes into account each column, its padding, and its column separator.
            // This will also account for the right edge of the table.
            for (var i = 0; i < maxColumnWidths.Length; i++)
            {
                entireRowSeparator.Append(options.RowSeparator, options.ColumnLeftPadding.Length + maxColumnWidths[i] + options.ColumnRightPadding.Length + 1 /* column separator */);
            }

            return entireRowSeparator.ToString();
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
