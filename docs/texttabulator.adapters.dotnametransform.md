# DotNameTransform

Namespace: TextTabulator.Adapters

A name transform that, when given names separated by dots ('.'), can capitalize the first letter of words and replace dots.

```csharp
public class DotNameTransform : SeparatorNameTransform, INameTransform
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [SeparatorNameTransform](./texttabulator.adapters.separatornametransform.md) → [DotNameTransform](./texttabulator.adapters.dotnametransform.md)<br>
Implements [INameTransform](./texttabulator.adapters.inametransform.md)

## Constructors

### **DotNameTransform(Boolean, Boolean, Nullable&lt;Char&gt;)**

Creates an object of type DotNameTransform.

```csharp
public DotNameTransform(bool capitalizeFirstLetterOfFirstWord, bool capitalizeFirstLetterOfSubsequentWords, Nullable<char> dotReplacement)
```

#### Parameters

`capitalizeFirstLetterOfFirstWord` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
True to capitalize the first letter of the first word, false otherwise.

`capitalizeFirstLetterOfSubsequentWords` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
True to capitalize the first letter of subsequent words, false otherwise.

`dotReplacement` [Nullable&lt;Char&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Specifies a character used to replace a dot. Pass in null to not replace a dot.
