# JsonTabulatorAdapter

Namespace: TextTabulator.Adapters.Json

The adapter class that accepts JSON data and presents the data that it reads in a format that TextTabulator.Tabulate can consume.
 
 The data should be in the following format:
 
 [
 {
 "field1": value1A,
 "field2": "value2A"
 },
 {
 "field1": value1B,
 "field2": "value2B"
 },
 {
 "field1": value1C,
 "field2": "value2C"
 }
 ]

```csharp
public class JsonTabulatorAdapter : IJsonTabulatorAdapter, TextTabulator.Adapters.ITabulatorAdapter
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [JsonTabulatorAdapter](./texttabulator.adapters.json.jsontabulatoradapter.md)<br>
Implements [IJsonTabulatorAdapter](./texttabulator.adapters.json.ijsontabulatoradapter.md), ITabulatorAdapter<br>
Attributes [NullableContextAttribute](./system.runtime.compilerservices.nullablecontextattribute.md), [NullableAttribute](./system.runtime.compilerservices.nullableattribute.md)

## Constructors

### **JsonTabulatorAdapter(Func&lt;Stream&gt;, JsonTabulatorAdapterOptions)**

Creates an object of type JsonTabulatorAdapter.
 In order for the adpater to function correctly, make sure the JSON data only contains an array of homogeneous JSON objects.

```csharp
public JsonTabulatorAdapter(Func<Stream> jsonStreamProvider, JsonTabulatorAdapterOptions options)
```

#### Parameters

`jsonStreamProvider` [Func&lt;Stream&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-1)<br>
Function that provides a stream containing an array containing homogeneous objects of UTF-8 encoded JSON data.

`options` [JsonTabulatorAdapterOptions](./texttabulator.adapters.json.jsontabulatoradapteroptions.md)<br>
Options for the adapter.

### **JsonTabulatorAdapter(Stream, JsonTabulatorAdapterOptions)**

Creates an object of type JsonTabulatorAdapter.
 In order for the adpater to function correctly, make sure the JSON data only contains an array of homogeneous JSON objects.

```csharp
public JsonTabulatorAdapter(Stream jsonStream, JsonTabulatorAdapterOptions options)
```

#### Parameters

`jsonStream` [Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>
Stream containing UTF-8 encoded JSON data.

`options` [JsonTabulatorAdapterOptions](./texttabulator.adapters.json.jsontabulatoradapteroptions.md)<br>
Options for the adapter.

### **JsonTabulatorAdapter(String, JsonTabulatorAdapterOptions)**

Creates an object of type JsonTabulatorAdapter.
 In order for the adpater to function correctly, make sure the JSON data only contains an array of homogeneous JSON objects.

```csharp
public JsonTabulatorAdapter(string json, JsonTabulatorAdapterOptions options)
```

#### Parameters

`json` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
String containing raw JSON data.

`options` [JsonTabulatorAdapterOptions](./texttabulator.adapters.json.jsontabulatoradapteroptions.md)<br>
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
