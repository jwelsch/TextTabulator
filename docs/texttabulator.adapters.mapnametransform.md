# MapNameTransform

Namespace: TextTabulator.Adapters

A name transform that maps an existing name to a new name.

```csharp
public class MapNameTransform : INameTransform
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [MapNameTransform](./texttabulator.adapters.mapnametransform.md)<br>
Implements [INameTransform](./texttabulator.adapters.inametransform.md)<br>
Attributes [NullableContextAttribute](./system.runtime.compilerservices.nullablecontextattribute.md), [NullableAttribute](./system.runtime.compilerservices.nullableattribute.md)

## Constructors

### **MapNameTransform(IDictionary&lt;String, String&gt;)**

Creates an object of type MapNameTransform.

```csharp
public MapNameTransform(IDictionary<string, string> map)
```

#### Parameters

`map` [IDictionary&lt;String, String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.idictionary-2)<br>
The mapping of the existing names to the new names.

## Methods

### **Apply(String)**

Applies the transform to the name.

```csharp
public string Apply(string name)
```

#### Parameters

`name` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Name upon which to apply the tranform.

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The transformed name.
