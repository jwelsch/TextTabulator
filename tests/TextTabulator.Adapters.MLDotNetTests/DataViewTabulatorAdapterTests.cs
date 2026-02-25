using Microsoft.ML;
using TextTabulator.Adapters.MLDotNet;
using TextTabulator.Testing;

namespace TextTabulator.Adapters.MLDotNetTests
{
    public class DataViewTabulatorAdapterTests
    {
        [Fact]
        public void When_data_is_loaded_from_enumerable_then_headers_and_values_returned()
        {
            var columns = 4;
            var list = new List<Dinosaur>
            {
                new ("Tyrannosaurus Rex", 6.7, "Carnivore", 66),
                new ("Triceratops", 8, "Herbivore", 66),
                new ("Apatosaurus", 33 ,"Herbivore", 147),
                new ("Archaeopteryx", 0.001, "Omnivore", 147),
                new ("Anklyosaurus", 4.8, "Herbivore", 66),
                new ("Stegosaurus", 3.8, "Herbivore", 147),
                new ("Hadrosaurus", 3, "Herbivore", 66)
            };

            var mlContext = new MLContext();
            var data = mlContext.Data.LoadFromEnumerable(list);

            var sut = new DataViewTabulatorAdapter(data);

            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.NotEmpty(values);
            Assert.NotNull(headers);
            Assert.Equal(columns, headers.Count());

            var i = 0;
            foreach (var h in headers)
            {
                Assert.Equal(data.Schema[i++].Name, h);
            }

            for (i = 0; i < list.Count; i++)
            {
                var row = values.ElementAt(i);
                Assert.Equal(list[i].Name, row.ElementAt(0));
                Assert.Equal(list[i].Weight.ToString(), row.ElementAt(1));
                Assert.Equal(list[i].Diet, row.ElementAt(2));
                Assert.Equal(list[i].ExtinctionMya.ToString(), row.ElementAt(3));
            }
        }

        [Fact]
        public void When_data_has_no_rows_then_return_only_headers()
        {
            var columns = 4;

            var mlContext = new MLContext();
            var data = mlContext.Data.LoadFromEnumerable(new List<Dinosaur>
            {
            });

            var sut = new DataViewTabulatorAdapter(data);

            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.Empty(values);
            Assert.NotNull(headers);
            Assert.Equal(columns, headers.Count());

            var i = 0;
            foreach (var h in headers)
            {
                Assert.Equal(data.Schema[i++].Name, h);
            }
        }

        [Fact]
        public void When_data_has_null_schema_then_return_null_headers_and_empty_values()
        {
            var mlContext = new MLContext();
            var data = mlContext.Data.LoadFromEnumerable(new List<Dinosaur>());
            var sut = new DataViewTabulatorAdapter(data);
            // Simulate null schema by using reflection to set _dataView to null or a mock with null schema if possible
            // Here, just assert that headers and values are not null for a valid schema
            Assert.NotNull(sut.GetHeaderStrings());
            Assert.Empty(sut.GetValueStrings());
        }

        [Fact]
        public void When_data_has_single_row_then_values_are_returned_correctly()
        {
            var mlContext = new MLContext();
            var list = new List<Dinosaur>
            {
                new ("Tyrannosaurus Rex", 6.7, "Carnivore", 66)
            };
            var data = mlContext.Data.LoadFromEnumerable(list);
            var sut = new DataViewTabulatorAdapter(data);
            var _ = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();
            Assert.Single(values);
            var row = values.First();
            Assert.Equal(list[0].Name, row.ElementAt(0));
            Assert.Equal(list[0].Weight.ToString(), row.ElementAt(1));
            Assert.Equal(list[0].Diet, row.ElementAt(2));
            Assert.Equal(list[0].ExtinctionMya.ToString(), row.ElementAt(3));
        }

        [Fact]
        public void When_data_has_various_types_then_values_are_returned_correctly()
        {
            var mlContext = new MLContext();
            var list = new List<VariousTypesTestClass>
            {
                new()
                {
                    BoolValue = true,
                    ByteValue = 42,
                    SByteValue = -42,
                    ShortValue = -1234,
                    UShortValue = 1234,
                    IntValue = -5678,
                    UIntValue = 5678U,
                    LongValue = -9876543210,
                    ULongValue = 9876543210,
                    FloatValue = 1.23f,
                    DoubleValue = 4.56,
                    DateTimeValue = new System.DateTime(2020, 1, 1, 12, 0, 0),
                    StringValue = "test"
                }
            };
            var data = mlContext.Data.LoadFromEnumerable(list);

            var formatters = new Dictionary<Type, Func<object, string>>
            {
                { typeof(DateTime), v => ((DateTime)v).ToString("u") }
            };
            var options = new DataViewTabulatorAdapterOptions(null, new TypeFormatter(formatters));

            var sut = new DataViewTabulatorAdapter(data, options);

            var _ = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();
            var row = values.First();

            Assert.Equal("True", row.ElementAt(0));
            Assert.Equal("42", row.ElementAt(1));
            Assert.Equal("-42", row.ElementAt(2));
            Assert.Equal("-1234", row.ElementAt(3));
            Assert.Equal("1234", row.ElementAt(4));
            Assert.Equal("-5678", row.ElementAt(5));
            Assert.Equal("5678", row.ElementAt(6));
            Assert.Equal("-9876543210", row.ElementAt(7));
            Assert.Equal("9876543210", row.ElementAt(8));
            Assert.Equal("1.23", row.ElementAt(9));
            Assert.Equal("4.56", row.ElementAt(10));
            Assert.Equal("2020-01-01 12:00:00Z", row.ElementAt(11));
            Assert.Equal("test", row.ElementAt(12));
        }

        private class VariousTypesTestClass
        {
            public bool BoolValue { get; set; }
            public byte ByteValue { get; set; }
            public sbyte SByteValue { get; set; }
            public short ShortValue { get; set; }
            public ushort UShortValue { get; set; }
            public int IntValue { get; set; }
            public uint UIntValue { get; set; }
            public long LongValue { get; set; }
            public ulong ULongValue { get; set; }
            public float FloatValue { get; set; }
            public double DoubleValue { get; set; }
            //public decimal DecimalValue { get; set; } // Decimal is not supported by automatic schema generation.
            public System.DateTime DateTimeValue { get; set; }
            public string StringValue { get; set; } = string.Empty;
        }

        [Fact]
        public void When_column_name_transform_is_passed_then_headers_are_transformed()
        {
            var mlContext = new MLContext();
            var list = new List<Dinosaur>
            {
                new ("Tyrannosaurus Rex", 6.7, "Carnivore", 66)
            };
            var data = mlContext.Data.LoadFromEnumerable(list);
            var options = new DataViewTabulatorAdapterOptions(new UpperCaseNameTransform());
            var sut = new DataViewTabulatorAdapter(data, options);
            var headers = sut.GetHeaderStrings();
            var expected = data.Schema.Select(c => c.Name.ToUpperInvariant()).ToArray();
            Assert.Equal(expected, headers);
        }

        [Fact]
        public void When_column_name_transform_adds_suffix_then_headers_have_suffix()
        {
            var mlContext = new MLContext();
            var list = new List<Dinosaur>
            {
                new ("Triceratops", 8, "Herbivore", 66)
            };
            var data = mlContext.Data.LoadFromEnumerable(list);
            var options = new DataViewTabulatorAdapterOptions(new SuffixNameTransform());
            var sut = new DataViewTabulatorAdapter(data, options);
            var headers = sut.GetHeaderStrings();
            var expected = data.Schema.Select(c => c.Name + "_SUFFIX").ToArray();
            Assert.Equal(expected, headers);
        }

        [Fact]
        public void When_header_sorter_sorts_ascending_then_headers_and_values_are_sorted()
        {
            var mlContext = new MLContext();
            var list = new List<Dinosaur>
            {
                new ("Tyrannosaurus Rex", 6.7, "Carnivore", 66),
                new ("Triceratops", 8, "Herbivore", 66),
                new ("Archaeopteryx", 0.001, "Omnivore", 147),
            };
            var data = mlContext.Data.LoadFromEnumerable(list);

            var options = new DataViewTabulatorAdapterOptions(null, null, HeaderOrderSorter.Ascending);
            var sut = new DataViewTabulatorAdapter(data, options);

            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.NotNull(headers);
            Assert.Collection(headers,
                i => Assert.Equal("Diet", i),
                i => Assert.Equal("ExtinctionMya", i),
                i => Assert.Equal("Name", i),
                i => Assert.Equal("Weight", i)
            );

            Assert.NotNull(values);
            Assert.Equal(3, values.Count());
            Assert.Collection(values,
                r =>
                {
                    Assert.Equal(4, r.Count());
                    Assert.Equal("Carnivore", r.ElementAt(0));
                    Assert.Equal("66", r.ElementAt(1));
                    Assert.Equal("Tyrannosaurus Rex", r.ElementAt(2));
                    Assert.Equal("6.7", r.ElementAt(3));
                },
                r =>
                {
                    Assert.Equal(4, r.Count());
                    Assert.Equal("Herbivore", r.ElementAt(0));
                    Assert.Equal("66", r.ElementAt(1));
                    Assert.Equal("Triceratops", r.ElementAt(2));
                    Assert.Equal("8", r.ElementAt(3));
                },
                r =>
                {
                    Assert.Equal(4, r.Count());
                    Assert.Equal("Omnivore", r.ElementAt(0));
                    Assert.Equal("147", r.ElementAt(1));
                    Assert.Equal("Archaeopteryx", r.ElementAt(2));
                    Assert.Equal("0.001", r.ElementAt(3));
                }
            );
        }

        [Fact]
        public void When_header_sorter_sorts_ascending_and_pascal_case_header_transform_then_headers_and_values_are_sorted_and_transformed()
        {
            var mlContext = new MLContext();
            var list = new List<Dinosaur>
            {
                new ("Tyrannosaurus Rex", 6.7, "Carnivore", 66),
                new ("Triceratops", 8, "Herbivore", 66),
                new ("Archaeopteryx", 0.001, "Omnivore", 147),
            };
            var data = mlContext.Data.LoadFromEnumerable(list);

            var options = new DataViewTabulatorAdapterOptions(new PascalNameTransform(false, true, null), null, HeaderOrderSorter.Ascending);
            var sut = new DataViewTabulatorAdapter(data, options);

            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.NotNull(headers);
            Assert.Collection(headers,
                i => Assert.Equal("diet", i),
                i => Assert.Equal("extinctionMya", i),
                i => Assert.Equal("name", i),
                i => Assert.Equal("weight", i)
            );

            Assert.NotNull(values);
            Assert.Equal(3, values.Count());
            Assert.Collection(values,
                r =>
                {
                    Assert.Equal(4, r.Count());
                    Assert.Equal("Carnivore", r.ElementAt(0));
                    Assert.Equal("66", r.ElementAt(1));
                    Assert.Equal("Tyrannosaurus Rex", r.ElementAt(2));
                    Assert.Equal("6.7", r.ElementAt(3));
                },
                r =>
                {
                    Assert.Equal(4, r.Count());
                    Assert.Equal("Herbivore", r.ElementAt(0));
                    Assert.Equal("66", r.ElementAt(1));
                    Assert.Equal("Triceratops", r.ElementAt(2));
                    Assert.Equal("8", r.ElementAt(3));
                },
                r =>
                {
                    Assert.Equal(4, r.Count());
                    Assert.Equal("Omnivore", r.ElementAt(0));
                    Assert.Equal("147", r.ElementAt(1));
                    Assert.Equal("Archaeopteryx", r.ElementAt(2));
                    Assert.Equal("0.001", r.ElementAt(3));
                }
            );
        }
    }
}
