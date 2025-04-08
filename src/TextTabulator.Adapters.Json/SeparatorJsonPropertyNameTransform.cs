using System.Text;

namespace TextTabulator.Adapters.Json
{
    /// <summary>
    /// Abstract base class of JSON property name transform that can capitalize the first letter of words and replace separator characters.
    /// </summary>
    public abstract class SeparatorJsonPropertyNameTransform : IJsonPropertyNameTransform
    {
        private readonly char _separator;
        private readonly bool _capitalizeFirstLetterOfFirstWord;
        private readonly bool _capitalizeFirstLetterOfSubsequentWords;
        private readonly char? _separatorReplacement;

        /// <summary>
        /// Creates an object of type SeparatorJsonPropertyNameTransform.
        /// </summary>
        /// <param name="capitalizeFirstLetterOfFirstWord">True to capitalize the first letter of the first word, false otherwise.</param>
        /// <param name="capitalizeFirstLetterOfSubsequentWords">True to capitalize the first letter of subsequent words, false otherwise.</param>
        /// <param name="separatorReplacement">Specifies a character used to replace a separator. Pass in null to not replace a separator.</param>
        protected SeparatorJsonPropertyNameTransform(char separator, bool capitalizeFirstLetterOfFirstWord = true, bool capitalizeFirstLetterOfSubsequentWords = true, char? separatorReplacement = ' ')
        {
            _separator = separator;
            _capitalizeFirstLetterOfFirstWord = capitalizeFirstLetterOfFirstWord;
            _capitalizeFirstLetterOfSubsequentWords = capitalizeFirstLetterOfSubsequentWords;
            _separatorReplacement = separatorReplacement;
        }

        /// <summary>
        /// Applies the transform to the property name.
        /// </summary>
        /// <param name="propertyName">Property name upon which to apply the tranform.</param>
        /// <returns>The transformed name.</returns>
        public string Apply(string propertyName)
        {
            var sb = new StringBuilder();
            var firstLetter = true;
            var firstWord = true;

            for (var i = 0; i < propertyName.Length; i++)
            {
                var c = propertyName[i];

                if (firstLetter)
                {
                    if ((c >= 'a' && c <= 'z')
                        && ((firstWord && _capitalizeFirstLetterOfFirstWord) || (!firstWord && _capitalizeFirstLetterOfSubsequentWords)))
                    {
                        sb.Append((char)(c - 32));
                    }
                    else
                    {
                        sb.Append(c);
                    }

                    firstLetter = false;
                }
                else if (c == _separator)
                {
                    sb.Append(_separatorReplacement == null ? _separator : _separatorReplacement);

                    firstLetter = true;
                    firstWord = false;
                }
                else
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }
    }
}
