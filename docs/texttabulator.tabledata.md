# TableData

Namespace: TextTabulator

```csharp
public class TableData : ITableData
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [TableData](./texttabulator.tabledata.md)<br>
Implements [ITableData](./texttabulator.itabledata.md)<br>
Attributes [NullableContextAttribute](./system.runtime.compilerservices.nullablecontextattribute.md), [NullableAttribute](./system.runtime.compilerservices.nullableattribute.md)

## Properties

### **Headers**

```csharp
public IRowData Headers { get; }
```

#### Property Value

[IRowData](./texttabulator.irowdata.md)<br>

### **ValueRows**

```csharp
public IReadOnlyList<IRowData> ValueRows { get; }
```

#### Property Value

[IReadOnlyList&lt;IRowData&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

### **MaxColumnWidths**

```csharp
public IReadOnlyList<int> MaxColumnWidths { get; }
```

#### Property Value

[IReadOnlyList&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

## Constructors

### **TableData(IRowData, IReadOnlyList&lt;IRowData&gt;, IReadOnlyList&lt;Int32&gt;)**

```csharp
public TableData(IRowData headers, IReadOnlyList<IRowData> valueRows, IReadOnlyList<int> maxColumnWidths)
```

#### Parameters

`headers` [IRowData](./texttabulator.irowdata.md)<br>

`valueRows` [IReadOnlyList&lt;IRowData&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

`maxColumnWidths` [IReadOnlyList&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>
