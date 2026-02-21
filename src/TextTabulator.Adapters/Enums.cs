namespace TextTabulator.Adapters
{
    /// <summary>
    /// The order by which columns are sorted.
    /// </summary>
    public enum SortOrder
    {
        /// <summary>
        /// The default order, which is the order in which the columns are encountered in the data. For example, for a dictionary, this would be the order in which the keys are enumerated.
        /// </summary>
        Default,

        /// <summary>
        /// Columns are sorted in ascending alphanumeric order.
        /// </summary>
        AlphaNumericAscending,

        /// <summary>
        /// Columns are sorted in descending alphanumeric order.
        /// </summary>
        AlphaNumericDescending,
    }

    /// <summary>
    /// Specifies the orientation of axes in the table.
    /// </summary>
    public enum AxesOrientation
    {
        /// <summary>
        /// The default horizontal and vertical axes orientation for a particular adapter.
        /// </summary>
        Default,

        /// <summary>
        /// Swaps the horizontal and vertical axes orientation for a particular adapter.
        /// </summary>
        Swapped
    }
}
