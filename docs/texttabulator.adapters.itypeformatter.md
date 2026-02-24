# ITypeFormatter

Namespace: TextTabulator.Adapters

Interface for formatting values of different types to strings.

```csharp
public interface ITypeFormatter
```

Attributes [NullableContextAttribute](./system.runtime.compilerservices.nullablecontextattribute.md)

## Methods

### **FormatTypeValue(Object)**

Called to format a value of a specific type to a string representation.

```csharp
string FormatTypeValue(object value)
```

#### Parameters

`value` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>
Value to format.

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
String representation of the value.
