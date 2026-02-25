# CamelNameTransform

Namespace: TextTabulator.Adapters

A name transform that, when given camel case names, can capitalize the first letter of words and insert separators.

```csharp
public class CamelNameTransform : NoSeparatorNameTransform, INameTransform
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [NoSeparatorNameTransform](./texttabulator.adapters.noseparatornametransform.md) → [CamelNameTransform](./texttabulator.adapters.camelnametransform.md)<br>
Implements [INameTransform](./texttabulator.adapters.inametransform.md)

## Constructors

### **CamelNameTransform(Boolean, Boolean, Nullable&lt;Char&gt;)**

Creates an object of type CamelNameTransform.

```csharp
public CamelNameTransform(bool capitalizeFirstLetterOfFirstWord, bool capitalizeFirstLetterOfSubsequentWords, Nullable<char> separator)
```

#### Parameters

`capitalizeFirstLetterOfFirstWord` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
True to capitalize the first letter of the first word, false otherwise.

`capitalizeFirstLetterOfSubsequentWords` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
True to capitalize the first letter of subsequent words, false otherwise.

`separator` [Nullable&lt;Char&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Specifies a character used as a separator. Pass in null to not use a separator.
