# YamlDotNetTabulatorAdapter

Namespace: TextTabulator.Adapters.YamlDotNet

The adapter class that accepts YAML data and presents the data that it reads in a format that TextTabulator.Tabulate can consume.
 
 The data should be in the following format:
 
 - field1: value1A
 field2: value2A
 - field1: value1B
 field2: value2B
 - field1: value1C
 field2: value2C

```csharp
public class YamlDotNetTabulatorAdapter : IYamlDotNetTabulatorAdapter, TextTabulator.Adapters.ITabulatorAdapter
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [YamlDotNetTabulatorAdapter](./texttabulator.adapters.yamldotnet.yamldotnettabulatoradapter.md)<br>
Implements [IYamlDotNetTabulatorAdapter](./texttabulator.adapters.yamldotnet.iyamldotnettabulatoradapter.md), ITabulatorAdapter<br>
Attributes [NullableContextAttribute](./system.runtime.compilerservices.nullablecontextattribute.md), [NullableAttribute](./system.runtime.compilerservices.nullableattribute.md)

## Constructors

### **YamlDotNetTabulatorAdapter(Parser, YamlDotNetTabulatorAdapterOptions)**

Creates an object of type YamlDotNetTabulatorAdapter.

```csharp
public YamlDotNetTabulatorAdapter(Parser parser, YamlDotNetTabulatorAdapterOptions options)
```

#### Parameters

`parser` Parser<br>
A YamlDotNet.Core.Parser object with the YAML data to process.

`options` [YamlDotNetTabulatorAdapterOptions](./texttabulator.adapters.yamldotnet.yamldotnettabulatoradapteroptions.md)<br>
Options for the adapter.

## Methods

### **GetHeaderStrings()**

Called to return the header strings, if any, of the data. If the data does not contain headers, then null should be returned.

```csharp
public IEnumerable<string> GetHeaderStrings()
```

#### Returns

[IEnumerable&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
An enumerable containing the header strings for the table, or null if the data contains no header strings.

### **GetValueStrings()**

Called to return the row values. The outer enumeration is the rows, while the inner enumeration contains the values in each row.
 Can be an empty enumeration if the data contains no rows.

```csharp
public IEnumerable<IEnumerable<string>> GetValueStrings()
```

#### Returns

[IEnumerable&lt;IEnumerable&lt;String&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
An enumerable containing the rows and the values within each row.
