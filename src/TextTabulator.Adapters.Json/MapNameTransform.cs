using System.Collections.Generic;

namespace TextTabulator.Adapters.Json
{
    /// <summary>
    /// A name transform that maps an existing name to a new name.
    /// </summary>
    public class MapNameTransform : INameTransform
    {
        private readonly IDictionary<string, string> _map;

        /// <summary>
        /// Creates an object of type MapNameTransform.
        /// </summary>
        /// <param name="map">The mapping of the existing names to the new names.</param>
        public MapNameTransform(IDictionary<string, string> map)
        {
            _map = map;
        }

        /// <summary>
        /// Applies the transform to the name.
        /// </summary>
        /// <param name="name">Name upon which to apply the tranform.</param>
        /// <returns>The transformed name.</returns>
        public string Apply(string name) => _map.TryGetValue(name, out string transform) ? transform : name;
    }
}
