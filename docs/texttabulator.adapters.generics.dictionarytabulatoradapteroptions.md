# DictionaryTabulatorAdapterOptions

Namespace: TextTabulator.Adapters.Generics

Options to allow configuration of the DictionaryTabulatorAdapter class.

```csharp
public class DictionaryTabulatorAdapterOptions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [DictionaryTabulatorAdapterOptions](./texttabulator.adapters.generics.dictionarytabulatoradapteroptions.md)<br>
Attributes [NullableContextAttribute](./system.runtime.compilerservices.nullablecontextattribute.md), [NullableAttribute](./system.runtime.compilerservices.nullableattribute.md)

## Properties

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

### **DictionaryTabulatorAdapterOptions(INameTransform, SortOrder)**

Creates an object of type DictionaryTabulatorAdapterOptions.

```csharp
public DictionaryTabulatorAdapterOptions(INameTransform headerNameTransform, SortOrder columnSortOrder)
```

#### Parameters

`headerNameTransform` INameTransform<br>
The transform to apply to header names.

`columnSortOrder` SortOrder<br>
The way by which the columns themselves are sorted.
