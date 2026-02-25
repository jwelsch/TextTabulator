# ListTabulatorAdapter&lt;T&gt;

Namespace: TextTabulator.Adapters.Generics

```csharp
public class ListTabulatorAdapter<T> : IListTabulatorAdapter`1, TextTabulator.Adapters.ITabulatorAdapter
```

#### Type Parameters

`T`<br>

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ListTabulatorAdapter&lt;T&gt;](./texttabulator.adapters.generics.listtabulatoradapter-1.md)<br>
Implements IListTabulatorAdapter&lt;T&gt;, ITabulatorAdapter<br>
Attributes [NullableContextAttribute](./system.runtime.compilerservices.nullablecontextattribute.md), [NullableAttribute](./system.runtime.compilerservices.nullableattribute.md)

## Constructors

### **ListTabulatorAdapter(IList&lt;T&gt;, ListTabulatorAdapterOptions)**

```csharp
public ListTabulatorAdapter(IList<T> list, ListTabulatorAdapterOptions options)
```

#### Parameters

`list` IList&lt;T&gt;<br>

`options` [ListTabulatorAdapterOptions](./texttabulator.adapters.generics.listtabulatoradapteroptions.md)<br>

## Methods

### **GetHeaderStrings()**

```csharp
public IEnumerable<string> GetHeaderStrings()
```

#### Returns

[IEnumerable&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

### **GetValueStrings()**

```csharp
public IEnumerable<IEnumerable<string>> GetValueStrings()
```

#### Returns

[IEnumerable&lt;IEnumerable&lt;String&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
