# TypeFormatter

Namespace: TextTabulator.Adapters

Default implementation of ITypeFormatter that formats values based on their type.

```csharp
public class TypeFormatter : ITypeFormatter
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [TypeFormatter](./texttabulator.adapters.typeformatter.md)<br>
Implements [ITypeFormatter](./texttabulator.adapters.itypeformatter.md)

## Constructors

### **TypeFormatter(Dictionary&lt;Type, Func&lt;Object, String&gt;&gt;)**

Creates an object of type TypeFormatter.

```csharp
public TypeFormatter(Dictionary<Type, Func<object, string>> formatters)
```

#### Parameters

`formatters` [Dictionary&lt;Type, Func&lt;Object, String&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<br>
A mapping of types to formatting functions. Types not in the Dictionary will use default formatting. Pass null to use default formatting for all types.

## Methods

### **FormatTypeValue(Object)**

Called to format a value of a specific type to a string representation.

```csharp
public string FormatTypeValue(object value)
```

#### Parameters

`value` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>
Value to format.

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
String representation of the value.
