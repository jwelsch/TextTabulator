# TextTabulator.Adapters.Xml

This is an auxillary library for TextTabulator that uses [System.Xml.XmlReader](https://github.com/dotnet/runtime/blob/main/src/libraries/System.Private.Xml/src/System/Xml/Core/XmlReader.cs) to parse XML data. It can then provide the parsed XML data to TextTabulator for consumption.

## Installation

First, install the [TextTabulator main package](https://github.com/jwelsch/TextTabulator) and then this one.

Install the TextTabulator.Adapters.Xml Nuget package in your project.

```
nuget install JWelsch.TextTabulator.Adapters.Xml
```

## How to use

You can call the code like this:

```
using TextTabulator;
using TextTabulator.Adapters.Xml;

var xmlData =
"""
<?xml version="1.0" encoding="UTF-8"?>
<dinosaurs>
    <dinosaur>
        <name>Tyrannosaurus Rex</name>
        <weight>6.7</weight>
        <diet>Carnivore</diet>
        <extinction>66</extinction>
    </dinosaur>
    <dinosaur>
        <name>Triceratops</name>
        <weight>8</weight>
        <diet>Herbivore</diet>
        <extinction>66</extinction>
    </dinosaur>
    <dinosaur>
        <name>Apatosaurus</name>
        <weight>33</weight>
        <diet>Herbivore</diet>
        <extinction>147</extinction>
    </dinosaur>
    <dinosaur>
        <name>Archaeopteryx</name>
        <weight>0.001</weight>
        <diet>Omnivore</diet>
        <extinction>147</extinction>
    </dinosaur>
    <dinosaur>
        <name>Anklyosaurus</name>
        <weight>4.8</weight>
        <diet>Herbivore</diet>
        <extinction>66</extinction>
    </dinosaur>
    <dinosaur>
        <name>Stegosaurus</name>
        <weight>3.8</weight>
        <diet>Herbivore</diet>
        <extinction>147</extinction>
    </dinosaur>
    <dinosaur>
        <name>Hadrosaurus</name>
        <weight>3</weight>
        <diet>Herbivore</diet>
        <extinction>66</extinction>
    </dinosaur>
</dinosaurs>
""";

var xmlAdapter = new XmlTabulatorAdapter(xmlData, true);

var tabulator = new Tabulator();
var table = tabulator.Tabulate(xmlAdapter);

Console.WriteLine(table);
```

This will produce the output:
```
-----------------------------------------------
|name             |weight|diet     |extinction|
|-----------------+------+---------+----------|
|Tyrannosaurus Rex|6.7   |Carnivore|66        |
|-----------------+------+---------+----------|
|Triceratops      |8     |Herbivore|66        |
|-----------------+------+---------+----------|
|Apatosaurus      |33    |Herbivore|147       |
|-----------------+------+---------+----------|
|Archaeopteryx    |0.001 |Omnivore |147       |
|-----------------+------+---------+----------|
|Anklyosaurus     |4.8   |Herbivore|66        |
|-----------------+------+---------+----------|
|Stegosaurus      |3.8   |Herbivore|147       |
|-----------------+------+---------+----------|
|Hadrosaurus      |3     |Herbivore|66        |
-----------------------------------------------
```

## XML Format

The `XmlTabulatorAdapter` can only parse XML data in a specific format. The XML data must be a node that is a list of homogenous XML nodes only. If the XML document root is not a node that is a list an exception will be thrown. If the XML nodes are not homogenous an exception will be thrown.

The data should be in the following format:

```
<?xml version="1.0" encoding="UTF-8"?>
<list>
    <object>
        <value1>value1A</value1>
        <value2>value2A</value2>
    </object>
    <object>
        <value1>value1B</value1>
        <value2>value2B</value2>
    </object>
    <object>
        <value1>value1C</value1>
        <value2>value2C</value2>
    </object>
    ...
</list>
```

## Header Names

When constructing the table, the names of the XML nodes are used as the header names. The names can be transformed by passing a transform as the `nameTransform` parameter in the `XmlTabulatorAdapterOptions` constructor.

There are various transforms available to alter the property names:

- `PassThruNameTransform`: Transform that does not alter the name. This is the default.
- `MapNameTransform`: Transform that maps a name to a new name.
- `KebabNameTransform`: Transform that, when given kebab case names, can capitalize the first letter of words and replace dashes.
- `SnakeNameTransform`: Transform that, when given snake case names, can capitalize the first letter of words and replace underscores.
- `DotNameTransform`: Transform that, when given names separated by dots ('.'), can capitalize the first letter of words and replace dots.
- `CamelNameTransform`: Transform that, when given camel case names, can capitalize the first letter of words and insert separators.
- `PascalNameTransform`: Transform that, when given Pascal case names, can capitalize the first letter of words and insert separators.

## Public API

The API consists of the `TextTabulator.Adapters.Xml.XmlTabulatorAdapter` class. `XmlTabulatorAdapter` derives from the `IXmlTabulatorAdapter` to allow easy mocking for testing.

### `TextTabulator.Adapters.Xml.XmlTabulatorAdapter`

The adapter class that accepts XML data and presents the data that it reads in a format that `TextTabulator.Tabulate` can consume.

**Constructors**

> `public XmlTabulatorAdapter..ctor(Func<Stream> xmlStreamProvider, XmlTabulatorAdapterOptions options = default)`

Parameters
- `Func<Stream> xmlStreamProvider`: A method that returns a Stream containing UTF-8 encoded XML data.
- `XmlTabulatorAdapterOptions options`: Options for the adapter.

> `public XmlTabulatorAdapter..ctor(Stream xmlStream, XmlReaderOptions options = default)`

Parameters
- `Stream xmlStream`: A Stream containing UTF-8 encoded XML data.
- `XmlReaderOptions options`: Options for the adapter.

> `public XmlTabulatorAdapter..ctor(string xml, XmlReaderOptions options = default)`

Parameters
- `string xml`: A string containing raw XML data.
- `XmlReaderOptions options`: Options for the adapter.

**Methods**

> `public IEnumerable<string>? GetHeaderStrings()`

Called by `Tabulator.Tabulate` to return the header strings, if any, of the data. If the data does not contain headers, then null should be returned.

Parameters
- None

Return

- `IEnumerable<string>?`: An enumerable containing the header strings, or null if the data did not have headers.

> `public IEnumerable<IEnumerable<string>> GetValueStrings()`

Called to return the row values. The outer enumeration is the rows, while the inner enumeration contains the values in each row. Can be an empty enumeration if the data contains no rows.

Parameters
- None

Return
- `IEnumerable<IEnumerable<string>>`: An enumerable containing the values for each row.

### `TextTabulator.Adapters.Xml.XmlTabulatorAdapterOptions`

Options to allow configuration of the XmlTabulatorAdapter class.

**Constructors**

> `public XmlTabulatorAdapterOptions(INameTransform? nameTransform = null, XmlReaderOptions xmlReaderOptions = default)`

Parameters
- `INameTransform? nameTransform`: Transform to apply to names. Passing null will cause the names to not be altered.
- `XmlReaderOptions xmlReaderOptions`: Options that define customized behavior of the Utf8XmlReader that differs from the XML RFC (for example, how to handle comments or maximum depth allowed when reading). By default, the Utf8XmlReader follows the XML RFC strictly; comments within the XML are invalid, and the maximum depth is 64.

**Properties**

> `INameTransform nameTransform { get; }`

Gets the transform to apply to names.

> `XmlReaderOptions XmlReaderOptions { get; }`

Gets options that define customized behavior of the Utf8XmlReader that differs from the XML RFC (for example, how to handle comments or maximum depth allowed when reading). By default, the Utf8XmlReader follows the XML RFC strictly; comments within the XML are invalid, and the maximum depth is 64.

### `INameTransform`

Interface for defining a transform for a name.

**Methods**

> `string Apply(string name)`

Applies the transform to the property name.

Parameters
- `string name`: Property name upon which to apply the tranform.

Return
- `string`: The transformed name.

### `KebabNameTransform`

A name transform that, when given kebab case names, can capitalize the first letter of words and replace dashes.

**Constructors**

`public KebabNameTransform(bool capitalizeFirstLetterOfFirstWord = true, bool capitalizeFirstLetterOfSubsequentWords = true, char? dashReplacement = ' ')`

Parameters
- `bool capitalizeFirstLetterOfFirstWord`: True to capitalize the first letter of the first word, false otherwise.
- `bool capitalizeFirstLetterOfSubsequentWords`: True to capitalize the first letter of subsequent words, false otherwise.
- `char? dashReplacement`: Specifies a character used to replace a dash. Pass in null to not replace a dash.

**Methods**

> `string Apply(string name)`

Applies the transform to the property name.

Parameters
- `string name`: Property name upon which to apply the tranform.

Return
- `string`: The transformed name.

### `SnakeNameTransform`

A name transform that, when given snake case names, can capitalize the first letter of words and replace underscores.

**Constructors**

`public SnakeNameTransform(bool capitalizeFirstLetterOfFirstWord = true, bool capitalizeFirstLetterOfSubsequentWords = true, char? underscoreReplacement = ' ')`

Parameters
- `bool capitalizeFirstLetterOfFirstWord`: True to capitalize the first letter of the first word, false otherwise.
- `bool capitalizeFirstLetterOfSubsequentWords`: True to capitalize the first letter of subsequent words, false otherwise.
- `char? underscoreReplacement`: Specifies a character used to replace an underscore. Pass in null to not replace an underscore.

**Methods**

> `string Apply(string name)`

Applies the transform to the property name.

Parameters
- `string name`: Property name upon which to apply the tranform.

Return
- `string`: The transformed name.

### `DotNameTransform`

A name transform that, when given names separated by dots ('.'), can capitalize the first letter of words and replace dots.

**Constructors**

DotNameTransform(bool capitalizeFirstLetterOfFirstWord = true, bool capitalizeFirstLetterOfSubsequentWords = true, char? dotReplacement = ' ')`

Parameters
- `bool capitalizeFirstLetterOfFirstWord`: True to capitalize the first letter of the first word, false otherwise.
- `bool capitalizeFirstLetterOfSubsequentWords`: True to capitalize the first letter of subsequent words, false otherwise.
- `char? dotReplacement`: Specifies a character used to replace a dot. Pass in null to not replace a dot.

- **Methods**

> `string Apply(string name)`

Applies the transform to the property name.

Parameters
- `string name`: Property name upon which to apply the tranform.

Return
- `string`: The transformed name.

### `CamelNameTransform`

A name transform that, when given camel case names, can capitalize the first letter of words and insert separators.

**Constructors**

`public CamelNameTransform(bool capitalizeFirstLetterOfFirstWord = true, bool capitalizeFirstLetterOfSubsequentWords = true, char? separator = ' ')`

Parameters
- `bool capitalizeFirstLetterOfFirstWord`: True to capitalize the first letter of the first word, false otherwise.
- `bool capitalizeFirstLetterOfSubsequentWords`: True to capitalize the first letter of subsequent words, false otherwise.
- `char? separator`: Specifies a character used as a separator. Pass in null to not use a separator.

**Methods**

> `string Apply(string name)`

Applies the transform to the property name.

Parameters
- `string name`: Property name upon which to apply the tranform.

Return
- `string`: The transformed name.

### `PascalNameTransform`

A name transform that, when given Pascal case names, can capitalize the first letter of words and insert separators.

**Constructors**

`public CamelNameTransform(bool capitalizeFirstLetterOfFirstWord = true, bool capitalizeFirstLetterOfSubsequentWords = true, char? separator = ' ')`

Parameters
- `bool capitalizeFirstLetterOfFirstWord`: True to capitalize the first letter of the first word, false otherwise.
- `bool capitalizeFirstLetterOfSubsequentWords`: True to capitalize the first letter of subsequent words, false otherwise.
- `char? separator`: Specifies a character used as a separator. Pass in null to not use a separator.

**Methods**

> `string Apply(string name)`

Applies the transform to the property name.

Parameters
- `string name`: Property name upon which to apply the tranform.

Return
- `string`: The transformed name.

### `MapNameTransform`

A name transform that maps a property name in XML to a new name.

**Constructors**

`public MapNameTransform(IDictionary<string, string> map)`

Parameters
- `IDictionary<string, string> map`: The mapping of the property names in XML to the new names.

**Methods**

> `string Apply(string name)`

Applies the transform to the property name.

Parameters
- `string name`: Property name upon which to apply the tranform.

Return
- `string`: The transformed name.

### `PassThruNameTransform`

A name transform that does not alter the property name.

**Constructors**

`public PassThruNameTransform()`

Parameters
- None

**Methods**

> `string Apply(string name)`

Applies the transform to the property name.

Parameters
- `string name`: Property name upon which to apply the tranform.

Return
- `string`: The transformed name.
