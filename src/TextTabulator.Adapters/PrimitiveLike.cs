using System;

namespace TextTabulator.Adapters
{
    public static class PrimitiveLike
    {
        public static bool Detect(Type type)
        {
            return type.IsPrimitive
                || type.IsEnum
                || type == typeof(string)
                || type == typeof(DateTime)
                || type == typeof(DateTimeOffset)
                || type == typeof(TimeSpan)
                || type == typeof(decimal);
        }
    }
}
