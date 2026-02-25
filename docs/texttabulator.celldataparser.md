# CellDataParser

Namespace: TextTabulator

```csharp
public class CellDataParser : ICellDataParser
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [CellDataParser](./texttabulator.celldataparser.md)<br>
Implements [ICellDataParser](./texttabulator.icelldataparser.md)<br>
Attributes [NullableContextAttribute](./system.runtime.compilerservices.nullablecontextattribute.md), [NullableAttribute](./system.runtime.compilerservices.nullableattribute.md)

## Constructors

### **CellDataParser(ITabulatorOptions)**

```csharp
public CellDataParser(ITabulatorOptions options)
```

#### Parameters

`options` [ITabulatorOptions](./texttabulator.itabulatoroptions.md)<br>

## Methods

### **Parse(Int32, Int32, String)**

```csharp
public ICellData Parse(int column, int row, string text)
```

#### Parameters

`column` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`row` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`text` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[ICellData](./texttabulator.icelldata.md)<br>
