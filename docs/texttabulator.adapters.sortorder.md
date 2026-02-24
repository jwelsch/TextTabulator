# SortOrder

Namespace: TextTabulator.Adapters

The order by which columns are sorted.

```csharp
public enum SortOrder
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [Enum](https://docs.microsoft.com/en-us/dotnet/api/system.enum) → [SortOrder](./texttabulator.adapters.sortorder.md)<br>
Implements [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable), [ISpanFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.ispanformattable), [IFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.iformattable), [IConvertible](https://docs.microsoft.com/en-us/dotnet/api/system.iconvertible)

## Fields

| Name | Value | Description |
| --- | --: | --- |
| Default | 0 | The default order, which is the order in which the columns are encountered in the data. For example, for a dictionary, this would be the order in which the keys are enumerated. |
| AlphaNumericAscending | 1 | Columns are sorted in ascending alphanumeric order. |
| AlphaNumericDescending | 2 | Columns are sorted in descending alphanumeric order. |
