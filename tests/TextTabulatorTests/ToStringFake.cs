namespace TextTabulatorTests
{
    internal class ToStringFake
    {
        private readonly object _value;

        public ToStringFake(object value) => _value = value;

#pragma warning disable CS8603 // Possible null reference return.
        public override string ToString() => _value.ToString();
#pragma warning restore CS8603 // Possible null reference return.
    }
}
