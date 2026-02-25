# TableDataParser

Namespace: TextTabulator

```csharp
public class TableDataParser : ITableDataParser
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [TableDataParser](./texttabulator.tabledataparser.md)<br>
Implements [ITableDataParser](./texttabulator.itabledataparser.md)<br>
Attributes [NullableContextAttribute](./system.runtime.compilerservices.nullablecontextattribute.md), [NullableAttribute](./system.runtime.compilerservices.nullableattribute.md)

## Constructors

### **TableDataParser(ITabulatorOptions)**

```csharp
public TableDataParser(ITabulatorOptions options)
```

#### Parameters

`options` [ITabulatorOptions](./texttabulator.itabulatoroptions.md)<br>

## Methods

### **Parse(IEnumerable&lt;String&gt;, IEnumerable&lt;IEnumerable&lt;String&gt;&gt;)**

```csharp
public ITableData Parse(IEnumerable<string> headers, IEnumerable<IEnumerable<string>> rows)
```

#### Parameters

`headers` [IEnumerable&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

`rows` [IEnumerable&lt;IEnumerable&lt;String&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

#### Returns

[ITableData](./texttabulator.itabledata.md)<br>
