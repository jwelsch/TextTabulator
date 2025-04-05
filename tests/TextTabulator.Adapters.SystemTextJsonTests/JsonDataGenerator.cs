using System.Text;

namespace TextTabulator.Adapters.SystemTextJsonTests
{
    public interface IJsonDataGenerator
    {
        string GetJsonData(bool hasChildArray = false, bool hasChildObject = false, bool fieldsRandomOrder = false);
    }

    public class JsonDataGenerator : IJsonDataGenerator
    {
        public string GetJsonData(bool hasChildArray = false, bool hasChildObject = false, bool fieldsRandomOrder = false)
        {
            var sb = new StringBuilder();

            return sb.ToString();
        }
    }
}
