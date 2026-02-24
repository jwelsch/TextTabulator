# IColumnNameMapper

Namespace: TextTabulator.Adapters

```csharp
public interface IColumnNameMapper
```

Attributes [NullableContextAttribute](./system.runtime.compilerservices.nullablecontextattribute.md)

## Methods

### **GetColumnName(String)**

```csharp
ColumnNameIndex GetColumnName(string mappedColumnName)
```

#### Parameters

`mappedColumnName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[ColumnNameIndex](./texttabulator.adapters.columnnameindex.md)<br>

### **GetMappedColumnName(String)**

```csharp
ColumnNameIndex GetMappedColumnName(string columnName)
```

#### Parameters

`columnName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[ColumnNameIndex](./texttabulator.adapters.columnnameindex.md)<br>

### **GetSortedColumnNames()**

```csharp
String[] GetSortedColumnNames()
```

#### Returns

[String[]](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **GetSortedMappedColumnNames()**

```csharp
String[] GetSortedMappedColumnNames()
```

#### Returns

[String[]](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
