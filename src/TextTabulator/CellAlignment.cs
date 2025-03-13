using System.Collections.Generic;
using System.Linq;

namespace TextTabulator
{
    /// <summary>
    /// Specifies the alignment of the content of a cell.
    /// </summary>
    public enum CellAlignment
    {
        /// <summary>
        /// Align contents to the left.
        /// </summary>
        Left,

        /// <summary>
        /// Align contents to the right.
        /// </summary>
        Right,

        /// <summary>
        /// Attempt to center content within the cell. If the content cannot be exactly centered, the extra space will appear on the right.
        /// </summary>
        CenterLeftBias,

        /// <summary>
        /// Attempt to center content within the cell. If the content cannot be exactly centered, the extra space will appear on the left.
        /// </summary>
        CenterRightBias,
    };

    /// <summary>
    /// Interface for providing the alignment of a cell.
    /// </summary>
    public interface ICellAlignmentProvider
    {
        /// <summary>
        /// Returns the alignment for the header in the specified column.
        /// </summary>
        /// <param name="columnIndex">Zero-based index of the column to return the alignment for.</param>
        /// <returns>Alignment of the header.</returns>
        CellAlignment GetHeaderAlignment(int columnIndex);

        /// <summary>
        /// Returns the alignment for the value at the specified column and row.
        /// </summary>
        /// <param name="columnIndex">Zero-based index of the row to return the alignment for.</param>
        /// <param name="rowIndex">Zero-based index of the column to return the alignment for.</param>
        /// <returns>Alignment of the value.</returns>
        CellAlignment GetValueAlignment(int columnIndex, int rowIndex);
    }

    /// <summary>
    /// Provider that aligns all cells the same.
    /// </summary>
    public class UniformAlignmentProvider : ICellAlignmentProvider
    {
        private readonly CellAlignment _uniformAlignment;

        /// <summary>
        /// Creates an object of type UniformAlignmentProvider.
        /// </summary>
        /// <param name="uniformAlignment">Alignment to use for all cells in the table, including headers.</param>
        public UniformAlignmentProvider(CellAlignment uniformAlignment)
        {
            _uniformAlignment = uniformAlignment;
        }

        /// <summary>
        /// Returns the alignment for the header in the specified column.
        /// </summary>
        /// <param name="columnIndex">Zero-based index of the column to return the alignment for.</param>
        /// <returns>Alignment of the header.</returns>
        public CellAlignment GetHeaderAlignment(int columnIndex) => _uniformAlignment;

        /// <summary>
        /// Returns the alignment for the value at the specified column and row.
        /// </summary>
        /// <param name="columnIndex">Zero-based index of the row to return the alignment for.</param>
        /// <param name="rowIndex">Zero-based index of the column to return the alignment for.</param>
        /// <returns>Alignment of the value.</returns>
        public CellAlignment GetValueAlignment(int columnIndex, int rowIndex) => _uniformAlignment;
    }

    /// <summary>
    /// Provider that allows each cell to be aligned separately.
    /// </summary>
    public class IndividualCellAlignmentProvider : ICellAlignmentProvider
    {
        private readonly CellAlignment[] _headerAlignments;
        private readonly CellAlignment[][] _valueAlignments;

        /// <summary>
        /// Creates an object of type UniformAlignmentProvider.
        /// </summary>
        /// <param name="headerAlignments"></param>
        /// <param name="valueAlignments"></param>
        public IndividualCellAlignmentProvider(IEnumerable<CellAlignment> headerAlignments, IEnumerable<IEnumerable<CellAlignment>> valueAlignments)
        {
            _headerAlignments = headerAlignments.ToArray();
            _valueAlignments = valueAlignments.Select(i => i.ToArray()).ToArray();
        }
        /// <summary>
        /// Returns the alignment for the header in the specified column.
        /// </summary>
        /// <param name="columnIndex">Zero-based index of the column to return the alignment for.</param>
        /// <returns>Alignment of the header.</returns>
        public CellAlignment GetHeaderAlignment(int columnIndex) => _headerAlignments[columnIndex >= _headerAlignments.Length ? _headerAlignments.Length - 1 : columnIndex];

        /// <summary>
        /// Returns the alignment for the value at the specified column and row.
        /// </summary>
        /// <param name="columnIndex">Zero-based index of the row to return the alignment for.</param>
        /// <param name="rowIndex">Zero-based index of the column to return the alignment for.</param>
        /// <returns>Alignment of the value.</returns>
        public CellAlignment GetValueAlignment(int columnIndex, int rowIndex)
        {
            var columnAlignment = _valueAlignments[columnIndex >= _valueAlignments.Length ? _valueAlignments.Length - 1 : columnIndex];
            return columnAlignment[rowIndex >= columnAlignment.Length ? columnAlignment.Length - 1 : rowIndex];
        }
    }

    /// <summary>
    /// Provider that aligns all cells in a column the same.
    /// </summary>
    public class UniformColumnAlignmentProvider : ICellAlignmentProvider
    {
        private readonly CellAlignment[] _columnAlignments;

        /// <summary>
        /// Creates an object of type UniformAlignmentProvider.
        /// </summary>
        /// <param name="columnAlignments"></param>
        public UniformColumnAlignmentProvider(IEnumerable<CellAlignment> columnAlignments)
        {
            _columnAlignments = columnAlignments.ToArray();
        }

        /// <summary>
        /// Returns the alignment for the header in the specified column.
        /// </summary>
        /// <param name="columnIndex">Zero-based index of the column to return the alignment for.</param>
        /// <returns>Alignment of the header.</returns>
        public CellAlignment GetHeaderAlignment(int columnIndex) => GetValueAlignment(columnIndex, 0);

        /// <summary>
        /// Returns the alignment for the value at the specified column and row.
        /// </summary>
        /// <param name="columnIndex">Zero-based index of the row to return the alignment for.</param>
        /// <param name="rowIndex">Zero-based index of the column to return the alignment for.</param>
        /// <returns>Alignment of the value.</returns>
        public CellAlignment GetValueAlignment(int columnIndex, int rowIndex) => _columnAlignments[columnIndex >= _columnAlignments.Length ? _columnAlignments.Length - 1 : columnIndex];
    }

    /// <summary>
    /// Provider that aligns all values the same, while allowing the alignment of each header to vary.
    /// </summary>
    public class UniformValueAlignmentProvider : ICellAlignmentProvider
    {
        private readonly CellAlignment[] _headerAlignments;
        private readonly CellAlignment _uniformValueAlignment;

        /// <summary>
        /// Creates an object of type UniformAlignmentProvider.
        /// </summary>
        /// <param name="headerAlignments"></param>
        /// <param name="uniformValueAlignment"></param>
        public UniformValueAlignmentProvider(IEnumerable<CellAlignment> headerAlignments, CellAlignment uniformValueAlignment = CellAlignment.Left)
        {
            _headerAlignments = headerAlignments.ToArray();
            _uniformValueAlignment = uniformValueAlignment;
        }

        /// <summary>
        /// Returns the alignment for the header in the specified column.
        /// </summary>
        /// <param name="columnIndex">Zero-based index of the column to return the alignment for.</param>
        /// <returns>Alignment of the header.</returns>
        public CellAlignment GetHeaderAlignment(int columnIndex) => _headerAlignments[columnIndex >= _headerAlignments.Length ? _headerAlignments.Length - 1 : columnIndex];

        /// <summary>
        /// Returns the alignment for the value at the specified column and row.
        /// </summary>
        /// <param name="columnIndex">Zero-based index of the row to return the alignment for.</param>
        /// <param name="rowIndex">Zero-based index of the column to return the alignment for.</param>
        /// <returns>Alignment of the value.</returns>
        public CellAlignment GetValueAlignment(int columnIndex, int rowIndex) => _uniformValueAlignment;
    }

    /// <summary>
    /// Provider that allows a single alignment to be set for all headers and another one to be set for all values.
    /// </summary>
    public class UniformHeaderUniformValueAlignmentProvider : ICellAlignmentProvider
    {
        private readonly CellAlignment _uniformHeaderAlignment;
        private readonly CellAlignment _uniformValueAlignment;

        /// <summary>
        /// Creates an object of type UniformAlignmentProvider.
        /// </summary>
        /// <param name="uniformHeaderAlignment"></param>
        /// <param name="uniformValueAlignment"></param>
        public UniformHeaderUniformValueAlignmentProvider(CellAlignment uniformHeaderAlignment = CellAlignment.Left, CellAlignment uniformValueAlignment = CellAlignment.Right)
        {
            _uniformHeaderAlignment = uniformHeaderAlignment;
            _uniformValueAlignment = uniformValueAlignment;
        }

        /// <summary>
        /// Returns the alignment for the header in the specified column.
        /// </summary>
        /// <param name="columnIndex">Zero-based index of the column to return the alignment for.</param>
        /// <returns>Alignment of the header.</returns>
        public CellAlignment GetHeaderAlignment(int columnIndex) => _uniformHeaderAlignment;

        /// <summary>
        /// Returns the alignment for the value at the specified column and row.
        /// </summary>
        /// <param name="columnIndex">Zero-based index of the row to return the alignment for.</param>
        /// <param name="rowIndex">Zero-based index of the column to return the alignment for.</param>
        /// <returns>Alignment of the value.</returns>
        public CellAlignment GetValueAlignment(int columnIndex, int rowIndex) => _uniformValueAlignment;
    }

    /// <summary>
    /// Provider that aligns all headers the same way, while allowing the alignment of each value to vary.
    /// </summary>
    public class UniformHeaderAlignmentProvider : ICellAlignmentProvider
    {
        private readonly CellAlignment[][] _valueAlignments;
        private readonly CellAlignment _uniformHeaderAlignment;

        /// <summary>
        /// Creates an object of type UniformAlignmentProvider.
        /// </summary>
        /// <param name="valueAlignments"></param>
        /// <param name="uniformHeaderAlignment"></param>
        public UniformHeaderAlignmentProvider(IEnumerable<IEnumerable<CellAlignment>> valueAlignments, CellAlignment uniformHeaderAlignment = CellAlignment.Left)
        {
            _valueAlignments = valueAlignments.Select(i => i.ToArray()).ToArray();
            _uniformHeaderAlignment = uniformHeaderAlignment;
        }

        /// <summary>
        /// Returns the alignment for the header in the specified column.
        /// </summary>
        /// <param name="columnIndex">Zero-based index of the column to return the alignment for.</param>
        /// <returns>Alignment of the header.</returns>
        public CellAlignment GetHeaderAlignment(int columnIndex) => _uniformHeaderAlignment;

        /// <summary>
        /// Returns the alignment for the value at the specified column and row.
        /// </summary>
        /// <param name="columnIndex">Zero-based index of the row to return the alignment for.</param>
        /// <param name="rowIndex">Zero-based index of the column to return the alignment for.</param>
        /// <returns>Alignment of the value.</returns>
        public CellAlignment GetValueAlignment(int columnIndex, int rowIndex)
        {
            var columnAlignment = _valueAlignments[columnIndex >= _valueAlignments.Length ? _valueAlignments.Length - 1 : columnIndex];

            // Assume there is a header row.
            // Subtract 1 from the rowIndex because the valueAlignments that were given in the ctor did not include the header row.
            return columnAlignment[rowIndex >= columnAlignment.Length - 1 ? columnAlignment.Length - 1 : rowIndex];
        }
    }

    /// <summary>
    /// Provider that aligns all headers the same, while aligning the values in each column separately.
    /// </summary>
    public class UniformHeaderUniformColumnAlignmentProvider : ICellAlignmentProvider
    {
        private readonly CellAlignment[] _uniformColumnAlignments;
        private readonly CellAlignment _uniformHeaderAlignment;

        /// <summary>
        /// Creates an object of type UniformAlignmentProvider.
        /// </summary>
        /// <param name="uniformColumnAlignments"></param>
        /// <param name="uniformHeaderAlignment"></param>
        public UniformHeaderUniformColumnAlignmentProvider(IEnumerable<CellAlignment> uniformColumnAlignments, CellAlignment uniformHeaderAlignment = CellAlignment.Left)
        {
            _uniformColumnAlignments = uniformColumnAlignments.ToArray();
            _uniformHeaderAlignment = uniformHeaderAlignment;
        }

        /// <summary>
        /// Returns the alignment for the header in the specified column.
        /// </summary>
        /// <param name="columnIndex">Zero-based index of the column to return the alignment for.</param>
        /// <returns>Alignment of the header.</returns>
        public CellAlignment GetHeaderAlignment(int columnIndex) => _uniformHeaderAlignment;

        /// <summary>
        /// Returns the alignment for the value at the specified column and row.
        /// </summary>
        /// <param name="columnIndex">Zero-based index of the row to return the alignment for.</param>
        /// <param name="rowIndex">Zero-based index of the column to return the alignment for.</param>
        /// <returns>Alignment of the value.</returns>
        public CellAlignment GetValueAlignment(int columnIndex, int rowIndex) => _uniformColumnAlignments[columnIndex >= _uniformColumnAlignments.Length ? _uniformColumnAlignments.Length - 1 : columnIndex];
    }
}
