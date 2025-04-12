using System.Collections.Generic;

namespace TextTabulator
{
    public interface ITableData
    {
        IRowData? Headers { get; }

        IReadOnlyList<IRowData>? ValueRows { get; }
    }

    public class TableData : ITableData
    {
        public IRowData? Headers { get; }

        public IReadOnlyList<IRowData>? ValueRows { get; }

        public TableData(IRowData? headers, IReadOnlyList<IRowData>? valueRows)
        {
            Headers = headers;
            ValueRows = valueRows;
        }
    }
}
