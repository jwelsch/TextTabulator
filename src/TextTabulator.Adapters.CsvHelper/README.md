[![Build](https://github.com/jwelsch/TextTabulator/actions/workflows/build.yml/badge.svg)](https://github.com/jwelsch/TextTabulator/actions/workflows/build.yml)
[![Publish](https://github.com/jwelsch/TextTabulator/actions/workflows/publish.yml/badge.svg)](https://github.com/jwelsch/TextTabulator/actions/workflows/publish.yml)
[![NuGet](https://img.shields.io/nuget/v/Jwelsch.TextTabulator.Adapters.CsvHelper.svg)](https://www.nuget.org/packages/Jwelsch.TextTabulator.Adapters.CsvHelper)

# TextTabulator.Adapters.CsvHelper

This is an auxillary library for TextTabulator that provides an integration with the popular [CsvHelper](https://github.com/JoshClose/CsvHelper) library that allows TextTabulator to consume CSV data.

## Installation

First, install the [TextTabulator main package](https://github.com/jwelsch/TextTabulator) and then this one.

Install the TextTabulator.Adapters.CsvHelper Nuget package in your project.

```
nuget install JWelsch.TextTabulator.Adapters.CsvHelper
```

## How to use

You can call the code like this:

```
using CsvHelper;
using System.IO;
using TextTabulator;
using TextTabulator.Adapters.CsvHelper;

var csvData =
@"Name,Weight (tons),Diet,Extinction
Tyrannosaurus Rex,6.7,Carnivore,66 mya
Triceratops,8,Herbivore,66 mya
Apatosaurus,33,Herbivore,147 mya
Archaeopteryx,0.001,Omnivore,147 mya
Anklyosaurus,4.8,Herbivore,66 mya
Stegosaurus,3.8,Herbivore,147 mya
Hadrosaurus,3,Herbivore,66 mya
";

using var textReader = new StringReader(csvData);
using var csvReader = new CsvReader(textReader, CultureInfo.InvariantCulture);
var csvAdapter = new CsvHelperTabulatorAdapter(csvReader);

var tabulator = new Tabulator();
var table = tabulator.Tabulate(csvAdapter);

Console.WriteLine(table);
```

This will produce the output:
```
------------------------------------------------------
|Name             |Weight (tons)|Diet     |Extinction|
|-----------------+-------------+---------+----------|
|Tyrannosaurus Rex|6.7          |Carnivore|66 mya    |
|-----------------+-------------+---------+----------|
|Triceratops      |8            |Herbivore|66 mya    |
|-----------------+-------------+---------+----------|
|Apatosaurus      |33           |Herbivore|147 mya   |
|-----------------+-------------+---------+----------|
|Archaeopteryx    |0.001        |Omnivore |147 mya   |
|-----------------+-------------+---------+----------|
|Anklyosaurus     |4.8          |Herbivore|66 mya    |
|-----------------+-------------+---------+----------|
|Stegosaurus      |3.8          |Herbivore|147 mya   |
|-----------------+-------------+---------+----------|
|Hadrosaurus      |3            |Herbivore|66 mya    |
------------------------------------------------------
```

## Public API

The API consists of the `TextTabulator.Adapters.CsvHelper.CsvHelperTabulatorAdapter` class. `CsvHelperTabulatorAdapter` derives from the `ICsvHelperTabulatorAdapter` to allow easy mocking for testing.

### `TextTabulator.Adapters.CsvHelper.CsvHelperTabulatorAdapter`

The adapter class that accepts a `CsvHelper` object and presents the data that it reads in a format that `TextTabulator.Tabulate` can consume.

**Constructors**

> `public CsvHelperTabulatorAdapter..ctor(CsvReader csvReader, CsvHelperTabulatorAdapterOptions? options = null)`

Parameters
- `CsvReader csvReader`: The `CsvReader` object that will read the desired data.
- `CsvHelperTabulatorAdapterOptions? options`: Options for the adapter.

**Methods**

> `public IEnumerable<string>? GetHeaderStrings()`

Called by `Tabulator.Tabulate` to return the header strings, if any, of the data. If the data does not contain headers, then null should be returned.

Parameters
- None

Return

- `IEnumerable<string>?`: An enumerable containing the header strings, or null if the CSV data did not have headers.

> `public IEnumerable<IEnumerable<string>> GetValueStrings()`

Called to return the row values. The outer enumeration is the rows, while the inner enumeration contains the values in each row. Can be an empty enumeration if the data contains no rows.

Parameters
- None

Return

- `IEnumerable<IEnumerable<string>>`: An enumerable containing the values for each row.

### `TextTabulator.Adapters.CsvHelper.CsvHelperTabulatorAdapterOptions`

Options to allow configuration of the CsvHelperTabulatorAdapter class.

**Constructors**

> `public CsvHelperTabulatorAdapterOptions(INameTransform? headerNameTransform = null, bool hasHeaderRow = true)`

Parameters
- `INameTransform? headerNameTransform`: Transform to apply to CSV header names. Passing null will cause the CSV header names to not be altered.
- `bool hasHeaderRow`: True if the CSV data contains a header row, false if not. Defaults to true.

**Properties**

> `INameTransform HeaderNameTransform { get; }`

 Gets the transform to apply to CSV header names.

> `bool HasHeaderRow { get; }`

Gets whether or not the CSV data contains a header row. Defaults to true.

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
