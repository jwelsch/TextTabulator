namespace TextTabulator.Adapters.Json
{
    /// <summary>
    /// A name transform that, when given snake case names, can capitalize the first letter of words and replace underscores.
    /// </summary>
    public class SnakeNameTransform : SeparatorNameTransform
    {
        /// <summary>
        /// Creates an object of type SnakeNameTransform.
        /// </summary>
        /// <param name="capitalizeFirstLetterOfFirstWord">True to capitalize the first letter of the first word, false otherwise.</param>
        /// <param name="capitalizeFirstLetterOfSubsequentWords">True to capitalize the first letter of subsequent words, false otherwise.</param>
        /// <param name="underscoreReplacement">Specifies a character used to replace an underscore. Pass in null to not replace an underscore.</param>
        public SnakeNameTransform(bool capitalizeFirstLetterOfFirstWord = true, bool capitalizeFirstLetterOfSubsequentWords = true, char? underscoreReplacement = ' ')
            : base('_', capitalizeFirstLetterOfFirstWord, capitalizeFirstLetterOfSubsequentWords, underscoreReplacement)
        {
        }
    }
}
