
namespace TextTabulator.Adapters.YamlDotNet
{
    public interface IYamlDotNetTabulatorAdapter : ITabulatorAdapter
    {
    }

    public class YamlDotNetTabulatorAdapter : IYamlDotNetTabulatorAdapter
    {
        public IEnumerable<string>? GetHeaderStrings()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IEnumerable<string>> GetValueStrings()
        {
            throw new NotImplementedException();
        }
    }
}
