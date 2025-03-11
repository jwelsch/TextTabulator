namespace TextTabulator
{
    /// <summary>
    /// Delegate for generating the content of a cell dynamically.
    /// </summary>
    /// <returns>String representation of the item that the cell represents.</returns>
    public delegate string CellValue();
}
