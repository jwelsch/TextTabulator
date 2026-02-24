# ITableData

Namespace: TextTabulator

```csharp
public interface ITableData
```

Attributes [NullableContextAttribute](./system.runtime.compilerservices.nullablecontextattribute.md)

## Properties

### **Headers**

```csharp
public abstract IRowData Headers { get; }
```

#### Property Value

[IRowData](./texttabulator.irowdata.md)<br>

### **ValueRows**

```csharp
public abstract IReadOnlyList<IRowData> ValueRows { get; }
```

#### Property Value

[IReadOnlyList&lt;IRowData&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

### **MaxColumnWidths**

```csharp
public abstract IReadOnlyList<int> MaxColumnWidths { get; }
```

#### Property Value

[IReadOnlyList&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>
