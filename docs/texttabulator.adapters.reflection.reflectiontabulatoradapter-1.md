# ReflectionTabulatorAdapter&lt;T&gt;

Namespace: TextTabulator.Adapters.Reflection

Class that implements the ITabulatorAdapter interface in order to adapt types to be consumed by the Tabulator.Tabulate method.
 The type 'T' will be reflected and the names of its properties and/or fields will be adapted to headers. The values contained
 in each object in the enumeration will be adapted to the row values.

```csharp
public class ReflectionTabulatorAdapter<T> : IReflectionTabulatorAdapter, TextTabulator.Adapters.ITabulatorAdapter
```

#### Type Parameters

`T`<br>
Type that will be reflected.

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ReflectionTabulatorAdapter&lt;T&gt;](./texttabulator.adapters.reflection.reflectiontabulatoradapter-1.md)<br>
Implements [IReflectionTabulatorAdapter](./texttabulator.adapters.reflection.ireflectiontabulatoradapter.md), ITabulatorAdapter<br>
Attributes [NullableContextAttribute](./system.runtime.compilerservices.nullablecontextattribute.md), [NullableAttribute](./system.runtime.compilerservices.nullableattribute.md)

## Constructors

### **ReflectionTabulatorAdapter(IEnumerable&lt;T&gt;, ReflectionTabulatorAdapterOptions)**

ReflectionTabulatorAdapter constructor that takes an enumerable.

```csharp
public ReflectionTabulatorAdapter(IEnumerable<T> items, ReflectionTabulatorAdapterOptions options)
```

#### Parameters

`items` IEnumerable&lt;T&gt;<br>
Enumerable of items to adapt.

`options` [ReflectionTabulatorAdapterOptions](./texttabulator.adapters.reflection.reflectiontabulatoradapteroptions.md)<br>
Options for the adapter.

### **ReflectionTabulatorAdapter(T, ReflectionTabulatorAdapterOptions)**

ReflectionTabulatorAdapter constructor that takes an enumerable.

```csharp
public ReflectionTabulatorAdapter(T item, ReflectionTabulatorAdapterOptions options)
```

#### Parameters

`item` T<br>
Item to adapt.

`options` [ReflectionTabulatorAdapterOptions](./texttabulator.adapters.reflection.reflectiontabulatoradapteroptions.md)<br>
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
