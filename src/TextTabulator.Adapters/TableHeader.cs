namespace TextTabulator.Adapters
{
    public class TableHeader
    {
        public string TransformedName { get; }

        public int Index { get; }

        public TableHeader(string transformedName, int index)
        {
            TransformedName = transformedName;
            Index = index;
        }
    }
}
