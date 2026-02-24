# KebabNameTransform

Namespace: TextTabulator.Adapters

A name transform that, when given kebab case names, can capitalize the first letter of words and replace dashes.

```csharp
public class KebabNameTransform : SeparatorNameTransform, INameTransform
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [SeparatorNameTransform](./texttabulator.adapters.separatornametransform.md) → [KebabNameTransform](./texttabulator.adapters.kebabnametransform.md)<br>
Implements [INameTransform](./texttabulator.adapters.inametransform.md)

## Constructors

### **KebabNameTransform(Boolean, Boolean, Nullable&lt;Char&gt;)**

Creates an object of type KebabNameTransform.

```csharp
public KebabNameTransform(bool capitalizeFirstLetterOfFirstWord, bool capitalizeFirstLetterOfSubsequentWords, Nullable<char> dashReplacement)
```

#### Parameters

`capitalizeFirstLetterOfFirstWord` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
True to capitalize the first letter of the first word, false otherwise.

`capitalizeFirstLetterOfSubsequentWords` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
True to capitalize the first letter of subsequent words, false otherwise.

`dashReplacement` [Nullable&lt;Char&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Specifies a character used to replace a dash. Pass in null to not replace a dash.
