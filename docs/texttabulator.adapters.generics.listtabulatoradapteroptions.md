# ListTabulatorAdapterOptions

Namespace: TextTabulator.Adapters.Generics

Options to allow configuration of the ListTabulatorAdapter class.

```csharp
public class ListTabulatorAdapterOptions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ListTabulatorAdapterOptions](./texttabulator.adapters.generics.listtabulatoradapteroptions.md)<br>
Attributes [NullableContextAttribute](./system.runtime.compilerservices.nullablecontextattribute.md), [NullableAttribute](./system.runtime.compilerservices.nullableattribute.md)

## Properties

### **IncludeIndex**

Gets whether or not to include an index column as the first column in the output.

```csharp
public bool IncludeIndex { get; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **HeaderNameTransform**

Gets the transform to apply to header names.

```csharp
public INameTransform HeaderNameTransform { get; }
```

#### Property Value

INameTransform<br>

### **ColumnSortOrder**

Gets the way by which the columns themselves are sorted.

```csharp
public SortOrder ColumnSortOrder { get; }
```

#### Property Value

SortOrder<br>

## Constructors

### **ListTabulatorAdapterOptions(Boolean, INameTransform, SortOrder)**

Creates an object of type ListTabulatorAdapterOptions.

```csharp
public ListTabulatorAdapterOptions(bool includeIndex, INameTransform headerNameTransform, SortOrder columnSortOrder)
```

#### Parameters

`includeIndex` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Whether or not to include an index column as the first column in the output.

`headerNameTransform` INameTransform<br>
The transform to apply to header names.

`columnSortOrder` SortOrder<br>
The way by which the columns themselves are sorted.
