# PascalNameTransform

Namespace: TextTabulator.Adapters

A name transform that, when given Pascal case names, can capitalize the first letter of words and insert separators.

```csharp
public class PascalNameTransform : NoSeparatorNameTransform, INameTransform
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [NoSeparatorNameTransform](./texttabulator.adapters.noseparatornametransform.md) → [PascalNameTransform](./texttabulator.adapters.pascalnametransform.md)<br>
Implements [INameTransform](./texttabulator.adapters.inametransform.md)

## Constructors

### **PascalNameTransform(Boolean, Boolean, Nullable&lt;Char&gt;)**

Creates an object of type PascalNameTransform.

```csharp
public PascalNameTransform(bool capitalizeFirstLetterOfFirstWord, bool capitalizeFirstLetterOfSubsequentWords, Nullable<char> separator)
```

#### Parameters

`capitalizeFirstLetterOfFirstWord` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
True to capitalize the first letter of the first word, false otherwise.

`capitalizeFirstLetterOfSubsequentWords` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
True to capitalize the first letter of subsequent words, false otherwise.

`separator` [Nullable&lt;Char&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Specifies a character used as a separator.
