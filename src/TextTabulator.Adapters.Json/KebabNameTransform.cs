namespace TextTabulator.Adapters.Json
{
    /// <summary>
    /// A name transform that, when given kebab case names, can capitalize the first letter of words and replace dashes.
    /// </summary>
    public class KebabNameTransform : SeparatorNameTransform
    {
        /// <summary>
        /// Creates an object of type KebabNameTransform.
        /// </summary>
        /// <param name="capitalizeFirstLetterOfFirstWord">True to capitalize the first letter of the first word, false otherwise.</param>
        /// <param name="capitalizeFirstLetterOfSubsequentWords">True to capitalize the first letter of subsequent words, false otherwise.</param>
        /// <param name="dashReplacement">Specifies a character used to replace a dash. Pass in null to not replace a dash.</param>
        public KebabNameTransform(bool capitalizeFirstLetterOfFirstWord = true, bool capitalizeFirstLetterOfSubsequentWords = true, char? dashReplacement = ' ')
            : base('-', capitalizeFirstLetterOfFirstWord, capitalizeFirstLetterOfSubsequentWords, dashReplacement)
        {
        }
    }
}