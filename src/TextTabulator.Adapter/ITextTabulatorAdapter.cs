using System.Collections.Generic;

namespace TextTabulator.Adapter
{
    public interface ITextTabulatorAdapter
    {
        IEnumerable<string>? GetHeaderStrings();

        IEnumerable<IEnumerable<string>> GetValueStrings();
    }
}
