namespace TextTabulator
{
    public interface ITableStyling
    {
        string ColumnLeftPadding { get; set; }

        string ColumnRightPadding { get; set; }

        char ColumnSeparator { get; set; }

        char ValueRowSeparator { get; set; }

        char ValueLeftEdgeJoint { get; set; }

        char ValueMiddleJoint { get; set; }

        char ValueRightEdgeJoint { get; set; }

        char HeaderRowSeparator { get; set; }

        char HeaderLeftEdgeJoint { get; set; }

        char HeaderMiddleJoint { get; set; }

        char HeaderRightEdgeJoint { get; set; }

        char LeftEdge { get; set; }

        char RightEdge { get; set; }

        char TopEdge { get; set; }

        char BottomEdge { get; set; }

        char TopLeftCorner { get; set; }

        char TopRightCorner { get; set; }

        char BottomRightCorner { get; set; }

        char BottomLeftCorner { get; set; }

        char TopEdgeJoint { get; set; }

        char BottomEdgeJoint { get; set; }
    }

    public class AsciiTableStyling : ITableStyling
    {
        public string ColumnLeftPadding { get; set; } = string.Empty;

        public string ColumnRightPadding { get; set; } = string.Empty;

        public char ColumnSeparator { get; set; } = '|';

        public char ValueRowSeparator { get; set; } = '-';

        public char ValueLeftEdgeJoint { get; set; } = '|';

        public char ValueMiddleJoint { get; set; } = '+';

        public char ValueRightEdgeJoint { get; set; } = '|';

        public char HeaderRowSeparator { get; set; } = '-';

        public char HeaderLeftEdgeJoint { get; set; } = '|';

        public char HeaderMiddleJoint { get; set; } = '+';

        public char HeaderRightEdgeJoint { get; set; } = '|';

        public char LeftEdge { get; set; } = '|';

        public char RightEdge { get; set; } = '|';

        public char TopEdge { get; set; } = '-';

        public char BottomEdge { get; set; } = '-';

        public char TopLeftCorner { get; set; } = '-';

        public char TopRightCorner { get; set; } = '-';

        public char BottomRightCorner { get; set; } = '-';

        public char BottomLeftCorner { get; set; } = '-';

        public char TopEdgeJoint { get; set; } = '-';

        public char BottomEdgeJoint { get; set; } = '-';
    }

    public class UnicodeTableStyling : ITableStyling
    {
        public string ColumnLeftPadding { get; set; } = string.Empty;

        public string ColumnRightPadding { get; set; } = string.Empty;

        public char ColumnSeparator { get; set; } = '│';

        public char ValueRowSeparator { get; set; } = '─';

        public char ValueLeftEdgeJoint { get; set; } = '╟';

        public char ValueMiddleJoint { get; set; } = '┼';

        public char ValueRightEdgeJoint { get; set; } = '╢';

        public char HeaderRowSeparator { get; set; } = '═';

        public char HeaderLeftEdgeJoint { get; set; } = '╠';

        public char HeaderMiddleJoint { get; set; } = '╪';

        public char HeaderRightEdgeJoint { get; set; } = '╣';

        public char LeftEdge { get; set; } = '║';

        public char RightEdge { get; set; } = '║';

        public char TopEdge { get; set; } = '═';

        public char BottomEdge { get; set; } = '═';

        public char TopLeftCorner { get; set; } = '╔';

        public char TopRightCorner { get; set; } = '╗';

        public char BottomRightCorner { get; set; } = '╝';

        public char BottomLeftCorner { get; set; } = '╚';

        public char TopEdgeJoint { get; set; } = '╤';

        public char BottomEdgeJoint { get; set; } = '╧';
    }
}
