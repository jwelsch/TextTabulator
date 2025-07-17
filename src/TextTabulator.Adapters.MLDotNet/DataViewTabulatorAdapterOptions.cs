namespace TextTabulator.Adapters.MLDotNet
{
    /// <summary>
    /// Options to allow configuration of the DataViewTabulatorAdapter class.
    /// </summary>
    public class DataViewTabulatorAdapterOptions
    {
        /// <summary>
        /// Gets the transform to apply to column names. Passing null will cause the column names to not be altered.
        /// </summary>
        public INameTransform ColumnNameTransform { get; }

        /// <summary>
        /// Gets the formatter to apply to cell values. Passing null will cause the cell values to use default formatting.
        /// </summary>
        public ITypeFormatter TypeFormatter { get; }

        /// <summary>
        /// Creates an object of type DataViewTabulatorAdapterOptions.
        /// </summary>
        /// <param name="columnNameTransform">Transform to apply to cell names. Passing null will cause the cell names to not be altered.</param>
        /// <param name="typeFormatter">Formatter to apply to cell values. Passing null will cause the cell values to use default formatting.</param>
        public DataViewTabulatorAdapterOptions(INameTransform? columnNameTransform = null, ITypeFormatter? typeFormatter = null)
        {
            ColumnNameTransform = columnNameTransform ?? new PassThruNameTransform();
            TypeFormatter = typeFormatter ?? new TypeFormatter();
        }
    }
}
