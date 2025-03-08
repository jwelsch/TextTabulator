using TextTabulator;

internal class Program
{
    private static void Main(string[] args)
    {
        var headers = new string[]
        {
            "XXXHeader1",
            "YYYHeader2",
            "ZZZHeader3"
        };

        var values = new string[][]
        {
            new string[] { "Value1A", "Value1B", "Value1C" },
            new string[] { "Value2A", "Value2B", "Value2C" },
            new string[] { "Value3A", "Value3B", "Value3C" },
        };

        var tabulator = new Tabulator();

        var table = tabulator.Tabulate(headers, values, new TabulatorOptions
        {
            //CellAlignmentProvider = new UniformHeaderUniformValueAlignmentProvider(CellAlignment.Left, CellAlignment.Left)
            CellAlignmentProvider = new UniformColumnAlignmentProvider(new[] {CellAlignment.Left, CellAlignment.Right, CellAlignment.Right}),
        });

        Console.WriteLine(table);
    }
}