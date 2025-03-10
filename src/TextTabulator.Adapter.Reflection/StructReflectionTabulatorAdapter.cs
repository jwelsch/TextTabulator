using System;
using System.Collections.Generic;

namespace TextTabulator.Adapter.Reflection
{
    public interface IStructReflectionTabulatorAdapter : IReflectionTabulatorAdapter
    {
    }

    public class StructReflectionTabulatorAdapter<TStruct> : IStructReflectionTabulatorAdapter where TStruct : struct
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
