namespace TextTabulator
{
    public interface ITabulatorOptions
    {
    }

    public class TabulatorOptions : ITabulatorOptions
    {
        public char ColumnSeparator { get; set; } = '|';

        public string ColumnLeftPadding { get; set; } = string.Empty;

        public string ColumnRightPadding { get; set; } = string.Empty;

        public char RowSeparator { get; set; } = '-';

        //public char TopLeftCorner { get; set; } = '-';

        //public char TopRightCorner { get; set; } = '-';

        //public char BottomRightCorner { get; set; } = '-';

        //public char BottomLeftCorner { get; set; } = '-';

        //public char LeftEdgeJoint { get; set; } = '|';

        //public char RightEdgeJoint { get; set; } = '|';

        //public char TopEdgeJoint { get; set; } = '|';

        //public char BottomEdgeJoint { get; set; } = '|';

        //public char MiddleJoint { get; set; } = '-';

        public ICellAlignmentProvider CellAlignmentProvider { get; set; } = new UniformAlignmentProvider(CellAlignment.Left);
    }
}
