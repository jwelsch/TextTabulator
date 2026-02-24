# INameTransform

Namespace: TextTabulator.Adapters

Interface for defining a transform for a name.

```csharp
public interface INameTransform
```

Attributes [NullableContextAttribute](./system.runtime.compilerservices.nullablecontextattribute.md)

## Methods

### **Apply(String)**

Applies the transform to the name.

```csharp
string Apply(string name)
```

#### Parameters

`name` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Name upon which to apply the tranform.

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The transformed name.
