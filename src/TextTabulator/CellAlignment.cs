using System.Collections.Generic;
using System.Linq;

namespace TextTabulator
{
    public enum CellAlignment
    {
        Left,
        Right
    };

    public interface ICellAlignmentProvider
    {
        CellAlignment GetColumnAlignment(int columnIndex, int rowIndex);
    }

    public class UniformAlignmentProvider : ICellAlignmentProvider
    {
        private readonly CellAlignment _uniformAlignment;

        public UniformAlignmentProvider(CellAlignment uniformAlignment)
        {
            _uniformAlignment = uniformAlignment;
        }

        public CellAlignment GetColumnAlignment(int columnIndex, int rowIndex) => _uniformAlignment;
    }

    public class IndividualCellAlignmentProvider : ICellAlignmentProvider
    {
        private readonly CellAlignment[][] _cellAlignments;

        public IndividualCellAlignmentProvider(IEnumerable<IEnumerable<CellAlignment>> cellAlignments)
        {
            _cellAlignments = cellAlignments.Select(i => i.ToArray()).ToArray();
        }

        public CellAlignment GetColumnAlignment(int columnIndex, int rowIndex) => _cellAlignments[columnIndex][rowIndex];
    }

    public class UniformColumnAlignmentProvider : ICellAlignmentProvider
    {
        private readonly CellAlignment[] _columnAlignments;

        public UniformColumnAlignmentProvider(IEnumerable<CellAlignment> columnAlignments)
        {
            _columnAlignments = columnAlignments.ToArray();
        }

        public CellAlignment GetColumnAlignment(int columnIndex, int rowIndex) => _columnAlignments[columnIndex];
    }

    public class UniformValueAlignmentProvider : ICellAlignmentProvider
    {
        private readonly CellAlignment[] _headerAlignments;
        private readonly CellAlignment _uniformValueAlignment;

        public UniformValueAlignmentProvider(IEnumerable<CellAlignment> headerAlignments, CellAlignment uniformValueAlignment = CellAlignment.Left)
        {
            _headerAlignments = headerAlignments.ToArray();
            _uniformValueAlignment = uniformValueAlignment;
        }

        public CellAlignment GetColumnAlignment(int columnIndex, int rowIndex)
        {
            return rowIndex == 0 ? _headerAlignments[columnIndex] : _uniformValueAlignment;
        }
    }

    public class UniformHeaderUniformValueAlignmentProvider : ICellAlignmentProvider
    {
        private readonly CellAlignment _headerAlignment;
        private readonly CellAlignment _uniformValueAlignment;

        public UniformHeaderUniformValueAlignmentProvider(CellAlignment headerAlignment = CellAlignment.Left, CellAlignment uniformValueAlignment = CellAlignment.Left)
        {
            _headerAlignment = headerAlignment;
            _uniformValueAlignment = uniformValueAlignment;
        }

        public CellAlignment GetColumnAlignment(int columnIndex, int rowIndex)
        {
            return rowIndex == 0 ? _headerAlignment : _uniformValueAlignment;
        }
    }

    public class UniformHeaderAlignmentProvider : ICellAlignmentProvider
    {
        private readonly CellAlignment _uniformHeaderAlignment;
        private readonly CellAlignment[] _valueAlignments;

        public UniformHeaderAlignmentProvider(IEnumerable<CellAlignment> valueAlignments, CellAlignment uniformHeaderAlignment = CellAlignment.Left)
        {
            _valueAlignments = valueAlignments.ToArray();
            _uniformHeaderAlignment = uniformHeaderAlignment;
        }

        public CellAlignment GetColumnAlignment(int columnIndex, int rowIndex)
        {
            return rowIndex == 0 ? _uniformHeaderAlignment : _valueAlignments[columnIndex];
        }
    }
}
