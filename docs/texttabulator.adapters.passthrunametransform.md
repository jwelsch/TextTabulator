# PassThruNameTransform

Namespace: TextTabulator.Adapters

A name transform that does not alter the property name.

```csharp
public class PassThruNameTransform : INameTransform
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [PassThruNameTransform](./texttabulator.adapters.passthrunametransform.md)<br>
Implements [INameTransform](./texttabulator.adapters.inametransform.md)

## Constructors

### **PassThruNameTransform()**

```csharp
public PassThruNameTransform()
```

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
