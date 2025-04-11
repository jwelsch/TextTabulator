using System.Collections.Generic;

namespace TextTabulator
{
    public interface IRowDataParser
    {
        IRowData Parse(int row, IEnumerable<string>? cells);
    }

    public class RowDataParser : IRowDataParser
    {
        private readonly ICellDataParser _cellDataParser = new CellDataParser();

        public IRowData Parse(int row, IEnumerable<string>? cells)
        {
            if (cells == null)
            {
                return new RowData(row, null, 0);
            }

            var cellDataList = new List<ICellData>();
            var columnIndex = 0;
            var maxHeight = 1;

            foreach (var cell in cells)
            {
                var cellData = _cellDataParser.Parse(columnIndex, row, cell);
                cellDataList.Add(cellData);

                if (cellData.Height > maxHeight)
                {
                    maxHeight = cellData.Height;
                }

                columnIndex++;
            }

            return new RowData(row, cellDataList, maxHeight);
        }
    }
}
