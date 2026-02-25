namespace TextTabulator.Adapters.CsvHelper
{
    /// <summary>
    /// Options to allow configuration of the CsvHelperTabulatorAdapter class.
    /// </summary>
    public class CsvHelperTabulatorAdapterOptions
    {
        /// <summary>
        /// Gets the transform to apply to CSV header names.
        /// </summary>
        public INameTransform HeaderNameTransform { get; }

        /// <summary>
        /// Gets whether or not the CSV data contains a header row. Defaults to true.
        /// </summary>
        public bool HasHeaderRow { get; } = true;

        /// <summary>
        /// Gets the sorter to use for the columns.
        /// </summary>
        public IColumnOrderSorter ColumnSorter { get; }

        /// <summary>
        /// Creates an object of type CsvHelperTabulatorAdapterOptions.
        /// </summary>
        /// <param name="headerNameTransform">Transform to apply to CSV header names. Passing null will cause the CSV header names to not be altered.</param>
        /// <param name="hasHeaderRow">True if the CSV data contains a header row, false if not. Defaults to true.</param>
        /// <param name="columnSorter">Specifies the sorter to use for the columns. Passing null will use DefaultColumnOrderSorter.</param>
        public CsvHelperTabulatorAdapterOptions(INameTransform? headerNameTransform = null, bool hasHeaderRow = true, IColumnOrderSorter? columnSorter = null)
        {
            HeaderNameTransform = headerNameTransform ?? new PassThruNameTransform();
            HasHeaderRow = hasHeaderRow;
            ColumnSorter = columnSorter ?? new DefaultColumnOrderSorter();
        }
    }
}
