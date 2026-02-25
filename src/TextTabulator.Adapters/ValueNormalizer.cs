using System;

namespace TextTabulator.Adapters
{
    internal interface IValueNormalizer
    {
        string Normalize(string value);
    }

    internal class ValueNormalizer : IValueNormalizer
    {
        public string Normalize(string value)
        {
            if (value.Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                return "True";
            }
            else if (value.Equals("false", StringComparison.OrdinalIgnoreCase))
            {
                return "False";
            }
            else if (value.Equals("null", StringComparison.OrdinalIgnoreCase))
            {
                return string.Empty;
            }

            return value;
        }
    }
}
