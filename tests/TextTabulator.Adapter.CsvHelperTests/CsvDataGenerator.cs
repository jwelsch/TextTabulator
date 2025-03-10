using System.Text;

namespace TextTabulator.Adapter.CsvHelperTests
{
    internal class CsvTestData
    {
        public string Csv { get; }

        public string[][] Data { get; }

        public CsvTestData(string csv, string[][] data)
        {
            Csv = csv;
            Data = data;
        }
    }

    internal class CsvRowTestData
    {
        public string CsvRow { get; }

        public string[] Data { get; }

        public CsvRowTestData(string csvRow, string[] data)
        {
            CsvRow = csvRow;
            Data = data;
        }
    }

    internal static class CsvDataGenerator
    {
        private static readonly char[] _characters =
        [
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
        ];

        private static string GenerateString(int minColumnLength, int maxLength)
        {
            var sb = new StringBuilder();
            var rand = new Random((int)DateTime.Now.Ticks);

            var length = rand.Next(minColumnLength, maxLength);

            for (var i = 0; i < length; i++)
            {
                sb.Append(_characters[rand.Next(_characters.Length)]);
            }

            return sb.ToString();
        }

        private static CsvRowTestData GenerateRow(int columns, int minColumnLength, int maxColumnLength)
        {
            var sb = new StringBuilder();
            var rowData = new List<string>();

            for (var i = 0; i < columns - 1; i++)
            {
                rowData.Add(GenerateString(minColumnLength, maxColumnLength));
                sb.Append(rowData[^1] + ',');
            }

            rowData.Add(GenerateString(minColumnLength, maxColumnLength));
            sb.AppendLine(rowData[^1]);

            return new CsvRowTestData(sb.ToString(), rowData.ToArray());
        }

        internal static CsvTestData GenerateCsv(int columns, int rows, bool includeHeaderRow)
        {
            const int minLength = 1;
            const int maxLength = 10;

            var sb = new StringBuilder();
            var data = new List<string[]>();

            if (includeHeaderRow)
            {
                var rowData = GenerateRow(columns, minLength, maxLength);
                sb.Append(rowData.CsvRow);
                data.Add(rowData.Data);
            }

            for (var i = 0; i < rows; i++)
            {
                var rowData = GenerateRow(columns, minLength, maxLength);
                sb.Append(rowData.CsvRow);
                data.Add(rowData.Data);
            }

            return new CsvTestData(sb.ToString(), data.ToArray());
        }
    }
}
