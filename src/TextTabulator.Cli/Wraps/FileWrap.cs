namespace TextTabulator.Cli.Wraps
{
    public interface IFileWrap
    {
        bool Exists(string? path);
    }

    public class FileWrap : IFileWrap
    {
        public bool Exists(string? path)
        {
            return File.Exists(path);
        }
    }
}
