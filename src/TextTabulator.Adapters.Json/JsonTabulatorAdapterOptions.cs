using System.Text.Json;

namespace TextTabulator.Adapters.Json
{
    public class JsonTabulatorAdapterOptions
    {
        public IJsonPropertyNameTransform JsonPropertyNameTransform { get; }

        public JsonReaderOptions JsonReaderOptions { get; }

        public JsonTabulatorAdapterOptions(IJsonPropertyNameTransform? jsonPropertyNameTransform = null, JsonReaderOptions jsonReaderOptions = default)
        {
            JsonPropertyNameTransform = jsonPropertyNameTransform ?? new PassThruJsonPropertyNameTransform();
            JsonReaderOptions = jsonReaderOptions;
        }
    }
}
