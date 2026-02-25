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

            var columnB = sut.GetColumn("ColumnB");
            var columnC = sut.GetColumn("ColumnC");
            var columnA = sut.GetColumn("ColumnA");

            Assert.Equal("ColumnB", columnB.ColumnName);
            Assert.Equal("ColumnC", columnC.ColumnName);
            Assert.Equal("ColumnA", columnA.ColumnName);

            Assert.Equal(0, columnB.Index);
            Assert.Equal(1, columnC.Index);
            Assert.Equal(2, columnA.Index);

            var mappedColumnB = sut.GetMappedColumn("ColumnB");
            var mappedColumnC = sut.GetMappedColumn("ColumnC");
            var mappedColumnA = sut.GetMappedColumn("ColumnA");

            Assert.Equal(0, mappedColumnB.Index);
            Assert.Equal(1, mappedColumnC.Index);
            Assert.Equal(2, mappedColumnA.Index);

            Assert.Equal("ColumnB", mappedColumnB.ColumnName);
            Assert.Equal("ColumnC", mappedColumnC.ColumnName);
            Assert.Equal("ColumnA", mappedColumnA.ColumnName);

            Assert.Equal(new[] { "ColumnB", "ColumnC", "ColumnA" }, sut.GetSortedColumnNames());
            Assert.Equal(new[] { "ColumnB", "ColumnC", "ColumnA" }, sut.GetSortedMappedColumnNames());
        }

        [Fact]
        public void When_default_transform_and_ascending_sorter_are_used_then_column_names_are_mapped_correctly()
        {
            var columnNames = new List<string> { "ColumnB", "ColumnC", "ColumnA" };

            var sut = new ColumnNameMapper(columnNames, null, ColumnOrderSorter.Ascending);

            var columnA = sut.GetColumn("ColumnA");
            var columnB = sut.GetColumn("ColumnB");
            var columnC = sut.GetColumn("ColumnC");

            Assert.Equal("ColumnA", columnA.ColumnName);
            Assert.Equal("ColumnB", columnB.ColumnName);
            Assert.Equal("ColumnC", columnC.ColumnName);

            Assert.Equal(2, columnA.Index);
            Assert.Equal(0, columnB.Index);
            Assert.Equal(1, columnC.Index);

            var mappedColumnA = sut.GetMappedColumn("ColumnA");
            var mappedColumnB = sut.GetMappedColumn("ColumnB");
            var mappedColumnC = sut.GetMappedColumn("ColumnC");

            Assert.Equal(0, mappedColumnA.Index);
            Assert.Equal(1, mappedColumnB.Index);
            Assert.Equal(2, mappedColumnC.Index);

            Assert.Equal("ColumnA", mappedColumnA.ColumnName);
            Assert.Equal("ColumnB", mappedColumnB.ColumnName);
            Assert.Equal("ColumnC", mappedColumnC.ColumnName);

            Assert.Equal(new[] { "ColumnA", "ColumnB", "ColumnC" }, sut.GetSortedColumnNames());
            Assert.Equal(new[] { "ColumnA", "ColumnB", "ColumnC" }, sut.GetSortedMappedColumnNames());
        }

        [Fact]
        public void When_default_transform_and_descending_sorter_are_used_then_column_names_are_mapped_correctly()
        {
            var columnNames = new List<string> { "ColumnB", "ColumnC", "ColumnA" };

            var sut = new ColumnNameMapper(columnNames, null, ColumnOrderSorter.Descending);

            var columnC = sut.GetColumn("ColumnC");
            var columnB = sut.GetColumn("ColumnB");
            var columnA = sut.GetColumn("ColumnA");

            Assert.Equal("ColumnC", columnC.ColumnName);
            Assert.Equal("ColumnB", columnB.ColumnName);
            Assert.Equal("ColumnA", columnA.ColumnName);

            Assert.Equal(1, columnC.Index);
            Assert.Equal(0, columnB.Index);
            Assert.Equal(2, columnA.Index);

            var mappedColumnC = sut.GetMappedColumn("ColumnC");
            var mappedColumnB = sut.GetMappedColumn("ColumnB");
            var mappedColumnA = sut.GetMappedColumn("ColumnA");

            Assert.Equal(0, mappedColumnC.Index);
            Assert.Equal(1, mappedColumnB.Index);
            Assert.Equal(2, mappedColumnA.Index);

            Assert.Equal("ColumnC", mappedColumnC.ColumnName);
            Assert.Equal("ColumnB", mappedColumnB.ColumnName);
            Assert.Equal("ColumnA", mappedColumnA.ColumnName);

            Assert.Equal(new[] { "ColumnC", "ColumnB", "ColumnA" }, sut.GetSortedColumnNames());
            Assert.Equal(new[] { "ColumnC", "ColumnB", "ColumnA" }, sut.GetSortedMappedColumnNames());
        }

        [Fact]
        public void When_pascal_transform_and_default_sorter_are_used_then_column_names_are_mapped_correctly()
        {
            var columnNames = new List<string> { "ColumnB", "ColumnC", "ColumnA" };

            var sut = new ColumnNameMapper(columnNames, new PascalNameTransform());

            var columnB = sut.GetColumn("Column B");
            var columnC = sut.GetColumn("Column C");
            var columnA = sut.GetColumn("Column A");

            Assert.Equal(0, columnB.Index);
            Assert.Equal(1, columnC.Index);
            Assert.Equal(2, columnA.Index);

            Assert.Equal("ColumnB", columnB.ColumnName);
            Assert.Equal("ColumnC", columnC.ColumnName);
            Assert.Equal("ColumnA", columnA.ColumnName);

            var mappedColumnB = sut.GetMappedColumn("ColumnB");
            var mappedColumnC = sut.GetMappedColumn("ColumnC");
            var mappedColumnA = sut.GetMappedColumn("ColumnA");

            Assert.Equal(0, mappedColumnB.Index);
            Assert.Equal(1, mappedColumnC.Index);
            Assert.Equal(2, mappedColumnA.Index);

            Assert.Equal("Column B", mappedColumnB.ColumnName);
            Assert.Equal("Column C", mappedColumnC.ColumnName);
            Assert.Equal("Column A", mappedColumnA.ColumnName);
        }

        [Fact]
        public void When_pascal_transform_and_ascending_sorter_are_used_then_column_names_are_mapped_correctly()
        {
            var columnNames = new List<string> { "ColumnB", "ColumnC", "ColumnA" };

            var sut = new ColumnNameMapper(columnNames, new PascalNameTransform(), ColumnOrderSorter.Ascending);

            var columnA = sut.GetColumn("Column A");
            var columnB = sut.GetColumn("Column B");
            var columnC = sut.GetColumn("Column C");

            Assert.Equal(2, columnA.Index);
            Assert.Equal(0, columnB.Index);
            Assert.Equal(1, columnC.Index);

            Assert.Equal("ColumnA", columnA.ColumnName);
            Assert.Equal("ColumnB", columnB.ColumnName);
            Assert.Equal("ColumnC", columnC.ColumnName);

            var mappedColumnA = sut.GetMappedColumn("ColumnA");
            var mappedColumnB = sut.GetMappedColumn("ColumnB");
            var mappedColumnC = sut.GetMappedColumn("ColumnC");

            Assert.Equal(0, mappedColumnA.Index);
            Assert.Equal(1, mappedColumnB.Index);
            Assert.Equal(2, mappedColumnC.Index);

            Assert.Equal("Column A", mappedColumnA.ColumnName);
            Assert.Equal("Column B", mappedColumnB.ColumnName);
            Assert.Equal("Column C", mappedColumnC.ColumnName);

            Assert.Equal(new[] { "ColumnA", "ColumnB", "ColumnC" }, sut.GetSortedColumnNames());
            Assert.Equal(new[] { "Column A", "Column B", "Column C" }, sut.GetSortedMappedColumnNames());
        }
    }
}
