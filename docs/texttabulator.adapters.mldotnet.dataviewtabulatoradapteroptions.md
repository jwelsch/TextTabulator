# DataViewTabulatorAdapterOptions

Namespace: TextTabulator.Adapters.MLDotNet

Options to allow configuration of the DataViewTabulatorAdapter class.

```csharp
public class DataViewTabulatorAdapterOptions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [DataViewTabulatorAdapterOptions](./texttabulator.adapters.mldotnet.dataviewtabulatoradapteroptions.md)<br>
Attributes [NullableContextAttribute](./system.runtime.compilerservices.nullablecontextattribute.md), [NullableAttribute](./system.runtime.compilerservices.nullableattribute.md)

## Properties

### **ColumnNameTransform**

Gets the transform to apply to column names. Passing null will cause the column names to not be altered.

```csharp
public INameTransform ColumnNameTransform { get; }
```

#### Property Value

INameTransform<br>

### **TypeFormatter**

Gets the formatter to apply to cell values. Passing null will cause the cell values to use default formatting.

```csharp
public ITypeFormatter TypeFormatter { get; }
```

#### Property Value

ITypeFormatter<br>

### **HeaderSorter**

Gets the sorter to use for the headers.

```csharp
public IHeaderOrderSorter HeaderSorter { get; }
```

#### Property Value

IHeaderOrderSorter<br>

## Constructors

### **DataViewTabulatorAdapterOptions(INameTransform, ITypeFormatter, IHeaderOrderSorter)**

Creates an object of type DataViewTabulatorAdapterOptions.

```csharp
public DataViewTabulatorAdapterOptions(INameTransform columnNameTransform, ITypeFormatter typeFormatter, IHeaderOrderSorter headerSorter)
```

#### Parameters

`columnNameTransform` INameTransform<br>
Transform to apply to cell names. Passing null will cause the cell names to not be altered.

`typeFormatter` ITypeFormatter<br>
Formatter to apply to cell values. Passing null will cause the cell values to use default formatting.

`headerSorter` IHeaderOrderSorter<br>
Sorter to use for the headers. Passing null will cause the headers to use default sorting.
