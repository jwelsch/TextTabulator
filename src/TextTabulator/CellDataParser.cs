using System;
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

            var lines = new List<string>();
            var carriageReturn = false;
            var maxWidth = 0;
            var height = 1;
            var sb = new StringBuilder();

            for (var i = 0; i < text.Length; i++)
            {
                var c = text[i];

                if (c == '\r')
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
