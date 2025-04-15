namespace TextTabulator.Adapters.Json
{
    /// <summary>
    /// Interface for defining a transform for a name.
    /// </summary>
    public interface INameTransform
    {
        /// <summary>
        /// Applies the transform to the name.
        /// </summary>
        /// <param name="name">Name upon which to apply the tranform.</param>
        /// <returns>The transformed name.</returns>
        string Apply(string name);
    }

    /// <summary>
    /// A name transform that does not alter the property name.
    /// </summary>
    public class PassThruNameTransform : INameTransform
    {
        /// <summary>
        /// Applies the transform to the name.
        /// </summary>
        /// <param name="name">Name upon which to apply the tranform.</param>
        /// <returns>The transformed name.</returns>
        public string Apply(string name) => name;
    }
}
