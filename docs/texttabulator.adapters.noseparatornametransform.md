# NoSeparatorNameTransform

Namespace: TextTabulator.Adapters

Abstract base class of name transforms where the name has no separator.

```csharp
public abstract class NoSeparatorNameTransform : INameTransform
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [NoSeparatorNameTransform](./texttabulator.adapters.noseparatornametransform.md)<br>
Implements [INameTransform](./texttabulator.adapters.inametransform.md)

## Constructors

### **NoSeparatorNameTransform(Boolean, Boolean, Nullable&lt;Char&gt;)**

Creates an object of type NoSeparatorNameTransform.

```csharp
protected NoSeparatorNameTransform(bool capitalizeFirstLetterOfFirstWord, bool capitalizeFirstLetterOfSubsequentWords, Nullable<char> separator)
```

#### Parameters

`capitalizeFirstLetterOfFirstWord` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
True to capitalize the first letter of the first word, false otherwise.

`capitalizeFirstLetterOfSubsequentWords` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
True to capitalize the first letter of subsequent words, false otherwise.

`separator` [Nullable&lt;Char&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Specifies a character used as a separator. Pass in null to not use a separator.

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
