# DataViewTabulatorAdapter

Namespace: TextTabulator.Adapters.MLDotNet

Class that implements the ITabulatorAdapter interface in order to adapt data contained
 in an IDataView to be consumed by Tabulator.Tabulate.

```csharp
public class DataViewTabulatorAdapter : TextTabulator.Adapters.ITabulatorAdapter
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [DataViewTabulatorAdapter](./texttabulator.adapters.mldotnet.dataviewtabulatoradapter.md)<br>
Implements ITabulatorAdapter<br>
Attributes [NullableContextAttribute](./system.runtime.compilerservices.nullablecontextattribute.md), [NullableAttribute](./system.runtime.compilerservices.nullableattribute.md)

## Constructors

### **DataViewTabulatorAdapter(IDataView, DataViewTabulatorAdapterOptions)**

Creates an object of type DataViewTabulatorAdapter.

```csharp
public DataViewTabulatorAdapter(IDataView dataView, DataViewTabulatorAdapterOptions options)
```

#### Parameters

`dataView` IDataView<br>
IDataView object containing data to be consumed by Tabulator.Tabulate.

`options` [DataViewTabulatorAdapterOptions](./texttabulator.adapters.mldotnet.dataviewtabulatoradapteroptions.md)<br>
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
