# SnakeNameTransform

Namespace: TextTabulator.Adapters

A name transform that, when given snake case names, can capitalize the first letter of words and replace underscores.

```csharp
public class SnakeNameTransform : SeparatorNameTransform, INameTransform
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [SeparatorNameTransform](./texttabulator.adapters.separatornametransform.md) → [SnakeNameTransform](./texttabulator.adapters.snakenametransform.md)<br>
Implements [INameTransform](./texttabulator.adapters.inametransform.md)

## Constructors

### **SnakeNameTransform(Boolean, Boolean, Nullable&lt;Char&gt;)**

Creates an object of type SnakeNameTransform.

```csharp
public SnakeNameTransform(bool capitalizeFirstLetterOfFirstWord, bool capitalizeFirstLetterOfSubsequentWords, Nullable<char> underscoreReplacement)
```

#### Parameters

`capitalizeFirstLetterOfFirstWord` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
True to capitalize the first letter of the first word, false otherwise.

`capitalizeFirstLetterOfSubsequentWords` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
True to capitalize the first letter of subsequent words, false otherwise.

`underscoreReplacement` [Nullable&lt;Char&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Specifies a character used to replace an underscore. Pass in null to not replace an underscore.
