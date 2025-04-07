using System;
using System.Collections.Generic;
using System.Text;

namespace TextTabulator.Adapters.Json
{
    /// <summary>
    /// A JSON property name transform that when given camel case names can capitalize the first letter of words and insert separators.
    /// </summary>
    public class CamelJsonPropertyNameTransform : IJsonPropertyNameTransform
    {
        private readonly char _separator;
        private readonly bool _capitalizeFirstLetterOfFirstWord;
        private readonly bool _capitalizeFirstLetterOfSubsequentWords;

        /// <summary>
        /// Creates an object of type CamelJsonPropertyNameTransform.
        /// </summary>
        /// <param name="separator">Specifies a character used as a separator.</param>
        /// <param name="capitalizeFirstLetterOfFirstWord">True to capitalize the first letter of the first word, false otherwise.</param>
        /// <param name="capitalizeFirstLetterOfSubsequentWords">True to capitalize the first letter of subsequent words, false otherwise.</param>
        public CamelJsonPropertyNameTransform(char separator = ' ', bool capitalizeFirstLetterOfFirstWord = true, bool capitalizeFirstLetterOfSubsequentWords = true)
        {
            _separator = separator;
            _capitalizeFirstLetterOfFirstWord = capitalizeFirstLetterOfFirstWord;
            _capitalizeFirstLetterOfSubsequentWords = capitalizeFirstLetterOfSubsequentWords;
        }

        /// <summary>
        /// Applies the transform to the property name.
        /// </summary>
        /// <param name="propertyName">Property name upon which to apply the tranform.</param>
        /// <returns>The transformed name.</returns>
        public string Apply(string propertyName)
        {
            var sb = new StringBuilder();

            var inNumber = false;

            for (var i = 0; i < propertyName.Length; i++)
            {
                var c = propertyName[i];

                if (i == 0)
                {
                    if (c >= 'a' && c <= 'z')
                    {
                        sb.Append(_capitalizeFirstLetterOfFirstWord ? (char)(c - 32) : c);
                        inNumber = false;
                    }
                    else if (c >= 'A' && c <= 'Z')
                    {
                        sb.Append(_capitalizeFirstLetterOfFirstWord ? c : (char)(c + 32));
                        inNumber = false;
                    }
                    else if (c >= '0' && c <= '9')
                    {
                        sb.Append(c);
                        inNumber = true;
                    }
                }
                else
                {
                    if (c >= 'A' && c <= 'Z')
                    {
                        sb.Append(_separator);
                        sb.Append(_capitalizeFirstLetterOfSubsequentWords ? c : (char)(c + 32));
                        inNumber = false;
                    }
                    else if (c >= '0' && c <= '9')
                    {
                        if (!inNumber)
                        {
                            sb.Append(_separator);
                        }

                        sb.Append(c);
                        inNumber = true;
                    }
                    else
                    {
                        sb.Append(c);
                    }
                }
            }

            return sb.ToString();
        }
    }
}
