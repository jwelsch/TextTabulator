namespace TextTabulator.Adapters.Generics
{
    public interface IDictionaryTabulatorAdapterOptions
    {
        /// <summary>
        /// Gets the transform to apply to header names.
        /// </summary>
        INameTransform HeaderNameTransform { get; }

        /// <summary>
        /// Gets the way by which the columns themselves are sorted.
        /// </summary>
        SortOrder ColumnSortOrder { get; }

        /// <summary>
        /// Gets the orientation of the axes used in the table. The default orientation is columns of keys and rows of values, but this can be swapped to have columns of values and rows of keys.
        /// </summary>
        AxesOrientation AxesOrientation { get; }
    }

    public class DictionaryTabulatorAdapterOptions : IDictionaryTabulatorAdapterOptions
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
        /// Gets the orientation of the axes used in the table. The default orientation is columns of keys and rows of values, but this can be swapped to have columns of values and rows of keys.
        /// </summary>
        public AxesOrientation AxesOrientation { get; }

        /// <summary>
        /// Creates an object of type DictionaryTabulatorAdapterOptions.
        /// </summary>
        /// <param name="headerNameTransform">The transform to apply to header names.</param>
        /// <param name="columnSortOrder">The way by which the columns themselves are sorted.</param>
        /// <param name="axesOrientation">The orientation of the axes used in the table.</param>
        public DictionaryTabulatorAdapterOptions(INameTransform? headerNameTransform = null, SortOrder columnSortOrder = SortOrder.Default, AxesOrientation axesOrientation = AxesOrientation.Default)
        {
            HeaderNameTransform = headerNameTransform ?? new PassThruNameTransform();
            ColumnSortOrder = columnSortOrder;
            AxesOrientation = axesOrientation;
        }
    }
}
