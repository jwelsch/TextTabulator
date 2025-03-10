using System.Collections.Generic;

namespace TextTabulator.Adapter
{
    public interface ITabulatorAdapter
    {
        IEnumerable<string>? GetHeaderStrings();

        IEnumerable<IEnumerable<string>> GetValueStrings();
    }
}
