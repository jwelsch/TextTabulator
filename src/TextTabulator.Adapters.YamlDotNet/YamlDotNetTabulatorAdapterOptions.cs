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
        /// Creates an object of type JsonTabulatorAdapterOptions.
        /// </summary>
        /// <param name="nodeNameTransform">Transform to apply to YAML node names. Passing null will cause the YAML node names to not be altered.</param>
        public YamlDotNetTabulatorAdapterOptions(INameTransform? propertyNameTransform = null)
        {
            NodeNameTransform = propertyNameTransform ?? new PassThruNameTransform();
        }
    }
}
