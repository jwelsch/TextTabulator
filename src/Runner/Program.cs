using TextTabulator;

internal class Program
{
    private static void Main(string[] args)
    {
        var headers = new string[]
        {
            "Header1",
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

        var table = tabulator.Tabulate(headers, values, new TabulatorOptions
        {
            CellAlignmentProvider = new UniformHeaderUniformColumnAlignmentProvider(new CellAlignment[] { CellAlignment.Left, CellAlignment.Right }),
            HeaderRowSeparator = '=',
            HeaderMiddleJoint = '=',
        });

        Console.WriteLine(table);
    }
}