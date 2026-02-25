using TextTabulator.Adapters;

namespace TextTabulator.AdaptersTests
{
    public class NameMapperTests
    {
        [Fact]
        public void When_default_transform_and_default_sorter_are_used_then_column_names_are_mapped_correctly()
        {
            var columnNames = new List<string> { "ColumnB", "ColumnC", "ColumnA" };

            var sut = new TableHeaderMapper(columnNames);

            var columnB = sut.GetHeader("ColumnB");
            var columnC = sut.GetHeader("ColumnC");
            var columnA = sut.GetHeader("ColumnA");

            Assert.Equal("ColumnB", columnB.Name);
            Assert.Equal("ColumnC", columnC.Name);
            Assert.Equal("ColumnA", columnA.Name);

            Assert.Equal(0, columnB.Index);
            Assert.Equal(1, columnC.Index);
            Assert.Equal(2, columnA.Index);

            var mappedColumnB = sut.GetMappedHeader("ColumnB");
            var mappedColumnC = sut.GetMappedHeader("ColumnC");
            var mappedColumnA = sut.GetMappedHeader("ColumnA");

            Assert.Equal(0, mappedColumnB.Index);
            Assert.Equal(1, mappedColumnC.Index);
            Assert.Equal(2, mappedColumnA.Index);

            Assert.Equal("ColumnB", mappedColumnB.Name);
            Assert.Equal("ColumnC", mappedColumnC.Name);
            Assert.Equal("ColumnA", mappedColumnA.Name);

            Assert.Equal(new[] { "ColumnB", "ColumnC", "ColumnA" }, sut.GetSortedHeaderNames());
            Assert.Equal(new[] { "ColumnB", "ColumnC", "ColumnA" }, sut.GetSortedMappedHeaderNames());
        }

        [Fact]
        public void When_default_transform_and_ascending_sorter_are_used_then_column_names_are_mapped_correctly()
        {
            var columnNames = new List<string> { "ColumnB", "ColumnC", "ColumnA" };

            var sut = new TableHeaderMapper(columnNames, null, HeaderOrderSorter.Ascending);

            var columnA = sut.GetHeader("ColumnA");
            var columnB = sut.GetHeader("ColumnB");
            var columnC = sut.GetHeader("ColumnC");

            Assert.Equal("ColumnA", columnA.Name);
            Assert.Equal("ColumnB", columnB.Name);
            Assert.Equal("ColumnC", columnC.Name);

            Assert.Equal(2, columnA.Index);
            Assert.Equal(0, columnB.Index);
            Assert.Equal(1, columnC.Index);

            var mappedColumnA = sut.GetMappedHeader("ColumnA");
            var mappedColumnB = sut.GetMappedHeader("ColumnB");
            var mappedColumnC = sut.GetMappedHeader("ColumnC");

            Assert.Equal(0, mappedColumnA.Index);
            Assert.Equal(1, mappedColumnB.Index);
            Assert.Equal(2, mappedColumnC.Index);

            Assert.Equal("ColumnA", mappedColumnA.Name);
            Assert.Equal("ColumnB", mappedColumnB.Name);
            Assert.Equal("ColumnC", mappedColumnC.Name);

            Assert.Equal(new[] { "ColumnA", "ColumnB", "ColumnC" }, sut.GetSortedHeaderNames());
            Assert.Equal(new[] { "ColumnA", "ColumnB", "ColumnC" }, sut.GetSortedMappedHeaderNames());
        }

        [Fact]
        public void When_default_transform_and_descending_sorter_are_used_then_column_names_are_mapped_correctly()
        {
            var columnNames = new List<string> { "ColumnB", "ColumnC", "ColumnA" };

            var sut = new TableHeaderMapper(columnNames, null, HeaderOrderSorter.Descending);

            var columnC = sut.GetHeader("ColumnC");
            var columnB = sut.GetHeader("ColumnB");
            var columnA = sut.GetHeader("ColumnA");

            Assert.Equal("ColumnC", columnC.Name);
            Assert.Equal("ColumnB", columnB.Name);
            Assert.Equal("ColumnA", columnA.Name);

            Assert.Equal(1, columnC.Index);
            Assert.Equal(0, columnB.Index);
            Assert.Equal(2, columnA.Index);

            var mappedColumnC = sut.GetMappedHeader("ColumnC");
            var mappedColumnB = sut.GetMappedHeader("ColumnB");
            var mappedColumnA = sut.GetMappedHeader("ColumnA");

            Assert.Equal(0, mappedColumnC.Index);
            Assert.Equal(1, mappedColumnB.Index);
            Assert.Equal(2, mappedColumnA.Index);

            Assert.Equal("ColumnC", mappedColumnC.Name);
            Assert.Equal("ColumnB", mappedColumnB.Name);
            Assert.Equal("ColumnA", mappedColumnA.Name);

            Assert.Equal(new[] { "ColumnC", "ColumnB", "ColumnA" }, sut.GetSortedHeaderNames());
            Assert.Equal(new[] { "ColumnC", "ColumnB", "ColumnA" }, sut.GetSortedMappedHeaderNames());
        }

        [Fact]
        public void When_pascal_transform_and_default_sorter_are_used_then_column_names_are_mapped_correctly()
        {
            var columnNames = new List<string> { "ColumnB", "ColumnC", "ColumnA" };

            var sut = new TableHeaderMapper(columnNames, new PascalNameTransform());

            var columnB = sut.GetHeader("Column B");
            var columnC = sut.GetHeader("Column C");
            var columnA = sut.GetHeader("Column A");

            Assert.Equal(0, columnB.Index);
            Assert.Equal(1, columnC.Index);
            Assert.Equal(2, columnA.Index);

            Assert.Equal("ColumnB", columnB.Name);
            Assert.Equal("ColumnC", columnC.Name);
            Assert.Equal("ColumnA", columnA.Name);

            var mappedColumnB = sut.GetMappedHeader("ColumnB");
            var mappedColumnC = sut.GetMappedHeader("ColumnC");
            var mappedColumnA = sut.GetMappedHeader("ColumnA");

            Assert.Equal(0, mappedColumnB.Index);
            Assert.Equal(1, mappedColumnC.Index);
            Assert.Equal(2, mappedColumnA.Index);

            Assert.Equal("Column B", mappedColumnB.Name);
            Assert.Equal("Column C", mappedColumnC.Name);
            Assert.Equal("Column A", mappedColumnA.Name);
        }

        [Fact]
        public void When_pascal_transform_and_ascending_sorter_are_used_then_column_names_are_mapped_correctly()
        {
            var columnNames = new List<string> { "ColumnB", "ColumnC", "ColumnA" };

            var sut = new TableHeaderMapper(columnNames, new PascalNameTransform(), HeaderOrderSorter.Ascending);

            var columnA = sut.GetHeader("Column A");
            var columnB = sut.GetHeader("Column B");
            var columnC = sut.GetHeader("Column C");

            Assert.Equal(2, columnA.Index);
            Assert.Equal(0, columnB.Index);
            Assert.Equal(1, columnC.Index);

            Assert.Equal("ColumnA", columnA.Name);
            Assert.Equal("ColumnB", columnB.Name);
            Assert.Equal("ColumnC", columnC.Name);

            var mappedColumnA = sut.GetMappedHeader("ColumnA");
            var mappedColumnB = sut.GetMappedHeader("ColumnB");
            var mappedColumnC = sut.GetMappedHeader("ColumnC");

            Assert.Equal(0, mappedColumnA.Index);
            Assert.Equal(1, mappedColumnB.Index);
            Assert.Equal(2, mappedColumnC.Index);

            Assert.Equal("Column A", mappedColumnA.Name);
            Assert.Equal("Column B", mappedColumnB.Name);
            Assert.Equal("Column C", mappedColumnC.Name);

            Assert.Equal(new[] { "ColumnA", "ColumnB", "ColumnC" }, sut.GetSortedHeaderNames());
            Assert.Equal(new[] { "Column A", "Column B", "Column C" }, sut.GetSortedMappedHeaderNames());
        }
    }
}
