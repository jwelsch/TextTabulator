using TextTabulator;

internal class Program
{
    private static void Main(string[] args)
    {
        var headers = new string[]
        {
            "Header1",
            "Header2",
            "Header3"
        };

        var values = new string[][]
        {
            new string[] { "Value1A", "Value1B", "Value1C" },
            new string[] { "Value2A", "Value2B", "Value2C" },
            new string[] { "Value3A", "Value3B", "Value3C" },
        };

        var tabulator = new Tabulator();

        var table = tabulator.Tabulate(headers, values);

        Console.WriteLine(table);
    }
}