namespace TextTabulator.Adapters.MLDotNet
{
    /// <summary>
    /// Options to allow configuration of the DataViewTabulatorAdapter class.
    /// </summary>
    public class DataViewTabulatorAdapterOptions
    {
        /// <summary>
        /// Gets the transform to apply to IDataView column names.
        /// </summary>
        public INameTransform ColumnNameTransform { get; }

        /// <summary>
        /// Creates an object of type DataViewTabulatorAdapterOptions.
        /// </summary>
        /// <param name="columnNameTransform">Transform to apply to IDataView column names. Passing null will cause the IDataView column names to not be altered.</param>
        public DataViewTabulatorAdapterOptions(INameTransform? columnNameTransform = null)
        {
            ColumnNameTransform = columnNameTransform ?? new PassThruNameTransform();
        }
    }
}
