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
    }

    public class CellData : ICellData
    {
        public int Column { get; }

        public int Row { get; }

        public IReadOnlyList<string>? Lines { get; }

        public int Width { get; }

        public int Height { get; }

        public CellData(int column, int row, IReadOnlyList<string>? lines, int width, int height)
        {
            Column = column;
            Row = row;
            Lines = lines;
            Width = width;
            Height = height;
        }
    }
}
