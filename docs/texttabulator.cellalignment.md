# CellAlignment

Namespace: TextTabulator

Specifies the alignment of the content of a cell.

```csharp
public enum CellAlignment
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [Enum](https://docs.microsoft.com/en-us/dotnet/api/system.enum) → [CellAlignment](./texttabulator.cellalignment.md)<br>
Implements [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable), [ISpanFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.ispanformattable), [IFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.iformattable), [IConvertible](https://docs.microsoft.com/en-us/dotnet/api/system.iconvertible)

## Fields

| Name | Value | Description |
| --- | --: | --- |
| Left | 0 | Align contents to the left. |
| Right | 1 | Align contents to the right. |
| CenterLeftBias | 2 | Attempt to center content within the cell. If the content cannot be exactly centered, the extra space will appear on the right. |
| CenterRightBias | 3 | Attempt to center content within the cell. If the content cannot be exactly centered, the extra space will appear on the left. |
