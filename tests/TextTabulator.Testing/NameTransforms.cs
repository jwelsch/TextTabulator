using TextTabulator.Adapters;

namespace TextTabulator.Testing
{

    public class UpperCaseNameTransform : INameTransform
    {
        public string Apply(string name) => name.ToUpperInvariant();
    }

    public class SuffixNameTransform : INameTransform
    {
        public string Apply(string name) => name + "_SUFFIX";
    }
}
