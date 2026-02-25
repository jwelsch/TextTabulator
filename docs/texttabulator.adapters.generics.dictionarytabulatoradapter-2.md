# DictionaryTabulatorAdapter&lt;TKey, TValue&gt;

Namespace: TextTabulator.Adapters.Generics

```csharp
public class DictionaryTabulatorAdapter<TKey, TValue> : IDictionaryTabulatorAdapter`2, TextTabulator.Adapters.ITabulatorAdapter
```

#### Type Parameters

`TKey`<br>

`TValue`<br>

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [DictionaryTabulatorAdapter&lt;TKey, TValue&gt;](./texttabulator.adapters.generics.dictionarytabulatoradapter-2.md)<br>
Implements IDictionaryTabulatorAdapter&lt;TKey, TValue&gt;, ITabulatorAdapter<br>
Attributes [NullableContextAttribute](./system.runtime.compilerservices.nullablecontextattribute.md), [NullableAttribute](./system.runtime.compilerservices.nullableattribute.md)

## Constructors

### **DictionaryTabulatorAdapter(IDictionary&lt;TKey, TValue&gt;, DictionaryTabulatorAdapterOptions)**

```csharp
public DictionaryTabulatorAdapter(IDictionary<TKey, TValue> dictionary, DictionaryTabulatorAdapterOptions options)
```

#### Parameters

`dictionary` IDictionary&lt;TKey, TValue&gt;<br>

`options` [DictionaryTabulatorAdapterOptions](./texttabulator.adapters.generics.dictionarytabulatoradapteroptions.md)<br>

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
