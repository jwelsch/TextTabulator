using System.Collections.Generic;

namespace TextTabulator
{
    public interface ICellData
    {
        int Column { get; }

        int Row { get; }

        IReadOnlyList<string>? Lines { get; }

        int Width { get; }

        int Height { get; }

        int MaxWidth => -1;
    }

    public class IndividualCellData : ICellData
    {
        public int Column { get; }

        public int Row { get; }

        public IReadOnlyList<string>? Lines { get; }

        public int Width { get; }

        public int Height { get; }

        public IndividualCellData(int column, int row, IReadOnlyList<string>? lines, int width, int height)
        {
            Column = column;
            Row = row;
            Lines = lines;
            Width = width;
            Height = height;
        }
    }

    public class CellData : ICellData
    {
        private readonly ICellData _cellData;

        public int Column => _cellData.Column;

        public int Row => _cellData.Row;

        public IReadOnlyList<string>? Lines => _cellData.Lines;

        public int Width => _cellData.Width;

        public int Height => _cellData.Height;

        public int MaxWidth { get; }

        public CellData(ICellData cellData, int maxWidth)
        {
            _cellData = cellData;
            MaxWidth = maxWidth;
        }
    }
}
