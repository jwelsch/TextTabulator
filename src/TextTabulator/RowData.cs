using System.Collections.Generic;

namespace TextTabulator
{
    public interface IRowData
    {
        int Row { get; }

        IReadOnlyList<ICellData>? Cells { get; }

        int MaxHeight { get; }
    }

    public class RowData : IRowData
    {
        public int Row { get; }

        public IReadOnlyList<ICellData>? Cells { get; }

        public int MaxHeight { get; }

        public RowData(int row, IReadOnlyList<ICellData>? cells, int maxHeight)
        {
            Row = row;
            Cells = cells;
            MaxHeight = maxHeight;
        }
    }
}
