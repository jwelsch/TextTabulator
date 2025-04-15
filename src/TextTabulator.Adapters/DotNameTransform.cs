namespace TextTabulator.Adapters
{
    /// <summary>
    /// A name transform that, when given names separated by dots ('.'), can capitalize the first letter of words and replace dots.
    /// </summary>
    public class DotNameTransform : SeparatorNameTransform
    {
        /// <summary>
        /// Creates an object of type DotNameTransform.
        /// </summary>
        /// <param name="capitalizeFirstLetterOfFirstWord">True to capitalize the first letter of the first word, false otherwise.</param>
        /// <param name="capitalizeFirstLetterOfSubsequentWords">True to capitalize the first letter of subsequent words, false otherwise.</param>
        /// <param name="dotReplacement">Specifies a character used to replace a dot. Pass in null to not replace a dot.</param>
        public DotNameTransform(bool capitalizeFirstLetterOfFirstWord = true, bool capitalizeFirstLetterOfSubsequentWords = true, char? dotReplacement = ' ')
            : base('.', capitalizeFirstLetterOfFirstWord, capitalizeFirstLetterOfSubsequentWords, dotReplacement)
        {
        }
    }
}
