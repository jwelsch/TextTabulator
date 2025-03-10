﻿using System.Collections.Generic;
using System.Linq;

namespace TextTabulator
{
    public enum CellAlignment
    {
        Left,
        Right,
        CenterLeftBias,
        CenterRightBias,
    };

    public interface ICellAlignmentProvider
    {
        CellAlignment GetHeaderAlignment(int columnIndex);

        CellAlignment GetValueAlignment(int columnIndex, int rowIndex);
    }

    public class UniformAlignmentProvider : ICellAlignmentProvider
    {
        private readonly CellAlignment _uniformAlignment;

        public UniformAlignmentProvider(CellAlignment uniformAlignment)
        {
            _uniformAlignment = uniformAlignment;
        }

        public CellAlignment GetHeaderAlignment(int columnIndex) => _uniformAlignment;

        public CellAlignment GetValueAlignment(int columnIndex, int rowIndex) => _uniformAlignment;
    }

    public class IndividualCellAlignmentProvider : ICellAlignmentProvider
    {
        private readonly CellAlignment[] _headerAlignments;
        private readonly CellAlignment[][] _valueAlignments;

        public IndividualCellAlignmentProvider(IEnumerable<CellAlignment> headerAlignments, IEnumerable<IEnumerable<CellAlignment>> valueAlignments)
        {
            _headerAlignments = headerAlignments.ToArray();
            _valueAlignments = valueAlignments.Select(i => i.ToArray()).ToArray();
        }
        public CellAlignment GetHeaderAlignment(int columnIndex) => _headerAlignments[columnIndex >= _headerAlignments.Length ? _headerAlignments.Length - 1 : columnIndex];

        public CellAlignment GetValueAlignment(int columnIndex, int rowIndex)
        {
            var columnAlignment = _valueAlignments[columnIndex >= _valueAlignments.Length ? _valueAlignments.Length - 1 : columnIndex];
            return columnAlignment[rowIndex >= columnAlignment.Length ? columnAlignment.Length - 1 : rowIndex];
        }
    }

    public class UniformColumnAlignmentProvider : ICellAlignmentProvider
    {
        private readonly CellAlignment[] _columnAlignments;

        public UniformColumnAlignmentProvider(IEnumerable<CellAlignment> columnAlignments)
        {
            _columnAlignments = columnAlignments.ToArray();
        }
        public CellAlignment GetHeaderAlignment(int columnIndex) => GetValueAlignment(columnIndex, 0);

        public CellAlignment GetValueAlignment(int columnIndex, int rowIndex) => _columnAlignments[columnIndex >= _columnAlignments.Length ? _columnAlignments.Length - 1 : columnIndex];
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

        public CellAlignment GetHeaderAlignment(int columnIndex) => _headerAlignments[columnIndex >= _headerAlignments.Length ? _headerAlignments.Length - 1 : columnIndex];

        public CellAlignment GetValueAlignment(int columnIndex, int rowIndex) => _uniformValueAlignment;
    }

    public class UniformHeaderUniformValueAlignmentProvider : ICellAlignmentProvider
    {
        private readonly CellAlignment _uniformHeaderAlignment;
        private readonly CellAlignment _uniformValueAlignment;

        public UniformHeaderUniformValueAlignmentProvider(CellAlignment uniformHeaderAlignment = CellAlignment.Left, CellAlignment uniformValueAlignment = CellAlignment.Right)
        {
            _uniformHeaderAlignment = uniformHeaderAlignment;
            _uniformValueAlignment = uniformValueAlignment;
        }

        public CellAlignment GetHeaderAlignment(int columnIndex) => _uniformHeaderAlignment;

        public CellAlignment GetValueAlignment(int columnIndex, int rowIndex) => _uniformValueAlignment;
    }

    public class UniformHeaderAlignmentProvider : ICellAlignmentProvider
    {
        private readonly CellAlignment[][] _valueAlignments;
        private readonly CellAlignment _uniformHeaderAlignment;

        public UniformHeaderAlignmentProvider(IEnumerable<IEnumerable<CellAlignment>> valueAlignments, CellAlignment uniformHeaderAlignment = CellAlignment.Left)
        {
            _valueAlignments = valueAlignments.Select(i => i.ToArray()).ToArray();
            _uniformHeaderAlignment = uniformHeaderAlignment;
        }

        public CellAlignment GetHeaderAlignment(int columnIndex) => _uniformHeaderAlignment;

        public CellAlignment GetValueAlignment(int columnIndex, int rowIndex)
        {
            var columnAlignment = _valueAlignments[columnIndex >= _valueAlignments.Length ? _valueAlignments.Length - 1 : columnIndex];

            // Assume there is a header row.
            // Subtract 1 from the rowIndex because the valueAlignments that were given in the ctor did not include the header row.
            return columnAlignment[rowIndex >= columnAlignment.Length - 1 ? columnAlignment.Length - 1 : rowIndex];
        }
    }

    public class UniformHeaderUniformColumnAlignmentProvider : ICellAlignmentProvider
    {
        private readonly CellAlignment[] _uniformColumnAlignments;
        private readonly CellAlignment _uniformHeaderAlignment;

        public UniformHeaderUniformColumnAlignmentProvider(IEnumerable<CellAlignment> uniformColumnAlignments, CellAlignment uniformHeaderAlignment = CellAlignment.Left)
        {
            _uniformColumnAlignments = uniformColumnAlignments.ToArray();
            _uniformHeaderAlignment = uniformHeaderAlignment;
        }

        public CellAlignment GetHeaderAlignment(int columnIndex) => _uniformHeaderAlignment;

        public CellAlignment GetValueAlignment(int columnIndex, int rowIndex) => _uniformColumnAlignments[columnIndex >= _uniformColumnAlignments.Length ? _uniformColumnAlignments.Length - 1 : columnIndex];
    }
}
