﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TextTabulator
{
    public interface ICellDataParser
    {
        ICellData Parse(int column, int row, string? text);
    }

    public class CellDataParser : ICellDataParser
    {
        private readonly ITabulatorOptions _options;

        public CellDataParser(ITabulatorOptions options)
        {
            _options = options;
        }

        public ICellData Parse(int column, int row, string? text)
        {
            if (column < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(column), $"Column must be greater than or equal to zero.");
            }

            if (row < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(row), $"Row must be greater than or equal to zero.");
            }

            if (text == null)
            {
                return new CellData(column, row, null, 0, 0);
            }

            PrintableCharacterMatcher? printableCharacterMatcher = _options.IncludeNonPrintableCharacters ? null : new PrintableCharacterMatcher();
            var lines = new List<string>();
            var carriageReturn = false;
            var maxWidth = 0;
            var height = 1;
            var sb = new StringBuilder();

            for (var i = 0; i < text.Length; i++)
            {
                var c = text[i];

                // If including non-printable characters, treat them as normal characters.
                // If not including non-printable characters, do not add them to the StringBuilder.
                if (printableCharacterMatcher != null && !printableCharacterMatcher.IsMatch(c))
                {
                    continue;
                }
                else if (c == '\r')
                {
                    carriageReturn = true;
                }
                else if (c == '\n')
                {
                    if (carriageReturn)
                    {
                        carriageReturn = false;
                    }

                    height++;

                    if (sb.Length > maxWidth)
                    {
                        maxWidth = sb.Length;
                    }

                    lines.Add(sb.ToString());
                    sb.Clear();
                }
                else if (c == '\t')
                {
                    // If _options.TabLength is negative, the tab character is not added to the line.
                    if (_options.TabLength == 0)
                    {
                        sb.Append(c);
                    }
                    else if (_options.TabLength > 0)
                    {
                        sb.Append(' ', _options.TabLength);
                    }
                }
                else
                {
                    if (carriageReturn)
                    {
                        carriageReturn = false;

                        height++;

                        if (sb.Length > maxWidth)
                        {
                            maxWidth = sb.Length;
                        }

                        lines.Add(sb.ToString());
                        sb.Clear();
                    }

                    sb.Append(c);
                }
            }

            lines.Add(sb.ToString());

            if (sb.Length > maxWidth)
            {
                maxWidth = sb.Length;
            }

            return new CellData(column, row, lines, maxWidth, height);
        }
    }
}
