using System.Collections.Generic;

namespace TextTabulator
{
    public interface ITableData
    {
        IRowData? Headers { get; }

        IReadOnlyList<IRowData>? ValueRows { get; }

        IReadOnlyList<int> MaxColumnWidths { get; }
    }

    public class TableData : ITableData
    {
        public IRowData? Headers { get; }

        public IReadOnlyList<IRowData>? ValueRows { get; }

        public IReadOnlyList<int> MaxColumnWidths { get; }

        public TableData(IRowData? headers, IReadOnlyList<IRowData>? valueRows, IReadOnlyList<int> maxColumnWidths)
        {
            Headers = headers;
            ValueRows = valueRows;
            MaxColumnWidths = maxColumnWidths;
        }
    }
}
