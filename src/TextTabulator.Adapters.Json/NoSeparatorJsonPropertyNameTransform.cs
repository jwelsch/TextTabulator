using System.Text;

namespace TextTabulator.Adapters.Json
{
    /// <summary>
    /// Abstract base class of JSON property name transforms where the name has no separator.
    /// </summary>
    public abstract class NoSeparatorJsonPropertyNameTransform : IJsonPropertyNameTransform
    {
        private readonly bool _capitalizeFirstLetterOfFirstWord;
        private readonly bool _capitalizeFirstLetterOfSubsequentWords;
        private readonly char? _separator;

        /// <summary>
        /// Creates an object of type NoSeparatorJsonPropertyNameTransform.
        /// </summary>
        /// <param name="capitalizeFirstLetterOfFirstWord">True to capitalize the first letter of the first word, false otherwise.</param>
        /// <param name="capitalizeFirstLetterOfSubsequentWords">True to capitalize the first letter of subsequent words, false otherwise.</param>
        /// <param name="separator">Specifies a character used as a separator. Pass in null to not use a separator.</param>
        protected NoSeparatorJsonPropertyNameTransform(bool capitalizeFirstLetterOfFirstWord = true, bool capitalizeFirstLetterOfSubsequentWords = true, char? separator = ' ')
        {
            _capitalizeFirstLetterOfFirstWord = capitalizeFirstLetterOfFirstWord;
            _capitalizeFirstLetterOfSubsequentWords = capitalizeFirstLetterOfSubsequentWords;
            _separator = separator;
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
                        if (_separator != null)
                        {
                            sb.Append(_separator);
                        }
                        sb.Append(_capitalizeFirstLetterOfSubsequentWords ? c : (char)(c + 32));
                        inNumber = false;
                    }
                    else if (c >= '0' && c <= '9')
                    {
                        if (!inNumber && _separator != null)
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
