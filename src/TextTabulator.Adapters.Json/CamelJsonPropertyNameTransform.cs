namespace TextTabulator.Adapters.Json
{
    /// <summary>
    /// A JSON property name transform that, when given camel case names, can capitalize the first letter of words and insert separators.
    /// </summary>
    public class CamelJsonPropertyNameTransform : NoSeparatorJsonPropertyNameTransform
    {
        /// <summary>
        /// Creates an object of type CamelJsonPropertyNameTransform.
        /// </summary>
        /// <param name="capitalizeFirstLetterOfFirstWord">True to capitalize the first letter of the first word, false otherwise.</param>
        /// <param name="capitalizeFirstLetterOfSubsequentWords">True to capitalize the first letter of subsequent words, false otherwise.</param>
        /// <param name="separator">Specifies a character used as a separator. Pass in null to not use a separator.</param>
        public CamelJsonPropertyNameTransform(bool capitalizeFirstLetterOfFirstWord = true, bool capitalizeFirstLetterOfSubsequentWords = true, char? separator = ' ')
            : base(capitalizeFirstLetterOfFirstWord, capitalizeFirstLetterOfSubsequentWords, separator)
        {
        }
    }
}
