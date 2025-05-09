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

        /// <summary>
        /// Gets or sets whether to include non-printable characters in the generated table. Setting false will cause any
        /// non-printable characters to be removed. Setting true will cause any non-printable characters to be left in the table.
        /// </summary>
        bool IncludeNonPrintableCharacters { get; set; }
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

        /// <summary>
        /// Gets or sets whether to include non-printable characters in the generated table. Setting false will cause any
        /// non-printable characters to be removed. Setting true will cause any non-printable characters to be left in the table.
        /// </summary>
        public bool IncludeNonPrintableCharacters { get; set; }
    }
}
