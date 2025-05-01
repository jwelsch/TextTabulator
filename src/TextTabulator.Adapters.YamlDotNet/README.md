# TextTabulator.Adapters.YamlDotNet

This is an auxillary library for TextTabulator that provides an integration with the popular [YamlDotNet](https://github.com/aaubry/YamlDotNet) library that allows TextTabulator to consume YAML data.

## Installation

First, install the [TextTabulator main package](https://github.com/jwelsch/TextTabulator) and then this one.

Install the TextTabulator.Adapters.YamlDotNet Nuget package in your project.

```
nuget install JWelsch.TextTabulator.Adapters.YamlDotNet
```

## How to Use

You can call the code like this:

```
using TextTabulator;
using TextTabulator.Adapters.YamlDotNet;

var yamlData =
"""
- name: Tyrannosaurus Rex
  weight: 6.7
  diet: Carnivore
  extinction: 66
- name: Triceratops
  weight: 8
  diet: Herbivore
  extinction: 66
- name: Archaeopteryx
  weight: 0.001
  diet: Omnivore
  extinction: 147
""";

using var textReader = new StringReader(csvData);
var parser = new Parser(textReader);

var yamlAdapter = new YamlDotNetTabulatorAdapter(parser);

var tabulator = new Tabulator();
var table = tabulator.Tabulate(yamlAdapter);

Console.WriteLine(table);
```

This will produce the output:
```
-----------------------------------------------
|Name             |Weight|Diet     |Extinction|
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

## YAML Format

The `YamlTabulatorAdapter` can only parse YAML data in a specific format. The YAML data must be a single sequence of homogeneous YAML mappings only.

The data should be in the following format:

```
- field1: value1A
  field2: value2A
- field1: value1B
  field2: value2B
- field1: value1C
  field2: value2C
```

## Header Names

When constructing the table, the names of the YAML nodes are used as the header names. The names can be transformed by passing a transform as the `nameTransform` parameter in the `YamlTabulatorAdapterOptions` constructor.

There are various transforms available to alter the property names:

- `PassThruNameTransform`: Transform that does not alter the name. This is the default.
- `MapNameTransform`: Transform that maps a name to a new name.
- `KebabNameTransform`: Transform that, when given kebab case names, can capitalize the first letter of words and replace dashes.
- `SnakeNameTransform`: Transform that, when given snake case names, can capitalize the first letter of words and replace underscores.
- `DotNameTransform`: Transform that, when given names separated by dots ('.'), can capitalize the first letter of words and replace dots.
- `CamelNameTransform`: Transform that, when given camel case names, can capitalize the first letter of words and insert separators.
- `PascalNameTransform`: Transform that, when given Pascal case names, can capitalize the first letter of words and insert separators.


## Public API

The API consits of the `TextTabulator.Adapters.YamlDotNet.YamlDotNetTabulatorAdapter` class. `YamlDotNetTabulatorAdapter` derives from the `IYamlDotNetTabulatorAdapter` to allow easy mocking for testing.

### `TextTabulator.Adapters.YamlDotNet.YamlDotNetTabulatorAdapter`

The adapter class that accepts a `YamlDotNet` object and presents the data that it reads in a format that `TextTabulator.Tabulate` can consume.

**Constructors**

> `public YamlDotNetTabulatorAdapter(YamlDotNet.Core.Parser parser, YamlDotNetTabulatorAdapterOptions? options = null)`

Parameters
- `YamlDotNet.Core.Parser parser`: A YamlDotNet.Core.Parser object with the YAML data to process.
- `YamlTabulatorAdapterOptions options`: Options for the adapter.

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

### `TextTabulator.Adapters.Yaml.YamlTabulatorAdapterOptions`

Options to allow configuration of the YamlTabulatorAdapter class.

**Constructors**

> `public YamlTabulatorAdapterOptions(INameTransform? nameTransform = null)`

Parameters
- `INameTransform? nodeNameTransform`: Transform to apply to YAML node names. Passing null will cause the YAML node names to not be altered.

**Properties**

> `INameTransform nameTransform { get; }`

Gets the transform to apply to names.

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
