namespace TextTabulator.Cli.Wraps
{
    public interface IConsoleWrap
    {
        void Write(string text);

        void WriteLine(string text);
    }

    public class ConsoleWrap : IConsoleWrap
    {
        public void Write(string text)
        {
            Console.Write(text);
        }

        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }
    }
}
