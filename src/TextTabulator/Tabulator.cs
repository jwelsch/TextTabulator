using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextTabulator
{
    public delegate string TableValue();

    public enum ColumnAlignment
    {
        Left,
        Right
    };

    public class Tabulator
    {
        public char ColumnSeparator { get; set; } = '|';

        public string ColumnLeftPadding { get; set; } = string.Empty;

        public string ColumnRightPadding { get; set; } = string.Empty;

        public char RowSeparator { get; set; } = '-';

        public string Tabulate(IEnumerable<IEnumerable<TableValue>> rowValues)
        {
            var rowValueStrings = rowValues.Select(i => i.Select(j => j.Invoke()));

            return Tabulate(Array.Empty<string>(), rowValueStrings);
        }

        public string Tabulate(IEnumerable<TableValue> headers, IEnumerable<IEnumerable<TableValue>> rowValues)
        {
            var headerStrings = headers.Select(i => i.Invoke());
            var rowValueStrings = rowValues.Select(i => i.Select(j => j.Invoke()));

            return Tabulate(headerStrings, rowValueStrings);
        }

        public string Tabulate(IEnumerable<IEnumerable<object>> rowValues)
        {
            var rowValueStrings = rowValues.Select(i => i.Select(j => j.ToString()));

            return Tabulate(Array.Empty<string>(), rowValueStrings);
        }

        public string Tabulate(IEnumerable<object> headers, IEnumerable<IEnumerable<object>> rowValues)
        {
            var headerStrings = headers.Select(i => i.ToString());
            var rowValueStrings = rowValues.Select(i => i.Select(j => j.ToString()));

            return Tabulate(headerStrings, rowValueStrings);
        }

        public string Tabulate(IEnumerable<IEnumerable<string>> rowValues)
        {
            return Tabulate(Array.Empty<string>(), rowValues);
        }

        public string Tabulate(IEnumerable<string> headers, IEnumerable<IEnumerable<string>> rowValues)
        {
            var maxColumnWidths = new List<int>();

            GetRowAndColumnData(new List<IEnumerable<string>> { headers }, ref maxColumnWidths);

            var headerCount = maxColumnWidths.Count;

            var rowCount = GetRowAndColumnData(rowValues, ref maxColumnWidths);

            if (headerCount != 0 && headerCount != maxColumnWidths.Count)
            {
                throw new Exception($"The number of headers ({headerCount}) does not match the number of values in each row ({maxColumnWidths.Count}).");
            }

            return TabulateData(headers, rowValues, maxColumnWidths.ToArray(), rowCount);
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

        private string TabulateData(IEnumerable<string> headers, IEnumerable<IEnumerable<string>> rowValues, int[] maxColumnWidths, int rowCount)
        {
            var entireRowSeparator = BuildEntireRowSeparator(RowSeparator, ColumnSeparator, ColumnLeftPadding, ColumnRightPadding, maxColumnWidths);

            var table = new StringBuilder();

            // Start with the top edge of the table.
            table.AppendLine(entireRowSeparator);

            if (headers.Any())
            {
                // Add the header row.
                var headerRow = BuildRowValues(headers, ColumnSeparator, ColumnLeftPadding, ColumnRightPadding, maxColumnWidths);
                table.AppendLine(headerRow);
                table.AppendLine(entireRowSeparator);
            }

            foreach (var rowValue in rowValues)
            {
                // Add the row values.
                var row = BuildRowValues(rowValue, ColumnSeparator, ColumnLeftPadding, ColumnRightPadding, maxColumnWidths);
                table.AppendLine(row);

                // Add the row separator.
                // Note that this will also account for the bottom edge of the table.
                table.AppendLine(entireRowSeparator);
            }

            return table.ToString();
        }

        private static string BuildEntireRowSeparator(char rowSeparator, char columnSeparator, string columnLeftPadding, string columnRightPadding, int[] maxColumnWidths)
        {
            var entireRowSeparator = new StringBuilder();

            // Account for the left edge of the table.
            entireRowSeparator.Append(rowSeparator);

            // Add enough row separators so that the entire row separator takes into account each column, its padding, and its column separator.
            // This will also account for the right edge of the table.
            for (var i = 0; i < maxColumnWidths.Length; i++)
            {
                entireRowSeparator.Append(rowSeparator, columnLeftPadding.Length + maxColumnWidths[i] + columnRightPadding.Length + 1 /* column separator */);
            }

            return entireRowSeparator.ToString();
        }

        private static string BuildRowValues(IEnumerable<string> values, char columnSeparator, string columnLeftPadding, string columnRightPadding, int[] maxColumnWidths)
        {
            var row = new StringBuilder();
            var col = 0;

            // Account for the left edge of the table.
            row.Append(columnSeparator);

            // Add the value, padding, and the right column separator for each column.
            // Note that this will also account for the right edge of the table.
            foreach (var value in values)
            {
                row.Append(columnLeftPadding);

                row.Append(value);
                row.Append(' ', maxColumnWidths[col] - value.Length);

                row.Append(columnRightPadding + columnSeparator);

                col++;
            }

            return row.ToString();
        }
    }
}
