# RowData

Namespace: TextTabulator

```csharp
public class RowData : IRowData
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [RowData](./texttabulator.rowdata.md)<br>
Implements [IRowData](./texttabulator.irowdata.md)

## Properties

### **Row**

```csharp
public int Row { get; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Cells**

```csharp
public IReadOnlyList<ICellData> Cells { get; }
```

#### Property Value

[IReadOnlyList&lt;ICellData&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

### **MaxHeight**

```csharp
public int MaxHeight { get; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

## Constructors

### **RowData(Int32, IReadOnlyList&lt;ICellData&gt;, Int32)**

```csharp
public RowData(int row, IReadOnlyList<ICellData> cells, int maxHeight)
```

#### Parameters

`row` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`cells` [IReadOnlyList&lt;ICellData&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

`maxHeight` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
