using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextTabulator.Adapter;

namespace TextTabulator
{
    public class Tabulator
    {
        public string Tabulate(ITabulatorAdapter adapter, TabulatorOptions? options = null)
        {
            var headers = adapter.GetHeaderStrings() ?? Array.Empty<string>();
            var values = adapter.GetValueStrings();

            return Tabulate(headers, values, options);
        }

        public string Tabulate(IEnumerable<IEnumerable<CellValue>> rowValues, TabulatorOptions? options = null)
        {
            var rowValueStrings = rowValues.Select(i => i.Select(j => j.Invoke()));

            return Tabulate(Array.Empty<string>(), rowValueStrings, options);
        }

        public string Tabulate(IEnumerable<CellValue> headers, IEnumerable<IEnumerable<CellValue>> rowValues, TabulatorOptions? options = null)
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
            var hasHeaders = headers.Any();
            var hasRowValues = rowValues.Any();

            if (!hasHeaders && !hasRowValues)
            {
                return string.Empty;
            }

            var table = new StringBuilder();

            // Start with the top edge of the table.
            var topEdge = BuildTopEdge(options, maxColumnWidths);
            table.Append(topEdge + options.NewLine);

            var middleRowSeparator = BuildRowSeparator(options, maxColumnWidths, false);

            if (hasHeaders)
            {
                // Add the header row.
                var headerRow = BuildRowHeaders(headers, maxColumnWidths, options);
                table.Append(headerRow + options.NewLine);

                if (hasRowValues)
                {
                    var headerRowSeparator = BuildRowSeparator(options, maxColumnWidths, true);
                    table.Append(headerRowSeparator + options.NewLine);
                }
            }

            var row = 0;

            foreach (var rowValue in rowValues)
            {
                // Add the row values.
                var rowString = BuildRowValues(rowValue, maxColumnWidths, row, options);
                table.Append(rowString + options.NewLine);

                if (row < rowCount - 1)
                {
                    // Add the row separator.
                    table.Append(middleRowSeparator + options.NewLine);
                }

                row++;
            }

            var bottomEdge = BuildBottomEdge(options, maxColumnWidths);
            table.Append(bottomEdge + options.NewLine);

            return table.ToString();
        }

        private string BuildTopEdge(TabulatorOptions options, int[] maxColumnWidths)
        {
            var sb = new StringBuilder();

            sb.Append(options.Styling.TopLeftCorner);

            for (var i = 0; i < maxColumnWidths.Length; i++)
            {
                sb.Append(options.Styling.TopEdge, options.Styling.ColumnLeftPadding.Length);
                sb.Append(options.Styling.TopEdge, maxColumnWidths[i]);
                sb.Append(options.Styling.TopEdge, options.Styling.ColumnRightPadding.Length);

                if (i < maxColumnWidths.Length - 1)
                {
                    sb.Append(options.Styling.TopEdgeJoint);
                }
            }

            sb.Append(options.Styling.TopRightCorner);

            return sb.ToString();
        }

        private string BuildRowSeparator(TabulatorOptions options, int[] maxColumnWidths, bool isHeaderRow)
        {
            var sb = new StringBuilder();

            sb.Append(isHeaderRow ? options.Styling.HeaderLeftEdgeJoint : options.Styling.ValueLeftEdgeJoint);

            var rowSeparator = isHeaderRow ? options.Styling.HeaderRowSeparator : options.Styling.ValueRowSeparator;
            var middleJoint = isHeaderRow ? options.Styling.HeaderMiddleJoint : options.Styling.ValueMiddleJoint;

            for (var i = 0; i < maxColumnWidths.Length; i++)
            {
                sb.Append(rowSeparator, options.Styling.ColumnLeftPadding.Length);
                sb.Append(rowSeparator, maxColumnWidths[i]);
                sb.Append(rowSeparator, options.Styling.ColumnRightPadding.Length);

                if (i < maxColumnWidths.Length - 1)
                {
                    sb.Append(middleJoint);
                }
            }

            sb.Append(isHeaderRow ? options.Styling.HeaderRightEdgeJoint : options.Styling.ValueRightEdgeJoint);

            return sb.ToString();
        }

        private string BuildBottomEdge(TabulatorOptions options, int[] maxColumnWidths)
        {
            var sb = new StringBuilder();

            sb.Append(options.Styling.BottomLeftCorner);

            for (var i = 0; i < maxColumnWidths.Length; i++)
            {
                sb.Append(options.Styling.BottomEdge, options.Styling.ColumnLeftPadding.Length);
                sb.Append(options.Styling.BottomEdge, maxColumnWidths[i]);
                sb.Append(options.Styling.BottomEdge, options.Styling.ColumnRightPadding.Length);

                if (i < maxColumnWidths.Length - 1)
                {
                    sb.Append(options.Styling.BottomEdgeJoint);
                }
            }

            sb.Append(options.Styling.BottomRightCorner);

            return sb.ToString();
        }

        private static string BuildRowHeaders(IEnumerable<string> values, int[] maxColumnWidths, TabulatorOptions options)
        {
            return BuildRow(values, maxColumnWidths, -1, options, (c, r) => options.CellAlignment.GetHeaderAlignment(c));
        }

        private static string BuildRowValues(IEnumerable<string> values, int[] maxColumnWidths, int row, TabulatorOptions options)
        {
            return BuildRow(values, maxColumnWidths, row, options, (c, r) => options.CellAlignment.GetValueAlignment(c, r));
        }

        private static string BuildRow(IEnumerable<string> values, int[] maxColumnWidths, int row, TabulatorOptions options, Func<int, int, CellAlignment> alignmentProvider)
        {
            var rowString = new StringBuilder();
            var col = 0;

            // Account for the left edge of the table.
            rowString.Append(options.Styling.LeftEdge);

            // Add the value, padding, and the right column separator for each column.
            // Note that this will also account for the right edge of the table.
            foreach (var value in values)
            {
                var cellAlignment = alignmentProvider(col, row);

                rowString.Append(options.Styling.ColumnLeftPadding);

                var leftOffset = 0;

                if (cellAlignment == CellAlignment.Right)
                {
                    leftOffset = maxColumnWidths[col] - value.Length;
                }
                else if (cellAlignment == CellAlignment.CenterLeftBias)
                {
                    leftOffset = (maxColumnWidths[col] - value.Length) / 2;
                }
                else if (cellAlignment == CellAlignment.CenterRightBias)
                {
                    leftOffset = ((maxColumnWidths[col] - value.Length) / 2) + ((maxColumnWidths[col] - value.Length) % 2 == 0 ? 0 : 1);
                }

                rowString.Append(' ', leftOffset);

                rowString.Append(value);

                var rightOffset = 0;

                if (cellAlignment == CellAlignment.Left)
                {
                    rightOffset = maxColumnWidths[col] - value.Length;
                }
                else if (cellAlignment == CellAlignment.CenterLeftBias)
                {
                    rightOffset = ((maxColumnWidths[col] - value.Length) / 2) + ((maxColumnWidths[col] - value.Length) % 2 == 0 ? 0 : 1);
                }
                else if (cellAlignment == CellAlignment.CenterRightBias)
                {
                    rightOffset = (maxColumnWidths[col] - value.Length) / 2;
                }

                rowString.Append(' ', rightOffset);

                // Add the right padding for the column.
                rowString.Append(options.Styling.ColumnRightPadding);

                // Add the right column separator or, if this is the last value in the row, the right edge of the table.
                rowString.Append(col < maxColumnWidths.Length - 1 ? options.Styling.ColumnSeparator : options.Styling.RightEdge);

                col++;
            }

            return rowString.ToString();
        }
    }
}
