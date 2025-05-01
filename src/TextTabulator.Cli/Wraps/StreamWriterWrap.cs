namespace TextTabulator.Cli.Wraps
{
    public interface IStreamWriterWrap : IDisposable
    {
        void Write(string? value);
    }

    public class StreamWriterWrap : IStreamWriterWrap
    {
        private readonly StreamWriter _writer;

        private bool _disposed;

        public StreamWriterWrap(StreamWriter writer)
        {
            _writer = writer;
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _writer.Dispose();
                _disposed = true;
            }

            GC.SuppressFinalize(this);
        }

        public void Write(string? value)
        {
            _writer.Write(value);
        }
    }
}
