# SeparatorNameTransform

Namespace: TextTabulator.Adapters

Abstract base class of name transform that can capitalize the first letter of words and replace separator characters.

```csharp
public abstract class SeparatorNameTransform : INameTransform
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [SeparatorNameTransform](./texttabulator.adapters.separatornametransform.md)<br>
Implements [INameTransform](./texttabulator.adapters.inametransform.md)

## Constructors

### **SeparatorNameTransform(Char, Boolean, Boolean, Nullable&lt;Char&gt;)**

Creates an object of type SeparatorNameTransform.

```csharp
protected SeparatorNameTransform(char separator, bool capitalizeFirstLetterOfFirstWord, bool capitalizeFirstLetterOfSubsequentWords, Nullable<char> separatorReplacement)
```

#### Parameters

`separator` [Char](https://docs.microsoft.com/en-us/dotnet/api/system.char)<br>

`capitalizeFirstLetterOfFirstWord` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
True to capitalize the first letter of the first word, false otherwise.

`capitalizeFirstLetterOfSubsequentWords` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
True to capitalize the first letter of subsequent words, false otherwise.

`separatorReplacement` [Nullable&lt;Char&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Specifies a character used to replace a separator. Pass in null to not replace a separator.

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
