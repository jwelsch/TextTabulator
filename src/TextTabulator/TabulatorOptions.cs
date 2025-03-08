namespace TextTabulator
{
    public interface ITabulatorOptions
    {
        ITableStyling Styling { get; set; }

        ICellAlignmentProvider CellAlignment { get; set; }
    }

    public class TabulatorOptions : ITabulatorOptions
    {
        public ITableStyling Styling { get; set; } = new AsciiTableStyling();

        public ICellAlignmentProvider CellAlignment { get; set; } = new UniformAlignmentProvider(TextTabulator.CellAlignment.Left);
    }
}
