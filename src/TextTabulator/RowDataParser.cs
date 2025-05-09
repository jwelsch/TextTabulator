using System.Collections.Generic;

namespace TextTabulator
{
    public interface IRowDataParser
    {
        IRowData Parse(int row, IEnumerable<string>? cells, ref List<int> maxWidths);
    }

    public class RowDataParser : IRowDataParser
    {
        private readonly ITabulatorOptions _options;

        public RowDataParser(ITabulatorOptions options)
        {
            _options = options;
        }

        public IRowData Parse(int row, IEnumerable<string>? cells, ref List<int> maxWidths)
        {
            if (cells == null)
            {
                return new RowData(row, null, 0);
            }

            var cellDataParser = new CellDataParser(_options);
            var cellDataList = new List<ICellData>();
            var columnIndex = 0;
            var maxHeight = 1;

            foreach (var cell in cells)
            {
                var cellData = cellDataParser.Parse(columnIndex, row, cell);
                cellDataList.Add(cellData);

                if (cellData.Height > maxHeight)
                {
                    maxHeight = cellData.Height;
                }

                //
                // Record maximum widths here to reduce the number of times looping through the cells is required.
                // Once all the rows in the table have been parsed, maxWidths will have accurate data.
                //

                while (maxWidths.Count <= columnIndex)
                {
                    maxWidths.Add(0);
                }

                if (cellData.Width > maxWidths[columnIndex])
                {
                    maxWidths[columnIndex] = cellData.Width;
                }

                columnIndex++;
            }

            return new RowData(row, cellDataList, maxHeight);
        }
    }
}
