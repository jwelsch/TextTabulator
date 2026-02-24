# RowDataParser

Namespace: TextTabulator

```csharp
public class RowDataParser : IRowDataParser
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [RowDataParser](./texttabulator.rowdataparser.md)<br>
Implements [IRowDataParser](./texttabulator.irowdataparser.md)<br>
Attributes [NullableContextAttribute](./system.runtime.compilerservices.nullablecontextattribute.md), [NullableAttribute](./system.runtime.compilerservices.nullableattribute.md)

## Constructors

### **RowDataParser(ITabulatorOptions)**

```csharp
public RowDataParser(ITabulatorOptions options)
```

#### Parameters

`options` [ITabulatorOptions](./texttabulator.itabulatoroptions.md)<br>

## Methods

### **Parse(Int32, IEnumerable&lt;String&gt;, List`1&)**

```csharp
public IRowData Parse(int row, IEnumerable<string> cells, List`1& maxWidths)
```

#### Parameters

`row` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`cells` [IEnumerable&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

`maxWidths` [List`1&](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1&)<br>

#### Returns

[IRowData](./texttabulator.irowdata.md)<br>
