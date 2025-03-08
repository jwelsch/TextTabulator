using TextTabulator;

internal class Program
{
    private static void Main(string[] args)
    {
        var headers = new string[]
        {
            "Header",
            "Header2",
            "ZZZHeader3"
        };

        var values = new string[][]
        {
            new string[] { "Value1A", "Value2A", "Value3A" },
            new string[] { "Value1B", "YYYValue2B", "Value3B" },
            new string[] { "XXXValue1C", "Value2C", "Value3C" },
        };

        var tabulator = new Tabulator();

        //var options = new TabulatorOptions();
        var options = new TabulatorOptions
        {
            CellAlignmentProvider = new UniformHeaderUniformColumnAlignmentProvider(new CellAlignment[] { CellAlignment.Left, CellAlignment.Right }, CellAlignment.CenterLeftBias),
            ValueRowSeparator = '─',
            ValueLeftEdgeJoint = '╟',
            ValueMiddleJoint = '┼',
            ValueRightEdgeJoint = '╢',
            HeaderRowSeparator = '═',
            HeaderLeftEdgeJoint = '╠',
            HeaderMiddleJoint = '╪',
            HeaderRightEdgeJoint = '╣',
            LeftEdge = '║',
            RightEdge = '║',
            TopEdge = '═',
            BottomEdge = '═',
            ColumnSeparator = '│',
            TopLeftCorner = '╔',
            TopRightCorner = '╗',
            BottomLeftCorner = '╚',
            BottomRightCorner = '╝',
            TopEdgeJoint = '╤',
            BottomEdgeJoint = '╧',
        };

        var table = tabulator.Tabulate(headers, values, options);

        Console.WriteLine(table);
    }
}