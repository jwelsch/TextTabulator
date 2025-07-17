using System;
using System.Collections.Generic;

namespace TextTabulator.Adapters
{
    /// <summary>
    /// Interface for formatting values of different types to strings.
    /// </summary>
    public interface ITypeFormatter
    {
        /// <summary>
        /// Called to format a value of a specific type to a string representation.
        /// </summary>
        /// <param name="value">Value to format.</param>
        /// <returns>String representation of the value.</returns>
        string FormatTypeValue(object value);
    }

    /// <summary>
    /// Default implementation of ITypeFormatter that formats values based on their type.
    /// </summary>
    public class TypeFormatter : ITypeFormatter
    {
        private readonly Dictionary<Type, Func<object, string>>? _formatters;

        /// <summary>
        /// Creates an object of type TypeFormatter.
        /// </summary>
        /// <param name="formatters">A mapping of types to formatting functions. Types not in the Dictionary will use default formatting. Pass null to use default formatting for all types.</param>
        public TypeFormatter(Dictionary<Type, Func<object, string>>? formatters = null)
        {
            _formatters = formatters;
        }

        /// <summary>
        /// Called to format a value of a specific type to a string representation.
        /// </summary>
        /// <param name="value">Value to format.</param>
        /// <returns>String representation of the value.</returns>
        public string FormatTypeValue(object value)
        {
            if (_formatters != null && _formatters.TryGetValue(value.GetType(), out var formatter))
            {
                return formatter.Invoke(value);
            }

            return value.ToString();
        }
    }
}
