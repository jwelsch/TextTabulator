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
            var options = new CsvHelperTabulatorAdapterOptions(null, true);
            var csvData = CsvDataGenerator.GenerateCsv(columns, rows, options.HasHeaderRow);
            using var textReader = new StringReader(csvData.Csv);
            using var csvReader = new CsvReader(textReader, CultureInfo.InvariantCulture);

            var sut = new CsvHelperTabulatorAdapter(csvReader, options);

            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.Empty(values);
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
            var options = new CsvHelperTabulatorAdapterOptions(null, false);
            var csvData = CsvDataGenerator.GenerateCsv(columns, rows, options.HasHeaderRow);
            using var textReader = new StringReader(csvData.Csv);
            using var csvReader = new CsvReader(textReader, CultureInfo.InvariantCulture);

            var sut = new CsvHelperTabulatorAdapter(csvReader, options);

            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.Null(headers);
            Assert.Equal(rows, values.Count());

            var ri = options.HasHeaderRow ? 1 : 0;
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
            var options = new CsvHelperTabulatorAdapterOptions(null, false);
            var csvData = CsvDataGenerator.GenerateCsv(columns, rows, options.HasHeaderRow);
            using var textReader = new StringReader(csvData.Csv);
            using var csvReader = new CsvReader(textReader, CultureInfo.InvariantCulture);

            var sut = new CsvHelperTabulatorAdapter(csvReader, options);

            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.Null(headers);
            Assert.Equal(rows, values.Count());

            var ri = options.HasHeaderRow ? 1 : 0;
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
            var options = new CsvHelperTabulatorAdapterOptions(null, true);
            var csvData = CsvDataGenerator.GenerateCsv(columns, rows, options.HasHeaderRow);
            using var textReader = new StringReader(csvData.Csv);
            using var csvReader = new CsvReader(textReader, CultureInfo.InvariantCulture);

            var sut = new CsvHelperTabulatorAdapter(csvReader);

            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.NotNull(headers);
            Assert.Equal(rows, values.Count());

            var i = 0;
            foreach (var h in headers)
            {
                Assert.Equal(csvData.Data[0][i++], h);
            }

            var ri = options.HasHeaderRow ? 1 : 0;
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
            var options = new CsvHelperTabulatorAdapterOptions(null, true);
            var csvData = CsvDataGenerator.GenerateCsv(columns, rows, options.HasHeaderRow);
            using var textReader = new StringReader(csvData.Csv);
            using var csvReader = new CsvReader(textReader, CultureInfo.InvariantCulture);

            var sut = new CsvHelperTabulatorAdapter(csvReader);

            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.NotNull(headers);
            Assert.Equal(rows, values.Count());

            var i = 0;
            foreach (var h in headers)
            {
                Assert.Equal(csvData.Data[0][i++], h);
            }

            var ri = options.HasHeaderRow ? 1 : 0;
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
        public void When_name_transform_used_and_headers_exist_then_transformed_headers_returned()
        {
            var transform = new CamelNameTransform();
            var options = new CsvHelperTabulatorAdapterOptions(transform, true);

            var csvData =
"""
name,weight,diet,extinction
Tyrannosaurus Rex,6.7,Carnivore,66
""";

            using var textReader = new StringReader(csvData);
            using var csvReader = new CsvReader(textReader, CultureInfo.InvariantCulture);

            var sut = new CsvHelperTabulatorAdapter(csvReader, options);

            var headers = sut.GetHeaderStrings();

            Assert.NotNull(headers);
            Assert.Collection(headers,
                i => Assert.Equal("Name", i),
                i => Assert.Equal("Weight", i),
                i => Assert.Equal("Diet", i),
                i => Assert.Equal("Extinction", i)
            );
        }

        [Fact]
        public void When_name_transform_used_and_headers_do_not_exist_then_no_headers_returned()
        {
            var transform = new CamelNameTransform();
            var options = new CsvHelperTabulatorAdapterOptions(transform, false);

            var csvData =
"""
Tyrannosaurus Rex,6.7,Carnivore,66
""";

            using var textReader = new StringReader(csvData);
            using var csvReader = new CsvReader(textReader, CultureInfo.InvariantCulture);

            var sut = new CsvHelperTabulatorAdapter(csvReader, options);

            var headers = sut.GetHeaderStrings();

            Assert.Null(headers);
        }
    }
}
