using System.Collections.Generic;

namespace TextTabulator.Adapter
{
    public interface ITextTabulatorAdapter
    {
        IEnumerable<string>? GetHeaderStrings();

        IEnumerable<IEnumerable<string>> GetValueStrings();

        IEnumerable<object>? GetHeaderObjects();

        IEnumerable<IEnumerable<object>> GetValueObjects();

        IEnumerable<CellValue>? GetHeaderCellValues();

        IEnumerable<IEnumerable<CellValue>> GetValueCellValues();
    }
}
