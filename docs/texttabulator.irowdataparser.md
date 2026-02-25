# IRowDataParser

Namespace: TextTabulator

```csharp
public interface IRowDataParser
```

Attributes [NullableContextAttribute](./system.runtime.compilerservices.nullablecontextattribute.md)

## Methods

### **Parse(Int32, IEnumerable&lt;String&gt;, List`1&)**

```csharp
IRowData Parse(int row, IEnumerable<string> cells, List`1& maxWidths)
```

#### Parameters

`row` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`cells` [IEnumerable&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

`maxWidths` [List`1&](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1&)<br>

#### Returns

[IRowData](./texttabulator.irowdata.md)<br>
