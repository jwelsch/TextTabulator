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
        /// <param name="rowValues">Enumeration containing objects for each value in each row.</param>
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
        /// <param name="headers">Enumeration containing objects for each header.</param>
        /// <param name="rowValues">Enumeration containing objects for each value in each row.</param>
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

            var maxColumnWidths = new List<int>();
            var maxRowHeights = new List<int>();

            var headerRowColumnData = GetRowAndColumnData(headers.Any() ? new List<IEnumerable<string>> { headers } : new List<IEnumerable<string>>(), ref maxColumnWidths, ref maxRowHeights);

            var headerCount = maxColumnWidths.Count;

            var rowValuesData = GetRowAndColumnData(rowValues, ref maxColumnWidths, ref maxRowHeights);

            if (headerCount != 0 && headerCount != maxColumnWidths.Count)
            {
                throw new Exception($"The number of headers ({headerCount}) does not match the number of values in each row ({maxColumnWidths.Count}).");
            }

            return TabulateData(headerRowColumnData, rowValuesData, maxColumnWidths.ToArray(), maxRowHeights.ToArray(), options);
        }

        private IRowColumnData GetRowAndColumnData(IEnumerable<IEnumerable<string>> rowValues, ref List<int> maxColumnWidths, ref List<int> maxRowHeights)
        {
            var rowValuesData = new List<IList<IRowValue>>();
            var row = 0;
            var col = 0;

            foreach (var rowValue in rowValues)
            {
                var rowValueList = new List<IRowValue>();

                col = 0;

                foreach (var value in rowValue)
                {
                    if (maxColumnWidths.Count <= col)
                    {
                        maxColumnWidths.Add(0);
                    }

                    if (maxRowHeights.Count <= row)
                    {
                        maxRowHeights.Add(0);
                    }

                    var valueBlock = GetValueBlock(value);

                    if (maxColumnWidths[col] < valueBlock.Size.Width)
                    {
                        maxColumnWidths[col] = valueBlock.Size.Width;
                    }

                    if (maxRowHeights[row] < valueBlock.Size.Height)
                    {
                        maxRowHeights[row] = valueBlock.Size.Height;
                    }

                    rowValueList.Add(new RowValue(row, col, valueBlock));

                    col++;
                }

                rowValuesData.Add(rowValueList);

                row++;
            }

            return new RowColumnData(rowValuesData, row, col, maxColumnWidths, maxRowHeights);
        }

        private static IValueBlock GetValueBlock(string rowValue)
        {
            var lines = new List<string>();
            var carriageReturn = false;
            var maxWidth = 0;
            var height = 1;
            var sb = new StringBuilder();

            for (var i = 0; i < rowValue.Length; i++)
            {
                var c = rowValue[i];

                if (c == '\r')
                {
                    carriageReturn = true;
                }
                else if (c == '\n')
                {
                    if (carriageReturn)
                    {
                        carriageReturn = false;
                    }
                    height++;
                    if (sb.Length > maxWidth)
                    {
                        maxWidth = sb.Length;
                    }
                    lines.Add(sb.ToString());
                    sb.Clear();
                }
                else
                {
                    if (carriageReturn)
                    {
                        carriageReturn = false;
                        height++;
                        if (sb.Length > maxWidth)
                        {
                            maxWidth = sb.Length;
                        }
                        lines.Add(sb.ToString());
                        sb.Clear();
                        continue;
                    }

                    sb.Append(c);
                }
            }

            lines.Add(sb.ToString());

            if (sb.Length > maxWidth)
            {
                maxWidth = sb.Length;
            }

            return new ValueBlock(lines, new Dimension(maxWidth, height));
        }

        private string TabulateData(IRowColumnData headersRowColumnData, IRowColumnData valuesRowColumnData, int[] maxColumnWidths, int[] maxRowHeights, TabulatorOptions options)
        {
            var hasHeaders = headersRowColumnData.RowCount > 0;
            var hasRowValues = valuesRowColumnData.RowCount > 0;

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
                var headerRow = BuildRowHeaders(headersRowColumnData.RowValues[0], maxColumnWidths, maxRowHeights, options);
                table.Append(headerRow + options.NewLine);

                if (hasRowValues)
                {
                    var headerRowSeparator = BuildRowSeparator(options, maxColumnWidths, true);
                    table.Append(headerRowSeparator + options.NewLine);
                }
            }

            var row = 0;

            foreach (var rowValue in valuesRowColumnData.RowValues)
            {
                // Add the row values.
                var rowString = BuildRowValues(rowValue, maxColumnWidths, maxRowHeights, row, options);
                table.Append(rowString + options.NewLine);

                if (row < valuesRowColumnData.RowCount - 1)
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

        private static string BuildRowHeaders(IEnumerable<IRowValue> headers, int[] maxColumnWidths, int[] maxRowHeights, TabulatorOptions options)
        {
            return BuildRow(headers, maxColumnWidths, maxRowHeights, -1, options, (c, r) => options.CellAlignment.GetHeaderAlignment(c));
        }

        private static string BuildRowValues(IEnumerable<IRowValue> values, int[] maxColumnWidths, int[] maxRowHeights, int row, TabulatorOptions options)
        {
            return BuildRow(values, maxColumnWidths, maxRowHeights, row, options, (c, r) => options.CellAlignment.GetValueAlignment(c, r));
        }

        private static string BuildRow(IEnumerable<IRowValue> values, int[] maxColumnWidths, int[] maxRowHeights, int row, TabulatorOptions options, Func<int, int, CellAlignment> alignmentProvider)
        {
            var rowString = new StringBuilder();
            var col = 0;

            // Account for the left edge of the table.
            rowString.Append(options.Styling.LeftEdge);

            // Add the value, padding, and the right column separator for each column.
            // Note that this will also account for the right edge of the table.
            foreach (var value in values)
            {
                for (var i = 0; i < value.Value.Lines.Count; i++)
                {
                    var line = value.Value.Lines[i];

                    var cellAlignment = alignmentProvider(col, row);

                    if (i != 0)
                    {
                        rowString.Append(options.Styling.LeftEdge);
                    }

                    rowString.Append(options.Styling.ColumnLeftPadding);

                    var leftOffset = 0;

                    if (cellAlignment == CellAlignment.Right)
                    {
                        leftOffset = maxColumnWidths[col] - line.Length;
                    }
                    else if (cellAlignment == CellAlignment.CenterLeftBias)
                    {
                        leftOffset = (maxColumnWidths[col] - line.Length) / 2;
                    }
                    else if (cellAlignment == CellAlignment.CenterRightBias)
                    {
                        leftOffset = ((maxColumnWidths[col] - line.Length) / 2) + ((maxColumnWidths[col] - line.Length) % 2 == 0 ? 0 : 1);
                    }

                    rowString.Append(' ', leftOffset);

                    rowString.Append(line);

                    var rightOffset = 0;

                    if (cellAlignment == CellAlignment.Left)
                    {
                        rightOffset = maxColumnWidths[col] - line.Length;
                    }
                    else if (cellAlignment == CellAlignment.CenterLeftBias)
                    {
                        rightOffset = ((maxColumnWidths[col] - line.Length) / 2) + ((maxColumnWidths[col] - line.Length) % 2 == 0 ? 0 : 1);
                    }
                    else if (cellAlignment == CellAlignment.CenterRightBias)
                    {
                        rightOffset = (maxColumnWidths[col] - line.Length) / 2;
                    }

                    rowString.Append(' ', rightOffset);

                    // Add the right padding for the column.
                    rowString.Append(options.Styling.ColumnRightPadding);

                    // Add the right column separator or, if this is the last value in the row, the right edge of the table.
                    rowString.Append(col < maxColumnWidths.Length - 1 ? options.Styling.ColumnSeparator : options.Styling.RightEdge);

                    if (i < value.Value.Lines.Count - 1)
                    {
                        rowString.Append(options.NewLine);
                    }
                }

                col++;
            }

            return rowString.ToString();
        }
    }
}
