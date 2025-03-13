namespace TextTabulator
{
    /// <summary>
    /// Interface used to implement table styling values.
    /// </summary>
    public interface ITableStyling
    {
        /// <summary>
        /// Gets or sets the string used for padding the left side of a column.
        /// The contents of a cell will be aligned within the left and right padding.
        /// </summary>
        string ColumnLeftPadding { get; set; }

        /// <summary>
        /// Gets or sets the string used for padding the right side of a column.
        /// The contents of a cell will be aligned within the left and right padding.
        /// </summary>
        string ColumnRightPadding { get; set; }

        /// <summary>
        /// Gets or sets the character used for the column separator.
        /// </summary>
        char ColumnSeparator { get; set; }

        /// <summary>
        /// Gets or sets the character used for separating rows of values.
        /// </summary>
        char ValueRowSeparator { get; set; }

        /// <summary>
        /// Gets or sets the character used on the left edge of the table where a
        /// character separating rows of values meets it.
        /// </summary>
        char ValueLeftEdgeJoint { get; set; }

        /// <summary>
        /// Gets or sets the character used where a row separator meets a column separator.
        /// </summary>
        char ValueMiddleJoint { get; set; }

        /// <summary>
        /// Gets or sets the character used on the right edge of the table where a
        /// character separating rows of values meets it.
        /// </summary>
        char ValueRightEdgeJoint { get; set; }

        /// <summary>
        /// Gets or sets the character used for separating the header row from the rows of values.
        /// </summary>
        char HeaderRowSeparator { get; set; }

        /// <summary>
        /// Gets or sets the character used on the left edge of the table where a
        /// character separating the header row from the rows of values meets it.
        /// </summary>
        char HeaderLeftEdgeJoint { get; set; }

        /// <summary>
        /// Gets or sets the character used where a header separator meets a column separator.
        /// </summary>
        char HeaderMiddleJoint { get; set; }

        /// <summary>
        /// Gets or sets the character used on the right edge of the table where a
        /// character separating the header row from the rows of values meets it.
        /// </summary>
        char HeaderRightEdgeJoint { get; set; }

        /// <summary>
        /// Gets or sets the character used for the left edge of the table.
        /// </summary>
        char LeftEdge { get; set; }

        /// <summary>
        /// Gets or sets the character used for the right edge of the table.
        /// </summary>
        char RightEdge { get; set; }

        /// <summary>
        /// Gets or sets the character used for the top edge of the table.
        /// </summary>
        char TopEdge { get; set; }

        /// <summary>
        /// Gets or sets the character used for the bottom edge of the table.
        /// </summary>
        char BottomEdge { get; set; }

        /// <summary>
        /// Gets or sets the character used for the top left corner of the table.
        /// </summary>
        char TopLeftCorner { get; set; }

        /// <summary>
        /// Gets or sets the character used for the top right corner of the table.
        /// </summary>
        char TopRightCorner { get; set; }

        /// <summary>
        /// Gets or sets the character used for the bottom right corner of the table.
        /// </summary>
        char BottomRightCorner { get; set; }

        /// <summary>
        /// Gets or sets the character used for the bottom left corner of the table.
        /// </summary>
        char BottomLeftCorner { get; set; }

        /// <summary>
        /// Gets or sets the character used where a column separator meets the top table edge.
        /// </summary>
        char TopEdgeJoint { get; set; }

        /// <summary>
        /// Gets or sets the character where a column separator meets the bottom table edge.
        /// </summary>
        char BottomEdgeJoint { get; set; }
    }

    /// <summary>
    /// Styling that only uses traditional ASCII characters to build the table.
    /// </summary>
    public class AsciiTableStyling : ITableStyling
    {
        /// <summary>
        /// Gets or sets the string used for padding the left side of a column.
        /// The contents of a cell will be aligned within the left and right padding.
        /// </summary>
        public string ColumnLeftPadding { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the string used for padding the right side of a column.
        /// The contents of a cell will be aligned within the left and right padding.
        /// </summary>
        public string ColumnRightPadding { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the character used for the column separator.
        /// </summary>
        public char ColumnSeparator { get; set; } = '|';

        /// <summary>
        /// Gets or sets the character used for separating rows of values.
        /// </summary>
        public char ValueRowSeparator { get; set; } = '-';

        /// <summary>
        /// Gets or sets the character used on the left edge of the table where a
        /// character separating rows of values meets it.
        /// </summary>
        public char ValueLeftEdgeJoint { get; set; } = '|';

        /// <summary>
        /// Gets or sets the character used where a row separator meets a column separator.
        /// </summary>
        public char ValueMiddleJoint { get; set; } = '+';

        /// <summary>
        /// Gets or sets the character used on the right edge of the table where a
        /// character separating rows of values meets it.
        /// </summary>
        public char ValueRightEdgeJoint { get; set; } = '|';

        /// <summary>
        /// Gets or sets the character used for separating the header row from the rows of values.
        /// </summary>
        public char HeaderRowSeparator { get; set; } = '-';

        /// <summary>
        /// Gets or sets the character used on the left edge of the table where a
        /// character separating the header row from the rows of values meets it.
        /// </summary>
        public char HeaderLeftEdgeJoint { get; set; } = '|';

        /// <summary>
        /// Gets or sets the character used where a header separator meets a column separator.
        /// </summary>
        public char HeaderMiddleJoint { get; set; } = '+';

        /// <summary>
        /// Gets or sets the character used on the right edge of the table where a
        /// character separating the header row from the rows of values meets it.
        /// </summary>
        public char HeaderRightEdgeJoint { get; set; } = '|';

        /// <summary>
        /// Gets or sets the character used for the left edge of the table.
        /// </summary>
        public char LeftEdge { get; set; } = '|';

        /// <summary>
        /// Gets or sets the character used for the right edge of the table.
        /// </summary>
        public char RightEdge { get; set; } = '|';

        /// <summary>
        /// Gets or sets the character used for the top edge of the table.
        /// </summary>
        public char TopEdge { get; set; } = '-';

        /// <summary>
        /// Gets or sets the character used for the bottom edge of the table.
        /// </summary>
        public char BottomEdge { get; set; } = '-';

        /// <summary>
        /// Gets or sets the character used for the top left corner of the table.
        /// </summary>
        public char TopLeftCorner { get; set; } = '-';

        /// <summary>
        /// Gets or sets the character used for the top right corner of the table.
        /// </summary>
        public char TopRightCorner { get; set; } = '-';

        /// <summary>
        /// Gets or sets the character used for the bottom right corner of the table.
        /// </summary>
        public char BottomRightCorner { get; set; } = '-';

        /// <summary>
        /// Gets or sets the character used for the bottom left corner of the table.
        /// </summary>
        public char BottomLeftCorner { get; set; } = '-';

        /// <summary>
        /// Gets or sets the character used where a column separator meets the top table edge.
        /// </summary>
        public char TopEdgeJoint { get; set; } = '-';

        /// <summary>
        /// Gets or sets the character used where a column separator meets the bottom table edge.
        /// </summary>
        public char BottomEdgeJoint { get; set; } = '-';
    }

    /// <summary>
    /// Styling that uses Unicode characters specifically designed to build tables.
    /// </summary>
    public class UnicodeTableStyling : ITableStyling
    {
        /// <summary>
        /// Gets or sets the string used for padding the left side of a column.
        /// The contents of a cell will be aligned within the left and right padding.
        /// </summary>
        public string ColumnLeftPadding { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the string used for padding the right side of a column.
        /// The contents of a cell will be aligned within the left and right padding.
        /// </summary>
        public string ColumnRightPadding { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the character used for the column separator.
        /// </summary>
        public char ColumnSeparator { get; set; } = '│';

        /// <summary>
        /// Gets or sets the character used for separating rows of values.
        /// </summary>
        public char ValueRowSeparator { get; set; } = '─';

        /// <summary>
        /// Gets or sets the character used on the left edge of the table where a
        /// character separating rows of values meets it.
        /// </summary>
        public char ValueLeftEdgeJoint { get; set; } = '╟';

        /// <summary>
        /// Gets or sets the character used where a row separator meets a column separator.
        /// </summary>
        public char ValueMiddleJoint { get; set; } = '┼';

        /// <summary>
        /// Gets or sets the character used on the right edge of the table where a
        /// character separating rows of values meets it.
        /// </summary>
        public char ValueRightEdgeJoint { get; set; } = '╢';

        /// <summary>
        /// Gets or sets the character used for separating the header row from the rows of values.
        /// </summary>
        public char HeaderRowSeparator { get; set; } = '═';

        /// <summary>
        /// Gets or sets the character used on the left edge of the table where a
        /// character separating the header row from the rows of values meets it.
        /// </summary>
        public char HeaderLeftEdgeJoint { get; set; } = '╠';

        /// <summary>
        /// Gets or sets the character used where a header separator meets a column separator.
        /// </summary>
        public char HeaderMiddleJoint { get; set; } = '╪';

        /// <summary>
        /// Gets or sets the character used on the right edge of the table where a
        /// character separating the header row from the rows of values meets it.
        /// </summary>
        public char HeaderRightEdgeJoint { get; set; } = '╣';

        /// <summary>
        /// Gets or sets the character used for the left edge of the table.
        /// </summary>
        public char LeftEdge { get; set; } = '║';

        /// <summary>
        /// Gets or sets the character used for the right edge of the table.
        /// </summary>
        public char RightEdge { get; set; } = '║';

        /// <summary>
        /// Gets or sets the character used for the top edge of the table.
        /// </summary>
        public char TopEdge { get; set; } = '═';

        /// <summary>
        /// Gets or sets the character used for the bottom edge of the table.
        /// </summary>
        public char BottomEdge { get; set; } = '═';

        /// <summary>
        /// Gets or sets the character used for the top left corner of the table.
        /// </summary>
        public char TopLeftCorner { get; set; } = '╔';

        /// <summary>
        /// Gets or sets the character used for the top right corner of the table.
        /// </summary>
        public char TopRightCorner { get; set; } = '╗';

        /// <summary>
        /// Gets or sets the character used for the bottom right corner of the table.
        /// </summary>
        public char BottomRightCorner { get; set; } = '╝';

        /// <summary>
        /// Gets or sets the character used for the bottom left corner of the table.
        /// </summary>
        public char BottomLeftCorner { get; set; } = '╚';

        /// <summary>
        /// Gets or sets the character used where a column separator meets the top table edge.
        /// </summary>
        public char TopEdgeJoint { get; set; } = '╤';

        /// <summary>
        /// Gets or sets the character used where a column separator meets the bottom table edge.
        /// </summary>
        public char BottomEdgeJoint { get; set; } = '╧';
    }
}
