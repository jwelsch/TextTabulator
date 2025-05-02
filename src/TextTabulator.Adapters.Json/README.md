# TextTabulator.Adapters.Json

This is an auxillary library for TextTabulator that uses the [System.Text.Json](https://github.com/dotnet/runtime/tree/main/src/libraries/System.Text.Json) library to parse JSON data. It can then provide the parsed JSON data to TextTabulator for consumption.

## Installation

First, install the [TextTabulator main package](https://github.com/jwelsch/TextTabulator) and then this one.

Install the TextTabulator.Adapters.Json Nuget package in your project.

```
nuget install JWelsch.TextTabulator.Adapters.Json
```

## How to use

You can call the code like this:

```
using TextTabulator;
using TextTabulator.Adapters.Json;

var jsonData =
"""
[
    {
        "name": "Tyrannosaurus Rex",
        "weight": 6.7,
        "diet": "Carnivore",
        "extinction": 66
    },
    {
        "name": "Triceratops",
        "weight": 8,
        "diet": "Herbivore",
        "extinction": 66
    },
    {
        "name": "Apatosaurus",
        "weight": 33,
        "diet": "Herbivore",
        "extinction": 147
    },
    {
        "name": "Archaeopteryx",
        "weight": 0.001,
        "diet": "Omnivore",
        "extinction": 147
    },
    {
        "name": "Anklyosaurus",
        "weight": 4.8,
        "diet": "Herbivore",
        "extinction": 66
    },
    {
        "name": "Stegosaurus",
        "weight": 3.8,
        "diet": "Herbivore",
        "extinction": 147
    },
    {
        "name": "Hadrosaurus",
        "weight": 3,
        "diet": "Herbivore",
        "extinction": 66
    }
]
""";

var jsonAdapter = new JsonTabulatorAdapter(jsonData, true);

var tabulator = new Tabulator();
var table = tabulator.Tabulate(jsonAdapter);

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

## JSON Format

The `JsonTabulatorAdapter` can only parse JSON data in a specific format. The JSON data must be an array consisting of homogenous JSON objects only. If the JSON document root is not an array an exception will be thrown. If the JSON objects are not homogenous an exception will be thrown.

The data should be in the following format:

```
[
  {
    "field1": value1A,
    "field2": "value2A"
  },
  {
    "field1": value1B,
    "field2": "value2B"
  },
  {
    "field1": value1C,
    "field2": "value2C"
  }
]
```

## Header Names

When constructing the table, the names of the JSON properties are used as the header names. The names can be transformed by passing a transform as the `nameTransform` parameter in the `JsonTabulatorAdapterOptions` constructor.

There are various transforms available to alter the names:

- `PassThruNameTransform`: Transform that does not alter the name. This is the default.
- `MapNameTransform`: Transform that maps an existing name to a new name.
- `KebabNameTransform`: Transform that, when given kebab case names, can capitalize the first letter of words and replace dashes.
- `SnakeNameTransform`: Transform that, when given snake case names, can capitalize the first letter of words and replace underscores.
- `CamelNameTransform`: Transform that, when given camel case names, can capitalize the first letter of words and insert separators.
- `PascalNameTransform`: Transform that, when given Pascal case names, can capitalize the first letter of words and insert separators.

## Public API

The API consists of the `TextTabulator.Adapters.Json.JsonTabulatorAdapter` class. `JsonTabulatorAdapter` derives from the `IJsonTabulatorAdapter` to allow easy mocking for testing.

### `TextTabulator.Adapters.Json.JsonTabulatorAdapter`

The adapter class that accepts JSON data and presents the data that it reads in a format that `TextTabulator.Tabulate` can consume.

**Constructors**

> `public JsonTabulatorAdapter..ctor(Func<Stream> jsonStreamProvider, JsonTabulatorAdapterOptions options = default)`

Parameters
- `Func<Stream> jsonStreamProvider`: A method that returns a Stream containing UTF-8 encoded JSON data.
- `JsonTabulatorAdapterOptions options`: Options for the adapter.

> `public JsonTabulatorAdapter..ctor(Stream jsonStream, JsonReaderOptions options = default)`

Parameters
- `Stream jsonStream`: A Stream containing UTF-8 encoded JSON data.
- `JsonReaderOptions options`: Options for the adapter.

> `public JsonTabulatorAdapter..ctor(string json, JsonReaderOptions options = default)`

Parameters
- `string json`: A string containing raw JSON data.
- `JsonReaderOptions options`: Options for the adapter.

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

### `TextTabulator.Adapters.Json.JsonTabulatorAdapterOptions`

Options to allow configuration of the JsonTabulatorAdapter class.

**Constructors**

> `public JsonTabulatorAdapterOptions(INameTransform? propertyNameTransform = null, JsonReaderOptions jsonReaderOptions = default)`

Parameters
- `INameTransform? propertyNameTransform`: Transform to apply to JSON property names. Passing null will cause the JSON property names to not be altered.
- `JsonReaderOptions jsonReaderOptions`: Options that define customized behavior of the Utf8JsonReader that differs from the JSON RFC (for example, how to handle comments or maximum depth allowed when reading). By default, the Utf8JsonReader follows the JSON RFC strictly; comments within the JSON are invalid, and the maximum depth is 64.

**Properties**

> `INameTransform PropertyNameTransform { get; }`

Gets the transform to apply to JSON property names.

> `JsonReaderOptions JsonReaderOptions { get; }`

Gets options that define customized behavior of the Utf8JsonReader that differs from the JSON RFC (for example, how to handle comments or maximum depth allowed when reading). By default, the Utf8JsonReader follows the JSON RFC strictly; comments within the JSON are invalid, and the maximum depth is 64.

### `INameTransform`

Interface for defining a transform for a name.

**Methods**

> `string Apply(string name)`

Applies the transform to the name.

Parameters
- `string name`: Name upon which to apply the tranform.

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

Applies the transform to the name.

Parameters
- `string name`: Name upon which to apply the tranform.

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

Applies the transform to the name.

Parameters
- `string name`: Name upon which to apply the tranform.

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

Applies the transform to the name.

Parameters
- `string name`: Name upon which to apply the tranform.

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

Applies the transform to the name.

Parameters
- `string name`: Name upon which to apply the tranform.

Return
- `string`: The transformed name.

### `MapNameTransform`

A name transform that maps a name in JSON to a new name.

**Constructors**

`public MapNameTransform(IDictionary<string, string> map)`

Parameters
- `IDictionary<string, string> map`: The mapping of the existing names to the new names.

**Methods**

> `string Apply(string name)`

Applies the transform to the name.

Parameters
- `string name`: Name upon which to apply the tranform.

Return
- `string`: The transformed name.

### `PassThruNameTransform`

A name transform that does not alter the name.

**Constructors**

`public PassThruNameTransform()`

Parameters
- None

**Methods**

> `string Apply(string name)`

Applies the transform to the name.

Parameters
- `string name`: Name upon which to apply the tranform.

Return
- `string`: The transformed name.
