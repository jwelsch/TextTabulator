using TextTabulator.Adapters;

namespace TextTabulator.AdaptersTests
{
    public class ColumnOrderSorterTests
    {
        [Theory]
        [InlineData(new[] { "C", "Z", "A", "M", "B" }, new[] { "C", "Z", "A", "M", "B" })]
        [InlineData(new[] { "A", "B", "C", "M", "Z" }, new[] { "A", "B", "C", "M", "Z" })]
        [InlineData(new[] { "Z", "M", "C", "B", "A" }, new[] { "Z", "M", "C", "B", "A" })]
        [InlineData(new[] { "C", "C", "A", "B", "B" }, new[] { "C", "C", "A", "B", "B" })]
        [InlineData(new[] { "1", "C", "A", "0", "B" }, new[] { "1", "C", "A", "0", "B" })]
        public void When_sort_order_default_then_return_unsorted_names(IEnumerable<string> data, string[] expected)
        {
            var sut = new DefaultColumnOrderSorter();

            var result = sut.Sort(data, i => i);

            Assert.Collection(result,
                i => Assert.Equal(expected[0], i),
                i => Assert.Equal(expected[1], i),
                i => Assert.Equal(expected[2], i),
                i => Assert.Equal(expected[3], i),
                i => Assert.Equal(expected[4], i)
            );
        }

        [Theory]
        [InlineData(new[] { "C", "Z", "A", "M", "B" }, new[] { "A", "B", "C", "M", "Z" })]
        [InlineData(new[] { "A", "B", "C", "M", "Z" }, new[] { "A", "B", "C", "M", "Z" })]
        [InlineData(new[] { "Z", "M", "C", "B", "A" }, new[] { "A", "B", "C", "M", "Z" })]
        [InlineData(new[] { "C", "C", "A", "B", "B" }, new[] { "A", "B", "B", "C", "C" })]
        [InlineData(new[] { "1", "C", "A", "0", "B" }, new[] { "0", "1", "A", "B", "C" })]
        public void When_sort_order_ascending_then_return_sorted_names(IEnumerable<string> data, string[] expected)
        {
            var sut = new AscendingColumnOrderSorter();

            var result = sut.Sort(data, i => i);

            Assert.Collection(result,
                i => Assert.Equal(expected[0], i),
                i => Assert.Equal(expected[1], i),
                i => Assert.Equal(expected[2], i),
                i => Assert.Equal(expected[3], i),
                i => Assert.Equal(expected[4], i)
            );
        }

        [Theory]
        [InlineData(new[] { "C", "Z", "A", "M", "B" }, new[] { "Z", "M", "C", "B", "A" })]
        [InlineData(new[] { "A", "B", "C", "M", "Z" }, new[] { "Z", "M", "C", "B", "A" })]
        [InlineData(new[] { "Z", "M", "C", "B", "A" }, new[] { "Z", "M", "C", "B", "A" })]
        [InlineData(new[] { "C", "C", "A", "B", "B" }, new[] { "C", "C", "B", "B", "A" })]
        [InlineData(new[] { "1", "C", "A", "0", "B" }, new[] { "C", "B", "A", "1", "0" })]
        public void When_sort_order_descending_then_return_sorted_names(IEnumerable<string> data, string[] expected)
        {
            var sut = new DescendingColumnOrderSorter();

            var result = sut.Sort(data, i => i);

            Assert.Collection(result,
                i => Assert.Equal(expected[0], i),
                i => Assert.Equal(expected[1], i),
                i => Assert.Equal(expected[2], i),
                i => Assert.Equal(expected[3], i),
                i => Assert.Equal(expected[4], i)
            );
        }

        [Fact]
        public void When_sort_order_custom_then_return_sorted_names()
        {
            var data = new[] { "1", "C", "A", "0", "B" };
            var expected = new[] { "A", "B", "C", "0", "1" };

            var sut = new CustomColumnOrderSorter();

            var result = sut.Sort(data, i => expected);

            Assert.Collection(result,
                i => Assert.Equal(expected[0], i),
                i => Assert.Equal(expected[1], i),
                i => Assert.Equal(expected[2], i),
                i => Assert.Equal(expected[3], i),
                i => Assert.Equal(expected[4], i)
            );
        }
    }
}
