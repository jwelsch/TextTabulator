using TextTabulator;

namespace TextTabulatorTests
{
    public class CellAlignmentTests
    {
        [Fact]
        public void When_UniformAlignmentProvider_called_then_return_expected()
        {
            var alignment = CellAlignment.Right;

            var sut = new UniformAlignmentProvider(alignment);

            Assert.Equal(alignment, sut.GetColumnAlignment(0, 0));
            Assert.Equal(alignment, sut.GetColumnAlignment(99, 99));
        }

        [Fact]
        public void When_IndividualCellAlignmentProvider_called_then_return_expected()
        {
            var alignments = new CellAlignment[][]
            {
                new CellAlignment[] { CellAlignment.Left, CellAlignment.Right },
                new CellAlignment[] { CellAlignment.Right, CellAlignment.Left },
                new CellAlignment[] { CellAlignment.Left, CellAlignment.Right },
            };

            var sut = new IndividualCellAlignmentProvider(alignments);

            Assert.Equal(alignments[0][0], sut.GetColumnAlignment(0, 0));
            Assert.Equal(alignments[0][1], sut.GetColumnAlignment(0, 1));
            Assert.Equal(alignments[1][0], sut.GetColumnAlignment(1, 0));
            Assert.Equal(alignments[1][1], sut.GetColumnAlignment(1, 1));
            Assert.Equal(alignments[2][0], sut.GetColumnAlignment(2, 0));
            Assert.Equal(alignments[2][1], sut.GetColumnAlignment(2, 1));
        }

        [Fact]
        public void When_UniformColumnAlignmentProvider_called_then_return_expected()
        {
            var alignments = new CellAlignment[]
            {
                CellAlignment.Left,
                CellAlignment.Right,
                CellAlignment.Left,
            };

            var sut = new UniformColumnAlignmentProvider(alignments);

            Assert.Equal(alignments[0], sut.GetColumnAlignment(0, 0));
            Assert.Equal(alignments[0], sut.GetColumnAlignment(0, 9));
            Assert.Equal(alignments[1], sut.GetColumnAlignment(1, 0));
            Assert.Equal(alignments[1], sut.GetColumnAlignment(1, 9));
            Assert.Equal(alignments[2], sut.GetColumnAlignment(2, 0));
            Assert.Equal(alignments[2], sut.GetColumnAlignment(2, 9));
        }

        [Fact]
        public void When_UniformValueAlignmentProvider_called_then_return_expected()
        {
            var headerAlignments = new CellAlignment[]
            {
                CellAlignment.Left,
                CellAlignment.Right,
                CellAlignment.Left,
            };

            var sut = new UniformValueAlignmentProvider(headerAlignments, CellAlignment.Right);

            Assert.Equal(headerAlignments[0], sut.GetColumnAlignment(0, 0));
            Assert.Equal(CellAlignment.Right, sut.GetColumnAlignment(0, 9));
            Assert.Equal(headerAlignments[1], sut.GetColumnAlignment(1, 0));
            Assert.Equal(CellAlignment.Right, sut.GetColumnAlignment(1, 9));
            Assert.Equal(headerAlignments[2], sut.GetColumnAlignment(2, 0));
            Assert.Equal(CellAlignment.Right, sut.GetColumnAlignment(2, 9));
        }

        [Fact]
        public void When_UniformHeaderUniformValueAlignmentProvider_called_then_return_expected()
        {
            var sut = new UniformHeaderUniformValueAlignmentProvider(CellAlignment.Left, CellAlignment.Right);

            Assert.Equal(CellAlignment.Left, sut.GetColumnAlignment(0, 0));
            Assert.Equal(CellAlignment.Right, sut.GetColumnAlignment(1, 1));
            Assert.Equal(CellAlignment.Left, sut.GetColumnAlignment(1, 0));
            Assert.Equal(CellAlignment.Right, sut.GetColumnAlignment(1, 9));
            Assert.Equal(CellAlignment.Left, sut.GetColumnAlignment(2, 0));
            Assert.Equal(CellAlignment.Right, sut.GetColumnAlignment(2, 9));
        }

        [Fact]
        public void When_UniformHeaderAlignmentProvider_called_then_return_expected()
        {
            var alignments = new CellAlignment[][]
            {
                new CellAlignment[] { CellAlignment.Left, CellAlignment.Right },
                new CellAlignment[] { CellAlignment.Right, CellAlignment.Left },
                new CellAlignment[] { CellAlignment.Left, CellAlignment.Right },
            };

            var sut = new UniformHeaderAlignmentProvider(alignments, CellAlignment.Right);

            Assert.Equal(CellAlignment.Right, sut.GetColumnAlignment(0, 0));
            Assert.Equal(alignments[0][1], sut.GetColumnAlignment(0, 2));
            Assert.Equal(CellAlignment.Right, sut.GetColumnAlignment(1, 0));
            Assert.Equal(alignments[1][1], sut.GetColumnAlignment(1, 2));
            Assert.Equal(CellAlignment.Right, sut.GetColumnAlignment(2, 0));
            Assert.Equal(alignments[2][1], sut.GetColumnAlignment(2, 2));
        }
    }
}
