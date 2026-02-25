# TypeMembers

Namespace: TextTabulator.Adapters.Reflection

Specifies which category of members. This enumeration supports bitwise operations.

```csharp
public enum TypeMembers
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [Enum](https://docs.microsoft.com/en-us/dotnet/api/system.enum) → [TypeMembers](./texttabulator.adapters.reflection.typemembers.md)<br>
Implements [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable), [ISpanFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.ispanformattable), [IFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.iformattable), [IConvertible](https://docs.microsoft.com/en-us/dotnet/api/system.iconvertible)<br>
Attributes [FlagsAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.flagsattribute)

## Fields

| Name | Value | Description |
| --- | --: | --- |
| Properties | 1 | Property members. |
| Fields | 16 | Field members, not including backing fields. |
| PropertiesAndFields | 17 | Both field and property members. |
