using CsvHelper;
using System.Globalization;
using TextTabulator.Adapters.CsvHelper;

namespace TextTabulator.Adapters.CsvHelperTests
{
    public class CsvHelperTabulatorAdapterTests
    {
        [Fact]
        public void When_csv_has_only_header_row_then_data_is_returned()
        {
            var rows = 0;
            var columns = 5;
            var hasHeaderRow = true;
            var csvData = CsvDataGenerator.GenerateCsv(columns, rows, hasHeaderRow);
            using var textReader = new StringReader(csvData.Csv);
            using var csvReader = new CsvReader(textReader, CultureInfo.InvariantCulture);

            var sut = new CsvHelperTabulatorAdapter(csvReader, hasHeaderRow);

            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.False(values.Any());
            Assert.NotNull(headers);
            Assert.Equal(columns, headers.Count());

            var i = 0;
            foreach (var h in headers)
            {
                Assert.Equal(csvData.Data[0][i++], h);
            }
        }

        [Fact]
        public void When_csv_has_only_one_row_then_data_is_returned()
        {
            var rows = 1;
            var columns = 5;
            var hasHeaderRow = false;
            var csvData = CsvDataGenerator.GenerateCsv(columns, rows, hasHeaderRow);
            using var textReader = new StringReader(csvData.Csv);
            using var csvReader = new CsvReader(textReader, CultureInfo.InvariantCulture);

            var sut = new CsvHelperTabulatorAdapter(csvReader, hasHeaderRow);

            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.Null(headers);
            Assert.Equal(rows, values.Count());

            var ri = hasHeaderRow ? 1 : 0;
            foreach (var r in values)
            {
                Assert.Equal(columns, r.Count());

                var ci = 0;
                foreach (var c in r)
                {
                    Assert.Equal(csvData.Data[ri][ci++], c);
                }

                ri++;
            }
        }

        [Fact]
        public void When_csv_has_multiple_rows_then_data_is_returned()
        {
            var rows = 5;
            var columns = 5;
            var hasHeaderRow = false;
            var csvData = CsvDataGenerator.GenerateCsv(columns, rows, hasHeaderRow);
            using var textReader = new StringReader(csvData.Csv);
            using var csvReader = new CsvReader(textReader, CultureInfo.InvariantCulture);

            var sut = new CsvHelperTabulatorAdapter(csvReader, hasHeaderRow);

            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.Null(headers);
            Assert.Equal(rows, values.Count());

            var ri = hasHeaderRow ? 1 : 0;
            foreach (var r in values)
            {
                Assert.Equal(columns, r.Count());

                var ci = 0;
                foreach (var c in r)
                {
                    Assert.Equal(csvData.Data[ri][ci++], c);
                }

                ri++;
            }
        }

        [Fact]
        public void When_csv_has_headers_and_one_row_then_data_is_returned()
        {
            var rows = 1;
            var columns = 5;
            var hasHeaderRow = true;
            var csvData = CsvDataGenerator.GenerateCsv(columns, rows, hasHeaderRow);
            using var textReader = new StringReader(csvData.Csv);
            using var csvReader = new CsvReader(textReader, CultureInfo.InvariantCulture);

            var sut = new CsvHelperTabulatorAdapter(csvReader, hasHeaderRow);

            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.NotNull(headers);
            Assert.Equal(rows, values.Count());

            var i = 0;
            foreach (var h in headers)
            {
                Assert.Equal(csvData.Data[0][i++], h);
            }

            var ri = hasHeaderRow ? 1 : 0;
            foreach (var r in values)
            {
                Assert.Equal(columns, r.Count());

                var ci = 0;
                foreach (var c in r)
                {
                    Assert.Equal(csvData.Data[ri][ci++], c);
                }

                ri++;
            }
        }

        [Fact]
        public void When_csv_has_headers_and_multiple_rows_then_data_is_returned()
        {
            var rows = 5;
            var columns = 5;
            var hasHeaderRow = true;
            var csvData = CsvDataGenerator.GenerateCsv(columns, rows, hasHeaderRow);
            using var textReader = new StringReader(csvData.Csv);
            using var csvReader = new CsvReader(textReader, CultureInfo.InvariantCulture);

            var sut = new CsvHelperTabulatorAdapter(csvReader, hasHeaderRow);

            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.NotNull(headers);
            Assert.Equal(rows, values.Count());

            var i = 0;
            foreach (var h in headers)
            {
                Assert.Equal(csvData.Data[0][i++], h);
            }

            var ri = hasHeaderRow ? 1 : 0;
            foreach (var r in values)
            {
                Assert.Equal(columns, r.Count());

                var ci = 0;
                foreach (var c in r)
                {
                    Assert.Equal(csvData.Data[ri][ci++], c);
                }

                ri++;
            }
        }
    }
}
