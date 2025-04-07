namespace TextTabulator.Adapters.Json
{
    /// <summary>
    /// Interface for defining a transform for a JSON property name.
    /// </summary>
    public interface IJsonPropertyNameTransform
    {
        /// <summary>
        /// Applies the transform to the property name.
        /// </summary>
        /// <param name="propertyName">Property name upon which to apply the tranform.</param>
        /// <returns>The transformed name.</returns>
        string Apply(string propertyName);
    }

    /// <summary>
    /// A JSON property name transform that does not alter the property name.
    /// </summary>
    public class PassThruJsonPropertyNameTransform : IJsonPropertyNameTransform
    {
        public string Apply(string propertyName) => propertyName;
    }
}
