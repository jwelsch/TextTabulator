namespace TextTabulator
{
    public interface IRowValue
    {
        int Row { get; }

        int Column { get; }

        IValueBlock Value { get; }
    }

    public class RowValue : IRowValue
    {
        public int Row { get; }

        public int Column { get; }

        public IValueBlock Value { get; }

        public RowValue(int row, int column, IValueBlock value)
        {
            Row = row;
            Column = column;
            Value = value;
        }
    }
}
