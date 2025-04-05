using System.Text;

namespace TextTabulator.Testing
{
    public static class DataGenerator
    {
        private static Random? _random;

        private static readonly char[] _characters =
        [
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
        ];

        public static string GetString(int minLength, int maxLength)
        {
            if (minLength < 0 || minLength > maxLength)
            {
                throw new ArgumentOutOfRangeException(nameof(minLength), "The minimum length must be greater than or equal to zero and less than or equal to the maximum length.");
            }

            var sb = new StringBuilder();
            _random ??= new Random((int)DateTime.Now.Ticks);

            var length = _random.Next(minLength, maxLength);

            for (var i = 0; i < length; i++)
            {
                sb.Append(_characters[_random.Next(_characters.Length)]);
            }

            return sb.ToString();
        }

        public static int GetInt(int minValue = 0, int maxValue = int.MaxValue)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(minValue), "The minimum value must be less than or equal to the maximum value.");
            }

            _random ??= new Random((int)DateTime.Now.Ticks);

            return _random.Next(minValue, maxValue);
        }
    }
}
