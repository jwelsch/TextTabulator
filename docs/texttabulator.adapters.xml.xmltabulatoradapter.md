# XmlTabulatorAdapter

Namespace: TextTabulator.Adapters.Xml

The adapter class that accepts XML data and presents the data that it reads in a format that TextTabulator.Tabulate can consume.
 
 The data should be in the following format:
 
 &lt;?xml version="1.0" encoding="UTF-8"?&gt;
 &lt;list&gt;
 &lt;object&gt;
 &lt;value1&gt;value1A&lt;/value1&gt;
 &lt;value2&gt;value2A&lt;/value2&gt;
 &lt;/object&gt;
 &lt;object&gt;
 &lt;value1&gt;value1B&lt;/value1&gt;
 &lt;value2&gt;value2B&lt;/value2&gt;
 &lt;/object&gt;
 &lt;object&gt;
 &lt;value1&gt;value1C&lt;/value1&gt;
 &lt;value2&gt;value2C&lt;/value2&gt;
 &lt;/object&gt;
 ...
 &lt;/list&gt;

```csharp
public class XmlTabulatorAdapter : IXmlTabulatorAdapter, TextTabulator.Adapters.ITabulatorAdapter
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [XmlTabulatorAdapter](./texttabulator.adapters.xml.xmltabulatoradapter.md)<br>
Implements [IXmlTabulatorAdapter](./texttabulator.adapters.xml.ixmltabulatoradapter.md), ITabulatorAdapter<br>
Attributes [NullableContextAttribute](./system.runtime.compilerservices.nullablecontextattribute.md), [NullableAttribute](./system.runtime.compilerservices.nullableattribute.md)

## Constructors

### **XmlTabulatorAdapter(Func&lt;Stream&gt;, XmlTabulatorAdapterOptions)**

Creates an object of type XmlTabulatorAdapter.
 In order for the adpater to function correctly, make sure the XML data only contains a list of homogeneous XML objects.

```csharp
public XmlTabulatorAdapter(Func<Stream> xmlStreamProvider, XmlTabulatorAdapterOptions options)
```

#### Parameters

`xmlStreamProvider` [Func&lt;Stream&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-1)<br>
Function that provides a stream containing a list containing homogeneous objects of UTF-8 encoded XML data.

`options` [XmlTabulatorAdapterOptions](./texttabulator.adapters.xml.xmltabulatoradapteroptions.md)<br>
Options for the adapter.

### **XmlTabulatorAdapter(Stream, XmlTabulatorAdapterOptions)**

Creates an object of type XmlTabulatorAdapter.
 In order for the adpater to function correctly, make sure the XML data only contains a list of homogeneous XML objects.

```csharp
public XmlTabulatorAdapter(Stream xmlStream, XmlTabulatorAdapterOptions options)
```

#### Parameters

`xmlStream` [Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>
Stream containing UTF-8 encoded XML data.

`options` [XmlTabulatorAdapterOptions](./texttabulator.adapters.xml.xmltabulatoradapteroptions.md)<br>
Options for the adapter.

### **XmlTabulatorAdapter(String, XmlTabulatorAdapterOptions)**

Creates an object of type XmlTabulatorAdapter.
 In order for the adpater to function correctly, make sure the XML data only contains a list of homogeneous XML objects.

```csharp
public XmlTabulatorAdapter(string xml, XmlTabulatorAdapterOptions options)
```

#### Parameters

`xml` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
String containing raw XML data.

`options` [XmlTabulatorAdapterOptions](./texttabulator.adapters.xml.xmltabulatoradapteroptions.md)<br>
Options for the adapter.

## Methods

### **GetHeaderStrings()**

Called to return the header strings, if any, of the data. If the data does not contain headers, then null should be returned.

```csharp
public IEnumerable<string> GetHeaderStrings()
```

#### Returns

[IEnumerable&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
An enumerable containing the header strings for the table, or null if the data contains no header strings.

### **GetValueStrings()**

Called to return the row values. The outer enumeration is the rows, while the inner enumeration contains the values in each row.
 Can be an empty enumeration if the data contains no rows.

```csharp
public IEnumerable<IEnumerable<string>> GetValueStrings()
```

#### Returns

[IEnumerable&lt;IEnumerable&lt;String&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
An enumerable containing the rows and the values within each row.
