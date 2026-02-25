# XmlTabulatorAdapterOptions

Namespace: TextTabulator.Adapters.Xml

Options to allow configuration of the XmlTabulatorAdapter class.

```csharp
public class XmlTabulatorAdapterOptions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [XmlTabulatorAdapterOptions](./texttabulator.adapters.xml.xmltabulatoradapteroptions.md)<br>
Attributes [NullableContextAttribute](./system.runtime.compilerservices.nullablecontextattribute.md), [NullableAttribute](./system.runtime.compilerservices.nullableattribute.md)

## Properties

### **NodeNameTransform**

Gets the transform to apply to XML node names.

```csharp
public INameTransform NodeNameTransform { get; }
```

#### Property Value

INameTransform<br>

### **XmlReaderSettings**

Gets settings that specify a set of features to support on the XmlReader object created by the Create method.

```csharp
public XmlReaderSettings XmlReaderSettings { get; }
```

#### Property Value

XmlReaderSettings<br>

### **HeaderSorter**

Gets the sorter to use for the headers.

```csharp
public IHeaderOrderSorter HeaderSorter { get; }
```

#### Property Value

IHeaderOrderSorter<br>

## Constructors

### **XmlTabulatorAdapterOptions(INameTransform, XmlReaderSettings, IHeaderOrderSorter)**

Creates an object of type XmlTabulatorAdapterOptions.

```csharp
public XmlTabulatorAdapterOptions(INameTransform nodeNameTransform, XmlReaderSettings xmlReaderSettings, IHeaderOrderSorter headerSorter)
```

#### Parameters

`nodeNameTransform` INameTransform<br>
Transform to apply to XML node names. Passing null will cause the XML node names to not be altered.

`xmlReaderSettings` XmlReaderSettings<br>
Specifies a set of features to support on the XmlReader object created by the Create method. Passing null will use an XmlReaderSettings object with default values.

`headerSorter` IHeaderOrderSorter<br>
Specifies the sorter to use for the headers. Passing null will use the default header sorter.
