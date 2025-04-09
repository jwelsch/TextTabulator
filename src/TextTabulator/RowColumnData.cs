using System.Collections.Generic;

namespace TextTabulator
{
    public interface IRowColumnData
    {
        IList<IList<IRowValue>> RowValues { get; }

        int RowCount { get; }

        int ColumnCount { get; }

        IList<int> MaxColumnWidths { get; }

        IList<int> MaxRowHeights { get; }
    }

    public class RowColumnData : IRowColumnData
    {
        public IList<IList<IRowValue>> RowValues { get; }

        public int RowCount { get; }

        public int ColumnCount { get; }

        public IList<int> MaxColumnWidths { get; }

        public IList<int> MaxRowHeights { get; }

        public RowColumnData(IList<IList<IRowValue>> rowValues, int rowCount, int columnCount, IList<int> maxColumnWidths, IList<int> maxRowHeights)
        {
            RowValues = rowValues;
            RowCount = rowCount;
            ColumnCount = columnCount;
            MaxColumnWidths = maxColumnWidths;
            MaxRowHeights = maxRowHeights;
        }
    }
}
