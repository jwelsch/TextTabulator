namespace TextTabulator.Adapters.Generics
{
    /// <summary>
    /// Options to allow configuration of the ListTabulatorAdapter class.
    /// </summary>
    public class ListTabulatorAdapterOptions
    {
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
        /// <param name="headerNameTransform">The transform to apply to header names.</param>
        /// <param name="columnSortOrder">The way by which the columns themselves are sorted.</param>
        public ListTabulatorAdapterOptions(INameTransform? headerNameTransform = null, SortOrder columnSortOrder = SortOrder.Default)
        {
            HeaderNameTransform = headerNameTransform ?? new PassThruNameTransform();
            ColumnSortOrder = columnSortOrder;
        }
    }
}
