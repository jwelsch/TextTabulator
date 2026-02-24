# IColumnOrderSorter

Namespace: TextTabulator.Adapters

Interface for sorting the columns themselves in a specific order.

```csharp
public interface IColumnOrderSorter
```

Attributes [NullableContextAttribute](./system.runtime.compilerservices.nullablecontextattribute.md)

## Methods

### **Sort(IEnumerable&lt;String&gt;)**

Called to sort the column names.

```csharp
IEnumerable<string> Sort(IEnumerable<string> columnNames)
```

#### Parameters

`columnNames` [IEnumerable&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
The column names to sort.

#### Returns

[IEnumerable&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
The sorted column names.
