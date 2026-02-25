# CsvHelperTabulatorAdapterOptions

Namespace: TextTabulator.Adapters.CsvHelper

Options to allow configuration of the CsvHelperTabulatorAdapter class.

```csharp
public class CsvHelperTabulatorAdapterOptions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [CsvHelperTabulatorAdapterOptions](./texttabulator.adapters.csvhelper.csvhelpertabulatoradapteroptions.md)<br>
Attributes [NullableContextAttribute](./system.runtime.compilerservices.nullablecontextattribute.md), [NullableAttribute](./system.runtime.compilerservices.nullableattribute.md)

## Properties

### **HeaderNameTransform**

Gets the transform to apply to CSV header names.

```csharp
public INameTransform HeaderNameTransform { get; }
```

#### Property Value

INameTransform<br>

### **HasHeaderRow**

Gets whether or not the CSV data contains a header row. Defaults to true.

```csharp
public bool HasHeaderRow { get; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **HeaderSorter**

Gets the sorter to use for the headers.

```csharp
public IHeaderOrderSorter HeaderSorter { get; }
```

#### Property Value

IHeaderOrderSorter<br>

## Constructors

### **CsvHelperTabulatorAdapterOptions(INameTransform, Boolean, IHeaderOrderSorter)**

Creates an object of type CsvHelperTabulatorAdapterOptions.

```csharp
public CsvHelperTabulatorAdapterOptions(INameTransform headerNameTransform, bool hasHeaderRow, IHeaderOrderSorter headerSorter)
```

#### Parameters

`headerNameTransform` INameTransform<br>
Transform to apply to CSV header names. Passing null will cause the CSV header names to not be altered.

`hasHeaderRow` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
True if the CSV data contains a header row, false if not. Defaults to true.

`headerSorter` IHeaderOrderSorter<br>
Specifies the sorter to use for the headers. Passing null will use DefaultHeaderOrderSorter.
