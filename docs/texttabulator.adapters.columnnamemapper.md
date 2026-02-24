# ColumnNameMapper

Namespace: TextTabulator.Adapters

```csharp
public class ColumnNameMapper : IColumnNameMapper
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ColumnNameMapper](./texttabulator.adapters.columnnamemapper.md)<br>
Implements [IColumnNameMapper](./texttabulator.adapters.icolumnnamemapper.md)<br>
Attributes [NullableContextAttribute](./system.runtime.compilerservices.nullablecontextattribute.md), [NullableAttribute](./system.runtime.compilerservices.nullableattribute.md)

## Constructors

### **ColumnNameMapper(IEnumerable&lt;String&gt;, INameTransform, IColumnOrderSorter)**

```csharp
public ColumnNameMapper(IEnumerable<string> columnNames, INameTransform transform, IColumnOrderSorter sorter)
```

#### Parameters

`columnNames` [IEnumerable&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

`transform` [INameTransform](./texttabulator.adapters.inametransform.md)<br>

`sorter` [IColumnOrderSorter](./texttabulator.adapters.icolumnordersorter.md)<br>

## Methods

### **GetColumnName(String)**

```csharp
public ColumnNameIndex GetColumnName(string mappedColumnName)
```

#### Parameters

`mappedColumnName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[ColumnNameIndex](./texttabulator.adapters.columnnameindex.md)<br>

### **GetMappedColumnName(String)**

```csharp
public ColumnNameIndex GetMappedColumnName(string columnName)
```

#### Parameters

`columnName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[ColumnNameIndex](./texttabulator.adapters.columnnameindex.md)<br>

### **GetSortedColumnNames()**

```csharp
public String[] GetSortedColumnNames()
```

#### Returns

[String[]](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **GetSortedMappedColumnNames()**

```csharp
public String[] GetSortedMappedColumnNames()
```

#### Returns

[String[]](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
