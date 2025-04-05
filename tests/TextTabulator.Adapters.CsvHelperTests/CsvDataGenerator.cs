using System.Text;
using TextTabulator.Testing;

namespace TextTabulator.Adapters.CsvHelperTests
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
        private static CsvRowTestData GenerateRow(int columns, int minColumnLength, int maxColumnLength)
        {
            var sb = new StringBuilder();
            var rowData = new List<string>();

            for (var i = 0; i < columns - 1; i++)
            {
                rowData.Add(DataGenerator.GetString(minColumnLength, maxColumnLength));
                sb.Append(rowData[^1] + ',');
            }

            rowData.Add(DataGenerator.GetString(minColumnLength, maxColumnLength));
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
