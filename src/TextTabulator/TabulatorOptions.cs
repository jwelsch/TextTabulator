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

        public char ValueRowSeparator { get; set; } = '-';

        public char HeaderRowSeparator { get; set;} = '-';

        public char TopLeftCorner { get; set; } = '-';

        public char TopRightCorner { get; set; } = '-';

        public char BottomRightCorner { get; set; } = '-';

        public char BottomLeftCorner { get; set; } = '-';

        public char ValueLeftEdgeJoint { get; set; } = '|';

        public char ValueRightEdgeJoint { get; set; } = '|';

        public char HeaderLeftEdgeJoint { get; set; } = '|';

        public char HeaderRightEdgeJoint { get; set; } = '|';

        public char TopEdgeJoint { get; set; } = '-';

        public char BottomEdgeJoint { get; set; } = '-';

        public char ValueMiddleJoint { get; set; } = '-';

        public char HeaderMiddleJoint { get; set; } = '-';

        public ICellAlignmentProvider CellAlignmentProvider { get; set; } = new UniformAlignmentProvider(CellAlignment.Left);
    }
}
