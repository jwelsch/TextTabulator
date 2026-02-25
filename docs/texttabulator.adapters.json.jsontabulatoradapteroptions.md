# JsonTabulatorAdapterOptions

Namespace: TextTabulator.Adapters.Json

Options to allow configuration of the JsonTabulatorAdapter class.

```csharp
public class JsonTabulatorAdapterOptions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [JsonTabulatorAdapterOptions](./texttabulator.adapters.json.jsontabulatoradapteroptions.md)<br>
Attributes [NullableContextAttribute](./system.runtime.compilerservices.nullablecontextattribute.md), [NullableAttribute](./system.runtime.compilerservices.nullableattribute.md)

## Properties

### **PropertyNameTransform**

Gets the transform to apply to JSON property names.

```csharp
public INameTransform PropertyNameTransform { get; }
```

#### Property Value

INameTransform<br>

### **JsonReaderOptions**

Gets options that define customized behavior of the Utf8JsonReader that differs from the JSON RFC (for example, how to handle comments or maximum depth allowed when reading). By default, the Utf8JsonReader follows the JSON RFC strictly; comments within the JSON are invalid, and the maximum depth is 64.

```csharp
public JsonReaderOptions JsonReaderOptions { get; }
```

#### Property Value

JsonReaderOptions<br>

### **HeaderSorter**

Gets the sorter to use for the headers.

```csharp
public IHeaderOrderSorter HeaderSorter { get; }
```

#### Property Value

IHeaderOrderSorter<br>

## Constructors

### **JsonTabulatorAdapterOptions(INameTransform, JsonReaderOptions, IHeaderOrderSorter)**

Creates an object of type JsonTabulatorAdapterOptions.

```csharp
public JsonTabulatorAdapterOptions(INameTransform propertyNameTransform, JsonReaderOptions jsonReaderOptions, IHeaderOrderSorter headerSorter)
```

#### Parameters

`propertyNameTransform` INameTransform<br>
Transform to apply to JSON property names. Passing null will cause the JSON property names to not be altered.

`jsonReaderOptions` JsonReaderOptions<br>
Options that define customized behavior of the Utf8JsonReader that differs from the JSON RFC (for example, how to handle comments or maximum depth allowed when reading). By default, the Utf8JsonReader follows the JSON RFC strictly; comments within the JSON are invalid, and the maximum depth is 64.

`headerSorter` IHeaderOrderSorter<br>
Specifies the sorter to use for the headers. Passing null will use DefaultHeaderOrderSorter.
