namespace TextTabulator.Adapters
{
    /// <summary>
    /// A name transform that, when given Pascal case names, can capitalize the first letter of words and insert separators.
    /// </summary>
    public class PascalNameTransform : NoSeparatorNameTransform
    {
        /// <summary>
        /// Creates an object of type PascalNameTransform.
        /// </summary>
        /// <param name="capitalizeFirstLetterOfFirstWord">True to capitalize the first letter of the first word, false otherwise.</param>
        /// <param name="capitalizeFirstLetterOfSubsequentWords">True to capitalize the first letter of subsequent words, false otherwise.</param>
        /// <param name="separator">Specifies a character used as a separator.</param>
        public PascalNameTransform(bool capitalizeFirstLetterOfFirstWord = true, bool capitalizeFirstLetterOfSubsequentWords = true, char? separator = ' ')
            : base(capitalizeFirstLetterOfFirstWord, capitalizeFirstLetterOfSubsequentWords, separator)
        {
        }
    }
}
