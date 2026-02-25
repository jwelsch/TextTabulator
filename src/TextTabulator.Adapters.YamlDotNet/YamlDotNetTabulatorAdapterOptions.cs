namespace TextTabulator.Adapters.YamlDotNet
{
    /// <summary>
    /// Options to allow configuration of the YamlDotNetTabulatorAdapter class.
    /// </summary>
    public class YamlDotNetTabulatorAdapterOptions
    {
        /// <summary>
        /// Gets the transform to apply to YAML node names.
        /// </summary>
        public INameTransform NodeNameTransform { get; }

        /// <summary>
        /// Gets the sorter to use for the headers.
        /// </summary>
        public IHeaderOrderSorter HeaderSorter { get; }

        /// <summary>
        /// Creates an object of type YamlDotNetTabulatorAdapterOptions.
        /// </summary>
        /// <param name="nodeNameTransform">Transform to apply to YAML node names. Passing null will cause the YAML node names to not be altered.</param>
        /// <param name="headerSorter">Sorter to use for the headers. Passing null will cause the headers to not be sorted.</param>
        public YamlDotNetTabulatorAdapterOptions(INameTransform? nodeNameTransform = null, IHeaderOrderSorter? headerSorter = null)
        {
            NodeNameTransform = nodeNameTransform ?? new PassThruNameTransform();
            HeaderSorter = headerSorter ?? HeaderOrderSorter.Default;
        }
    }
}
