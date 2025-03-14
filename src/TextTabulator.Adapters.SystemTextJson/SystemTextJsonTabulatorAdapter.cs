using System;
using System.Collections.Generic;

namespace TextTabulator.Adapters.SystemTextJson
{
    public interface ISystemTextJsonTabulatorAdapter : ITabulatorAdapter
    {
    }

    public class SystemTextJsonTabulatorAdapter : ISystemTextJsonTabulatorAdapter
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
