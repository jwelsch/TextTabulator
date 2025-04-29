# TextTabulator.Adapters

This is an auxillary library for TextTabulator that provides a way to expose data sources to `TextTabulator.Tabulate` method. This assembly should only need to be directly referenced by your project if you are implementing the `TextTabulator.Adapters.ITabulatorAdapter` yourself.

Common data formats already have implementions for `TextTabulator.Adapters.ITabulatorAdapter`:
- CSV (using CsvHelper)
    - [Github](https://github.com/jwelsch/TextTabulator/tree/main/src/TextTabulator.Adapters.CsvHelper)
    - [Nuget](https://www.nuget.org/packages/Jwelsch.TextTabulator.Adapters.CsvHelper)
- JSON
    - [Github](https://github.com/jwelsch/TextTabulator/tree/main/src/TextTabulator.Adapters.Json)
    - [Nuget](https://www.nuget.org/packages/Jwelsch.TextTabulator.Adapters.Json)
- XML
    - [Github](https://github.com/jwelsch/TextTabulator/tree/main/src/TextTabulator.Adapters.Xml)
    - [Nuget](https://www.nuget.org/packages/Jwelsch.TextTabulator.Adapters.Xml)
- YAML (using YamlDotNet)
    - [Github](https://github.com/jwelsch/TextTabulator/tree/main/src/TextTabulator.Adapters.YamlDotNet)
    - [Nuget](https://www.nuget.org/packages/Jwelsch.TextTabulator.Adapters.YamlDotNet)
- Reflection
    - [Github](https://github.com/jwelsch/TextTabulator/tree/main/src/TextTabulator.Adapters.Reflection)
    - [Nuget](https://www.nuget.org/packages/Jwelsch.TextTabulator.Adapters.Reflection)

## Installation

First, install the [TextTabulator main package](https://github.com/jwelsch/TextTabulator) and then this one.

Install the TextTabulator.Adapters Nuget package in your project.

```
nuget install JWelsch.TextTabulator.Adapters
```

## How to Use

The main reason to use this assembly directly in your code is to provide an implementation for the `ITabulatorAdapter` interface.  See the "Public API" section for more details.

Here is an example naive implementation that takes CSV data and adapts it for consumption by the `Tabulator.Tabulate` method:

```
using System.Collections.Generic;
using System.IO;
using TextTabulator.Adapters;

public class EZCsvAdapter : ITabulatorAdapter
{
    private readonly TextReader _reader;
    private readonly bool _hasHeaderRow;

    public EZCsvAdapter(TextReader reader, bool hasHeaderRow)
    {
        _reader = reader;
        _hasHeaderRow = hasHeaderRow;
    }

    public IEnumerable<string>? GetHeaderStrings()
    {
        if (!_hasHeaderRow)
        {
            return null;
        }

        var line = _reader.ReadLine();

        if (line == null)
        {
            return null;
        }

        return line.Split(',');
    }

    public IEnumerable<IEnumerable<string>> GetValueStrings()
    {
        var rows = new List<string[]>();

        while (true)
        {
            var line = _reader.ReadLine();

            if (line == null || line.Length == 0)
            {
                break;
            }

            rows.Add(line.Split(','));
        }

        return rows;
    }
}
```

Here is an example of usage of the `EZCsvAdapter` class:

```
using System.Text;
using TextTabulator;

private static void Main(string[] args)
{
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

    using var stream = new MemoryStream(Encoding.UTF8.GetBytes(csvData));
    using var reader = new StreamReader(stream);
    var adapter = new EZCsvAdapter(reader, true);

    var tabulator = new Tabulator();
    var table = tabulator.Tabulate(adapter);

    Console.WriteLine(table);
}
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

The API consits of the `TextTabulator.Adapters.ITabulatorAdapter` interface. `CsvHelperTabulatorAdapter` derives from the `ICsvHelperTabulatorAdapter` to allow easy mocking for testing.

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

---
Copyright 2025 Justin Welsch