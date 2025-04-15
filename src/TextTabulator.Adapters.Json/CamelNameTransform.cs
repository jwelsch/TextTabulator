namespace TextTabulator.Adapters.Json
{
    /// <summary>
    /// A name transform that, when given camel case names, can capitalize the first letter of words and insert separators.
    /// </summary>
    public class CamelNameTransform : NoSeparatorNameTransform
    {
        /// <summary>
        /// Creates an object of type CamelNameTransform.
        /// </summary>
        /// <param name="capitalizeFirstLetterOfFirstWord">True to capitalize the first letter of the first word, false otherwise.</param>
        /// <param name="capitalizeFirstLetterOfSubsequentWords">True to capitalize the first letter of subsequent words, false otherwise.</param>
        /// <param name="separator">Specifies a character used as a separator. Pass in null to not use a separator.</param>
        public CamelNameTransform(bool capitalizeFirstLetterOfFirstWord = true, bool capitalizeFirstLetterOfSubsequentWords = true, char? separator = ' ')
            : base(capitalizeFirstLetterOfFirstWord, capitalizeFirstLetterOfSubsequentWords, separator)
        {
        }
    }
}
