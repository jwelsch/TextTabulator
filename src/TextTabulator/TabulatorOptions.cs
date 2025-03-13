using System;

namespace TextTabulator
{
    /// <summary>
    /// Interface for specifying TabulatorOptions.
    /// </summary>
    public interface ITabulatorOptions
    {
        /// <summary>
        /// Gets or sets the styling of the table.
        /// </summary>
        ITableStyling Styling { get; set; }

        /// <summary>
        /// Gets or sets the alignment of cells in the table.
        /// </summary>
        ICellAlignmentProvider CellAlignment { get; set; }

        /// <summary>
        /// Gets or sets the characters to use for new lines in the table.
        /// </summary>
        string NewLine { get; set; }
    }

    /// <summary>
    /// Used for specifying options used when constructing the table.
    /// </summary>
    public class TabulatorOptions : ITabulatorOptions
    {
        /// <summary>
        /// Gets or sets the styling of the table.
        /// </summary>
        public ITableStyling Styling { get; set; } = new AsciiTableStyling();

        /// <summary>
        /// Gets or sets the alignment of cells in the table.
        /// </summary>
        public ICellAlignmentProvider CellAlignment { get; set; } = new UniformAlignmentProvider(TextTabulator.CellAlignment.Left);

        /// <summary>
        /// Gets or sets the characters to use for new lines in the table.
        /// </summary>
        public string NewLine { get; set; } = Environment.NewLine;
    }
}
