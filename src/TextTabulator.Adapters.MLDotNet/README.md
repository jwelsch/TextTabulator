[![Build](https://github.com/jwelsch/TextTabulator/actions/workflows/build.yml/badge.svg)](https://github.com/jwelsch/TextTabulator/actions/workflows/build.yml)
[![Publish](https://github.com/jwelsch/TextTabulator/actions/workflows/publish.yml/badge.svg)](https://github.com/jwelsch/TextTabulator/actions/workflows/publish.yml)
[![NuGet](https://img.shields.io/nuget/v/Jwelsch.TextTabulator.Adapters.MLDotNet.svg)](https://www.nuget.org/packages/Jwelsch.TextTabulator.Adapters.MLDotNet)

# TextTabulator.Adapters.CsvHelper

This is an auxillary library for TextTabulator that provides an integration with [IDataView](https://learn.microsoft.com/en-us/dotnet/api/microsoft.ml.idataview) used in [ML.NET](https://dotnet.microsoft.com/en-us/apps/ai/ml-dotnet).

## Installation

First, install the [TextTabulator main package](https://github.com/jwelsch/TextTabulator) and then this one.

Install the TextTabulator.Adapters.MLDotNet Nuget package in your project.

```
nuget install JWelsch.TextTabulator.Adapters.MLDotNet
```

## How to use

Provided `Dinosaur` is defined as:

```
public class Dinosaur
{
    public string Name { get; }

    public double Weight { get; }

    public string Diet { get; }

    public int ExtinctionMya { get; }

    public Dinosaur(string name, double weight, string diet, int extinctionMya)
    {
        Name = name;
        Weight = weight;
        Diet = diet;
        ExtinctionMya = extinctionMya;
    }
}
```

You can call the code like this:

```
using Microsoft.ML;
using TextTabulator;
using TextTabulator.Adapters.MLDotNet;

var list = new List<Dinosaur>
{
    new ("Tyrannosaurus Rex", 6.7, "Carnivore", 66),
    new ("Triceratops", 8, "Herbivore", 66),
    new ("Apatosaurus", 33 ,"Herbivore", 147),
    new ("Archaeopteryx", 0.001, "Omnivore", 147),
    new ("Anklyosaurus", 4.8, "Herbivore", 66),
    new ("Stegosaurus", 3.8, "Herbivore", 147),
    new ("Hadrosaurus", 3, "Herbivore", 66)
};

var mlContext = new MLContext();
var data = mlContext.Data.LoadFromEnumerable(list);

var adapter = new DataViewTabulatorAdapter(data);

var tabulator = new Tabulator();
var table = tabulator.Tabulate(adapter);

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

The API consists of the `TextTabulator.Adapters.MLDotNet.DataViewTabulatorAdapter` class. `DataViewTabulatorAdapter` derives from the `IDataViewTabulatorAdapter` to allow easy mocking for testing.

### `TextTabulator.Adapters.MLDotNet.DataViewTabulatorAdapter`

The adapter class that accepts an `IDataView` object and presents the data that it reads in a format that `TextTabulator.Tabulate` can consume.

`DataViewTabulatorAdapter` can handle the following types of data in each cell:
- bool
- char
- string
- byte
- sbyte
- short
- ushort
- int
- uint
- long
- ulong
- float
- double
- decimal
- System.DateTime
- System.DateTimeOffset
- System.TimeSpan
- System.Guid
- ReadOnlyMemory\<char>

**Constructors**

> `public DataViewTabulatorAdapter..ctor(IDataView dataView, DataViewTabulatorAdapterOptions? options = null)`

Parameters
- `IDataView dataView`: IDataView object containing data to be consumed by Tabulator.Tabulate.
- `DataViewTabulatorAdapterOptions? options`: Options for the adapter.

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

### `TextTabulator.Adapters.MLDotNet.DataViewTabulatorAdapterOptions`

Options to allow configuration of the `DataViewTabulatorAdapter` class.

**Constructors**

> `public DataViewTabulatorAdapterOptions(INameTransform? columnNameTransform = null, ITypeFormatter? typeFormatter = null)`

Parameters
- `INameTransform? columnNameTransform`: Transform to apply to `IDataView` column names. Passing null will cause the `IDataView` column names to not be altered.
- `ITypeFormatter? typeFormatter`: Formatter to apply to `IDataView` column values. Passing null will cause the `IDataView` column values to use default formatting.

**Properties**

> `INameTransform HeaderNameTransform { get; }`

 Gets the transform to apply to `IDataView` column names.

> `ITypeFormatter TypeFormatter { get; }`

Gets the formatter to apply to `IDataView` column values. Passing null will cause the `IDataView` column values to use default formatting.

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
