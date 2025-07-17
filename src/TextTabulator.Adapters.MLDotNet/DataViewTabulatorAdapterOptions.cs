namespace TextTabulator.Adapters.MLDotNet
{
    /// <summary>
    /// Options to allow configuration of the DataViewTabulatorAdapter class.
    /// </summary>
    public class DataViewTabulatorAdapterOptions
    {
        /// <summary>
        /// Gets the transform to apply to IDataView column names. Passing null will cause the IDataView column names to not be altered.
        /// </summary>
        public INameTransform ColumnNameTransform { get; }

        /// <summary>
        /// Gets the formatter to apply to IDataView column values. Passing null will cause the IDataView column values to use default formatting.
        /// </summary>
        public ITypeFormatter TypeFormatter { get; }

        /// <summary>
        /// Creates an object of type DataViewTabulatorAdapterOptions.
        /// </summary>
        /// <param name="columnNameTransform">Transform to apply to IDataView column names. Passing null will cause the IDataView column names to not be altered.</param>
        /// <param name="typeFormatter">Formatter to apply to IDataView column values. Passing null will cause the IDataView column values to use default formatting.</param>
        public DataViewTabulatorAdapterOptions(INameTransform? columnNameTransform = null, ITypeFormatter? typeFormatter = null)
        {
            ColumnNameTransform = columnNameTransform ?? new PassThruNameTransform();
            TypeFormatter = typeFormatter ?? new TypeFormatter();
        }
    }
}
