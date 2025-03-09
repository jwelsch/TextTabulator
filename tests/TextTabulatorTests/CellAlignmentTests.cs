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

            Assert.Equal(alignment, sut.GetHeaderAlignment(0));
            Assert.Equal(alignment, sut.GetHeaderAlignment(99));
            Assert.Equal(alignment, sut.GetValueAlignment(0, 0));
            Assert.Equal(alignment, sut.GetValueAlignment(99, 99));
        }

        [Fact]
        public void When_IndividualCellAlignmentProvider_called_then_return_expected()
        {
            var headerAlignments = new CellAlignment[]
            {
                CellAlignment.CenterLeftBias,
                CellAlignment.CenterRightBias,
            };

            var valueAlignments = new CellAlignment[][]
            {
                new CellAlignment[] { CellAlignment.Left, CellAlignment.Right },
                new CellAlignment[] { CellAlignment.Right, CellAlignment.Left },
            };

            var sut = new IndividualCellAlignmentProvider(headerAlignments, valueAlignments);

            Assert.Equal(headerAlignments[0], sut.GetHeaderAlignment(0));
            Assert.Equal(headerAlignments[1], sut.GetHeaderAlignment(1));
            Assert.Equal(valueAlignments[0][0], sut.GetValueAlignment(0, 0));
            Assert.Equal(valueAlignments[0][1], sut.GetValueAlignment(0, 1));
            Assert.Equal(valueAlignments[1][0], sut.GetValueAlignment(1, 0));
            Assert.Equal(valueAlignments[1][1], sut.GetValueAlignment(1, 1));
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

            Assert.Equal(alignments[0], sut.GetValueAlignment(0, 0));
            Assert.Equal(alignments[0], sut.GetValueAlignment(0, 9));
            Assert.Equal(alignments[1], sut.GetValueAlignment(1, 0));
            Assert.Equal(alignments[1], sut.GetValueAlignment(1, 9));
            Assert.Equal(alignments[2], sut.GetValueAlignment(2, 0));
            Assert.Equal(alignments[2], sut.GetValueAlignment(2, 9));
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

            Assert.Equal(headerAlignments[0], sut.GetHeaderAlignment(0));
            Assert.Equal(CellAlignment.Right, sut.GetValueAlignment(0, 9));
            Assert.Equal(headerAlignments[1], sut.GetHeaderAlignment(1));
            Assert.Equal(CellAlignment.Right, sut.GetValueAlignment(1, 9));
            Assert.Equal(headerAlignments[2], sut.GetHeaderAlignment(2));
            Assert.Equal(CellAlignment.Right, sut.GetValueAlignment(2, 9));
        }

        [Fact]
        public void When_UniformHeaderUniformValueAlignmentProvider_called_then_return_expected()
        {
            var sut = new UniformHeaderUniformValueAlignmentProvider(CellAlignment.Left, CellAlignment.Right);

            Assert.Equal(CellAlignment.Left, sut.GetHeaderAlignment(0));
            Assert.Equal(CellAlignment.Left, sut.GetHeaderAlignment(9));
            Assert.Equal(CellAlignment.Right, sut.GetValueAlignment(1, 1));
            Assert.Equal(CellAlignment.Right, sut.GetValueAlignment(1, 9));
            Assert.Equal(CellAlignment.Right, sut.GetValueAlignment(2, 9));
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

            Assert.Equal(CellAlignment.Right, sut.GetHeaderAlignment(0));
            Assert.Equal(alignments[0][1], sut.GetValueAlignment(0, 2));
            Assert.Equal(CellAlignment.Right, sut.GetHeaderAlignment(1));
            Assert.Equal(alignments[1][1], sut.GetValueAlignment(1, 2));
            Assert.Equal(CellAlignment.Right, sut.GetHeaderAlignment(2));
            Assert.Equal(alignments[2][1], sut.GetValueAlignment(2, 2));
        }

        [Fact]
        public void When_UniformHeaderUniformColumnAlignmentProvider_called_then_return_expected()
        {
            var alignments = new CellAlignment[]
            {
                CellAlignment.Left,
                CellAlignment.CenterLeftBias,
                CellAlignment.Right
            };

            var sut = new UniformHeaderUniformColumnAlignmentProvider(alignments, CellAlignment.CenterRightBias);

            Assert.Equal(CellAlignment.CenterRightBias, sut.GetHeaderAlignment(0));
            Assert.Equal(CellAlignment.CenterRightBias, sut.GetHeaderAlignment(1));
            Assert.Equal(CellAlignment.CenterRightBias, sut.GetHeaderAlignment(2));
            Assert.Equal(alignments[0], sut.GetValueAlignment(0, 2));
            Assert.Equal(alignments[1], sut.GetValueAlignment(1, 2));
            Assert.Equal(alignments[2], sut.GetValueAlignment(2, 2));
        }
    }
}
