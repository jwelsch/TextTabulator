using System.Reflection;

namespace TextTabulator.Adapters.ReflectionTests
{
    internal static class PrivateMemberGetter
    {
        public static object? GetFieldValue(object obj, string name)
        {
            var type = obj.GetType();
            var info = type.GetField(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                ?? throw new ArgumentException($"A field with name '{name}' does not exist in type '{type.FullName}'.", nameof(name));
            return info.GetValue(obj);
        }

        public static object? GetPropertyValue(object obj, string name)
        {
            var type = obj.GetType();
            var info = type.GetProperty(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                ?? throw new ArgumentException($"A property with name '{name}' does not exist in type '{type.FullName}'.", nameof(name));
            return info.GetValue(obj);
        }
    }
}
