using CsvHelper;
using System;
using System.Collections.Generic;

namespace TextTabulator.Adapter.CsvHelper
{
    public interface ICsvHelperTabulatorAdapter : ITabulatorAdapter
    {
    }

    public class CsvHelperTabulatorAdapter : ICsvHelperTabulatorAdapter
    {
        private readonly CsvReader _csvReader;
        private readonly bool _hasHeaderRow;

        public CsvHelperTabulatorAdapter(CsvReader csvReader, bool hasHeaderRow)
        {
            _csvReader = csvReader;
            _hasHeaderRow = hasHeaderRow;
        }

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
