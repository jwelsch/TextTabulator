using TextTabulator.Adapters;

namespace Runner
{
    public class EZCsvAdapter : ITabulatorAdapter
    {
        private readonly TextReader _reader;
        private readonly bool _hasHeaderRow;

        public EZCsvAdapter(TextReader reader, bool hasHeaderRow)
        {
            _reader = reader;
            _hasHeaderRow = hasHeaderRow;
        }

        public IEnumerable<string>? GetHeaderStrings()
        {
            if (!_hasHeaderRow)
            {
                return null;
            }

            var line = _reader.ReadLine();

            if (line == null)
            {
                return null;
            }

            return line.Split(',');
        }

        public IEnumerable<IEnumerable<string>> GetValueStrings()
        {
            var rows = new List<string[]>();

            while (true)
            {
                var line = _reader.ReadLine();

                if (line == null || line.Length == 0)
                {
                    break;
                }

                rows.Add(line.Split(','));
            }

            return rows;
        }
    }
}
