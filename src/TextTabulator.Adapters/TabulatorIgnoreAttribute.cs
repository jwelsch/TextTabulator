using System;

namespace TextTabulator.Adapters
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class TabulatorIgnoreAttribute : Attribute
    {
    }
}
