using System.Text.Json;

namespace TextTabulator.Adapters.Json
{
    public class JsonTabulatorAdapterOptions
    {
        private readonly IJsonPropertyNameTransform _jsonPropertyNameTransform;
        private readonly JsonReaderOptions _jsonReaderOptions;

        public JsonTabulatorAdapterOptions(IJsonPropertyNameTransform? jsonPropertyNameTransform = default, JsonReaderOptions jsonReaderOptions = default)
        {
            _jsonPropertyNameTransform = jsonPropertyNameTransform ?? new PassThruJsonPropertyNameTransform();
            _jsonReaderOptions = jsonReaderOptions;
        }
    }
}
