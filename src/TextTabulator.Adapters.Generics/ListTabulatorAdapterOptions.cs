namespace TextTabulator.Adapters.Generics
{
    /// <summary>
    /// Options to allow configuration of the ListTabulatorAdapter class.
    /// </summary>
    public class ListTabulatorAdapterOptions
    {
        /// <summary>
        /// Gets whether or not to include an index column as the first column in the output.
        /// </summary>
        public bool IncludeIndex { get; }

        /// <summary>
        /// Gets the transform to apply to header names.
        /// </summary>
        public INameTransform HeaderNameTransform { get; }

        /// <summary>
        /// Gets the way by which the columns themselves are sorted.
        /// </summary>
        public SortOrder ColumnSortOrder { get; }

        /// <summary>
        /// Creates an object of type ListTabulatorAdapterOptions.
        /// </summary>
        /// <param name="includeIndex">Whether or not to include an index column as the first column in the output.</param>
        /// <param name="headerNameTransform">The transform to apply to header names.</param>
        /// <param name="columnSortOrder">The way by which the columns themselves are sorted.</param>
        public ListTabulatorAdapterOptions(bool includeIndex = true, INameTransform? headerNameTransform = null, SortOrder columnSortOrder = SortOrder.Default)
        {
            IncludeIndex = includeIndex;
            HeaderNameTransform = headerNameTransform ?? new PassThruNameTransform();
            ColumnSortOrder = columnSortOrder;
        }
    }
}
