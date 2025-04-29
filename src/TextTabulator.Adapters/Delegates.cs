using System.IO;

namespace TextTabulator.Adapters
{
    /// <summary>
    /// Delegate which is called to provide raw string input data.
    /// </summary>
    /// <returns></returns>
    public delegate string StringProvider();

    /// <summary>
    /// Delegate which is called to provide input data from a Stream.
    /// </summary>
    /// <returns></returns>
    public delegate Stream StreamProvider();

    /// <summary>
    /// Delegate which is called to provide input data from a TextReader.
    /// </summary>
    /// <returns></returns>
    public delegate TextReader ReaderProvider();
}