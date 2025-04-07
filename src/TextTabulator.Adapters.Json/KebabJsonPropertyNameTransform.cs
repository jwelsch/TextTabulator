namespace TextTabulator.Adapters.Json
{
    /// <summary>
    /// A JSON property name transform that can capitalize the first letter of words and replace dashes.
    /// </summary>
    public class KebabJsonPropertyNameTransform : SeparatorJsonPropertyNameTransform
    {
        /// <summary>
        /// Creates an object of type KebabJsonPropertyNameTransform.
        /// </summary>
        /// <param name="capitalizeFirstLetterOfFirstWord">True to capitalize the first letter of the first word, false otherwise.</param>
        /// <param name="capitalizeFirstLetterOfSubsequentWords">True to capitalize the first letter of subsequent words, false otherwise.</param>
        /// <param name="dashReplacement">Specifies a character used to replace a dash. Pass in null to not replace a dash.</param>
        public KebabJsonPropertyNameTransform(bool capitalizeFirstLetterOfFirstWord = true, bool capitalizeFirstLetterOfSubsequentWords = true, char? dashReplacement = ' ')
            : base('-', capitalizeFirstLetterOfFirstWord, capitalizeFirstLetterOfSubsequentWords, dashReplacement)
        {
        }
    }
}