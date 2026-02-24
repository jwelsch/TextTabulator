# ITableDataParser

Namespace: TextTabulator

```csharp
public interface ITableDataParser
```

Attributes [NullableContextAttribute](./system.runtime.compilerservices.nullablecontextattribute.md)

## Methods

### **Parse(IEnumerable&lt;String&gt;, IEnumerable&lt;IEnumerable&lt;String&gt;&gt;)**

```csharp
ITableData Parse(IEnumerable<string> headers, IEnumerable<IEnumerable<string>> rows)
```

#### Parameters

`headers` [IEnumerable&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

`rows` [IEnumerable&lt;IEnumerable&lt;String&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

#### Returns

[ITableData](./texttabulator.itabledata.md)<br>
