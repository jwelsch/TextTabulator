using System.Collections.Generic;

namespace TextTabulator.Adapters.Json
{
    /// <summary>
    /// A JSON property name transform that maps a property name in JSON to a new name.
    /// </summary>
    public class MapJsonPropertyNameTransform : IJsonPropertyNameTransform
    {
        private readonly IDictionary<string, string> _map;

        /// <summary>
        /// Creates an object of type MapJsonPropertyNameTransform.
        /// </summary>
        /// <param name="map">The mapping of the property names in JSON to the new names.</param>
        public MapJsonPropertyNameTransform(IDictionary<string, string> map)
        {
            _map = map;
        }

        /// <summary>
        /// Applies the transform to the property name.
        /// </summary>
        /// <param name="propertyName">Property name upon which to apply the tranform.</param>
        /// <returns>The transformed name, or propertyName if a matching mapping was not found.</returns>
        public string Apply(string propertyName) => _map.TryGetValue(propertyName, out string transform) ? transform : propertyName;
    }
}
