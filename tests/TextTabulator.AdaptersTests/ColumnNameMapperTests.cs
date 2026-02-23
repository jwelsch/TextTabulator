using TextTabulator.Adapters;

namespace TextTabulator.AdaptersTests
{
    public class ColumnNameMapperTests
    {
        [Fact]
        public void When_default_transform_and_default_sorter_are_used_then_column_names_are_mapped_correctly()
        {
            var columnNames = new List<string> { "ColumnB", "ColumnC", "ColumnA" };

            var sut = new ColumnNameMapper(columnNames);

            var columnB = sut.GetColumnName("ColumnB");
            var columnC = sut.GetColumnName("ColumnC");
            var columnA = sut.GetColumnName("ColumnA");

            Assert.Equal("ColumnB", columnB.ColumnName);
            Assert.Equal("ColumnC", columnC.ColumnName);
            Assert.Equal("ColumnA", columnA.ColumnName);

            Assert.Equal(0, columnB.SortedIndex);
            Assert.Equal(1, columnC.SortedIndex);
            Assert.Equal(2, columnA.SortedIndex);

            var mappedColumnB = sut.GetMappedColumnName("ColumnB");
            var mappedColumnC = sut.GetMappedColumnName("ColumnC");
            var mappedColumnA = sut.GetMappedColumnName("ColumnA");

            Assert.Equal(0, mappedColumnB.SortedIndex);
            Assert.Equal(1, mappedColumnC.SortedIndex);
            Assert.Equal(2, mappedColumnA.SortedIndex);

            Assert.Equal("ColumnB", mappedColumnB.ColumnName);
            Assert.Equal("ColumnC", mappedColumnC.ColumnName);
            Assert.Equal("ColumnA", mappedColumnA.ColumnName);
        }

        [Fact]
        public void When_default_transform_and_ascending_sorter_are_used_then_column_names_are_mapped_correctly()
        {
            var columnNames = new List<string> { "ColumnB", "ColumnC", "ColumnA" };

            var sut = new ColumnNameMapper(columnNames, null, ColumnOrderSorter.Ascending);

            var columnA = sut.GetColumnName("ColumnA");
            var columnB = sut.GetColumnName("ColumnB");
            var columnC = sut.GetColumnName("ColumnC");

            Assert.Equal("ColumnA", columnA.ColumnName);
            Assert.Equal("ColumnB", columnB.ColumnName);
            Assert.Equal("ColumnC", columnC.ColumnName);

            Assert.Equal(0, columnA.SortedIndex);
            Assert.Equal(1, columnB.SortedIndex);
            Assert.Equal(2, columnC.SortedIndex);

            var mappedColumnA = sut.GetMappedColumnName("ColumnA");
            var mappedColumnB = sut.GetMappedColumnName("ColumnB");
            var mappedColumnC = sut.GetMappedColumnName("ColumnC");

            Assert.Equal(0, mappedColumnA.SortedIndex);
            Assert.Equal(1, mappedColumnB.SortedIndex);
            Assert.Equal(2, mappedColumnC.SortedIndex);

            Assert.Equal("ColumnA", mappedColumnA.ColumnName);
            Assert.Equal("ColumnB", mappedColumnB.ColumnName);
            Assert.Equal("ColumnC", mappedColumnC.ColumnName);
        }

        [Fact]
        public void When_default_transform_and_descending_sorter_are_used_then_column_names_are_mapped_correctly()
        {
            var columnNames = new List<string> { "ColumnB", "ColumnC", "ColumnA" };

            var sut = new ColumnNameMapper(columnNames, null, ColumnOrderSorter.Descending);

            var columnC = sut.GetColumnName("ColumnC");
            var columnB = sut.GetColumnName("ColumnB");
            var columnA = sut.GetColumnName("ColumnA");

            Assert.Equal("ColumnC", columnC.ColumnName);
            Assert.Equal("ColumnB", columnB.ColumnName);
            Assert.Equal("ColumnA", columnA.ColumnName);

            Assert.Equal(0, columnC.SortedIndex);
            Assert.Equal(1, columnB.SortedIndex);
            Assert.Equal(2, columnA.SortedIndex);

            var mappedColumnC = sut.GetMappedColumnName("ColumnC");
            var mappedColumnB = sut.GetMappedColumnName("ColumnB");
            var mappedColumnA = sut.GetMappedColumnName("ColumnA");

            Assert.Equal(0, mappedColumnC.SortedIndex);
            Assert.Equal(1, mappedColumnB.SortedIndex);
            Assert.Equal(2, mappedColumnA.SortedIndex);

            Assert.Equal("ColumnC", mappedColumnC.ColumnName);
            Assert.Equal("ColumnB", mappedColumnB.ColumnName);
            Assert.Equal("ColumnA", mappedColumnA.ColumnName);
        }

        [Fact]
        public void When_pascal_transform_and_default_sorter_are_used_then_column_names_are_mapped_correctly()
        {
            var columnNames = new List<string> { "ColumnB", "ColumnC", "ColumnA" };

            var sut = new ColumnNameMapper(columnNames, new PascalNameTransform());

            var columnB = sut.GetColumnName("Column B");
            var columnC = sut.GetColumnName("Column C");
            var columnA = sut.GetColumnName("Column A");

            Assert.Equal(0, columnB.SortedIndex);
            Assert.Equal(1, columnC.SortedIndex);
            Assert.Equal(2, columnA.SortedIndex);

            Assert.Equal("ColumnB", columnB.ColumnName);
            Assert.Equal("ColumnC", columnC.ColumnName);
            Assert.Equal("ColumnA", columnA.ColumnName);

            var mappedColumnB = sut.GetMappedColumnName("ColumnB");
            var mappedColumnC = sut.GetMappedColumnName("ColumnC");
            var mappedColumnA = sut.GetMappedColumnName("ColumnA");

            Assert.Equal(0, mappedColumnB.SortedIndex);
            Assert.Equal(1, mappedColumnC.SortedIndex);
            Assert.Equal(2, mappedColumnA.SortedIndex);

            Assert.Equal("Column B", mappedColumnB.ColumnName);
            Assert.Equal("Column C", mappedColumnC.ColumnName);
            Assert.Equal("Column A", mappedColumnA.ColumnName);
        }

        [Fact]
        public void When_pascal_transform_and_ascending_sorter_are_used_then_column_names_are_mapped_correctly()
        {
            var columnNames = new List<string> { "ColumnB", "ColumnC", "ColumnA" };

            var sut = new ColumnNameMapper(columnNames, new PascalNameTransform(), ColumnOrderSorter.Ascending);

            var columnA = sut.GetColumnName("Column A");
            var columnB = sut.GetColumnName("Column B");
            var columnC = sut.GetColumnName("Column C");

            Assert.Equal(0, columnA.SortedIndex);
            Assert.Equal(1, columnB.SortedIndex);
            Assert.Equal(2, columnC.SortedIndex);

            Assert.Equal("ColumnA", columnA.ColumnName);
            Assert.Equal("ColumnB", columnB.ColumnName);
            Assert.Equal("ColumnC", columnC.ColumnName);

            var mappedColumnA = sut.GetMappedColumnName("ColumnA");
            var mappedColumnB = sut.GetMappedColumnName("ColumnB");
            var mappedColumnC = sut.GetMappedColumnName("ColumnC");

            Assert.Equal(0, mappedColumnA.SortedIndex);
            Assert.Equal(1, mappedColumnB.SortedIndex);
            Assert.Equal(2, mappedColumnC.SortedIndex);

            Assert.Equal("Column A", mappedColumnA.ColumnName);
            Assert.Equal("Column B", mappedColumnB.ColumnName);
            Assert.Equal("Column C", mappedColumnC.ColumnName);
        }
    }
}
