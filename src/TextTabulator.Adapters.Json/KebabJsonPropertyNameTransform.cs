using System.Text;

namespace TextTabulator.Adapters.Json
{
    /// <summary>
    /// A relatively simple JSON property name transform that can capitalize the first letter of words
    /// and replace dashes.
    /// </summary>
    public class KebabJsonPropertyNameTransform : IJsonPropertyNameTransform
    {
        private readonly bool _capitalizeFirstLetterOfFirstWord;
        private readonly bool _capitalizeFirstLetterOfSubsequentWords;
        private readonly char? _dashReplacement;

        /// <summary>
        /// Creates an object of type BasicJsonPropertyNameTransform.
        /// </summary>
        /// <param name="capitalizeFirstLetterOfFirstWord">True to capitalize the first letter of the first word, false otherwise.</param>
        /// <param name="capitalizeFirstLetterOfSubsequentWords">True to capitalize the first letter of subsequent words, false otherwise.</param>
        /// <param name="dashReplacement">Specifies a character used to replace a dash. Pass in null to not replace a dash.</param>
        public KebabJsonPropertyNameTransform(bool capitalizeFirstLetterOfFirstWord = true, bool capitalizeFirstLetterOfSubsequentWords = true, char? dashReplacement = ' ')
        {
            _capitalizeFirstLetterOfFirstWord = capitalizeFirstLetterOfFirstWord;
            _capitalizeFirstLetterOfSubsequentWords = capitalizeFirstLetterOfSubsequentWords;
            _dashReplacement = dashReplacement;
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
                else if (c == '-')
                {
                    sb.Append(_dashReplacement == null ? '-' : _dashReplacement);

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