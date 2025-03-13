using CsvHelper;
using System;
using System.Collections.Generic;

namespace TextTabulator.Adapters.CsvHelper
{
    /// <summary>
    /// Public interface for ICsvHelperTabulatorAdapter.
    /// </summary>
    public interface ICsvHelperTabulatorAdapter : ITabulatorAdapter
    {
    }

    /// <summary>
    /// Class that implements the ITabulatorAdapter interface in order to adapt CSV data ready by CsvHelper
    /// to be consumed by Tabulator.Tabulate.
    /// </summary>
    public class CsvHelperTabulatorAdapter : ICsvHelperTabulatorAdapter
    {
        private readonly CsvReader _csvReader;
        private readonly bool _hasHeaderRow;

        /// <summary>
        /// CsvHelperTabulatorAdapter constructor.
        /// </summary>
        /// <param name="csvReader">CsvReader object that will provide the parsed CSV data.</param>
        /// <param name="hasHeaderRow">True if the CSV data contains a header row, false if not.</param>
        public CsvHelperTabulatorAdapter(CsvReader csvReader, bool hasHeaderRow)
        {
            _csvReader = csvReader;
            _hasHeaderRow = hasHeaderRow;
        }

        /// <summary>
        /// Called to return the header strings, if any, of the data. If the data does not contain headers, then null should be returned.
        /// </summary>
        /// <returns>An enumerable containing the header strings for the table, or null if the data contains no header strings.</returns>
        public IEnumerable<string>? GetHeaderStrings()
        {
            if (!_hasHeaderRow)
            {
                return null;
            }

            if (!_csvReader.Read()
                || !_csvReader.ReadHeader()
                || _csvReader.HeaderRecord == null)
            {
                throw new Exception($"No header row found.");
            }

            return _csvReader.HeaderRecord;
        }

        /// <summary>
        /// Called to return the row values. The outer enumeration is the rows, while the inner enumeration contains the values in each row.
        /// Can be an empty enumeration if the data contains no rows.
        /// </summary>
        /// <returns>An enumerable containing the rows and the values within each row.</returns>
        public IEnumerable<IEnumerable<string>> GetValueStrings()
        {
            var csvRows = new List<string[]>();

            while (_csvReader.Read())
            {
                var row = new string[_csvReader.ColumnCount];

                for (var i = 0; i < _csvReader.ColumnCount; i++)
                {
                    row[i] = _csvReader.GetField(i) ?? string.Empty;
                }

                csvRows.Add(row);
            }

            return csvRows;
        }
    }
}
