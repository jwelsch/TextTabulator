using System.Text.Json;

namespace TextTabulator.Adapters.Json
{
    /// <summary>
    /// Options to allow configuration of the JsonTabulatorAdapter class.
    /// </summary>
    public class JsonTabulatorAdapterOptions
    {
        /// <summary>
        /// Gets the transform to apply to JSON property names.
        /// </summary>
        public INameTransform PropertyNameTransform { get; }

        /// <summary>
        /// Gets options that define customized behavior of the Utf8JsonReader that differs from the JSON RFC (for example, how to handle comments or maximum depth allowed when reading). By default, the Utf8JsonReader follows the JSON RFC strictly; comments within the JSON are invalid, and the maximum depth is 64.
        /// </summary>
        public JsonReaderOptions JsonReaderOptions { get; }

        /// <summary>
        /// Creates an object of type JsonTabulatorAdapterOptions.
        /// </summary>
        /// <param name="propertyNameTransform">Transform to apply to JSON property names. Passing null will cause the JSON property names to not be altered.</param>
        /// <param name="jsonReaderOptions">Options that define customized behavior of the Utf8JsonReader that differs from the JSON RFC (for example, how to handle comments or maximum depth allowed when reading). By default, the Utf8JsonReader follows the JSON RFC strictly; comments within the JSON are invalid, and the maximum depth is 64.</param>
        public JsonTabulatorAdapterOptions(INameTransform? propertyNameTransform = null, JsonReaderOptions jsonReaderOptions = default)
        {
            PropertyNameTransform = propertyNameTransform ?? new PassThruNameTransform();
            JsonReaderOptions = jsonReaderOptions;
        }
    }
}
