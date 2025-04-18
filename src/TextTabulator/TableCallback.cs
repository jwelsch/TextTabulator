namespace TextTabulator
{
    /// <summary>
    /// Delegate for receiving callbacks when elements of the table are constructed.
    /// </summary>
    /// <param name="text">The most recently constructed element of the table.</param>
    public delegate void TableCallback(string text);
}
