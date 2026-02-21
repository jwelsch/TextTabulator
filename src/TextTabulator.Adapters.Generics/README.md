[![Build](https://github.com/jwelsch/TextTabulator/actions/workflows/build.yml/badge.svg)](https://github.com/jwelsch/TextTabulator/actions/workflows/build.yml)
[![Publish](https://github.com/jwelsch/TextTabulator/actions/workflows/publish.yml/badge.svg)](https://github.com/jwelsch/TextTabulator/actions/workflows/publish.yml)
[![NuGet](https://img.shields.io/nuget/v/Jwelsch.TextTabulator.Adapters.Generics.svg)](https://www.nuget.org/packages/Jwelsch.TextTabulator.Adapters.Generics)

# TextTabulator.Adapters.Generics

This is an auxillary library for TextTabulator that allows TextTabulator to easily consume .NET native generic types.

## Installation

First, install the [TextTabulator main package](https://github.com/jwelsch/TextTabulator) and then this one.

Install the TextTabulator.Adapters.Generics Nuget package in your project.

```
nuget install JWelsch.TextTabulator.Adapters.Generics
```

## How to use

See this example code.

Define types:
```
public enum Diet
{
   Carnivore,
   Herbivore,
   Omnivore
}

public class Dinosaur
{
   public string Name { get; set; }

   public double Weight { get; set; }

   public Diet Diet { get; set; }

   public int Extinction { get; set; }

   public Dinosaur(string name, double weight, Diet diet, int extinction)
   {
      Name = name;
      Weight = weight;
      Diet = diet;
      Extinction = extinction;
   }
}
```

Call `TextTabulator.Tabulate` to generate table:
```
using TextTabulator;
using TextTabulator.Adapters.Generics;

var data = new Dictionary<Guid, Dinosaur>
{
    { Guid.NewGuid(), new Dinosaur("Tyrannosaurus Rex", 6.7, Diet.Carnivore, 66) },
    { Guid.NewGuid(), new Dinosaur("Triceratops", 8, Diet.Herbivore, 66) },
    { Guid.NewGuid(), new Dinosaur("Apatosaurus", 33, Diet.Herbivore, 147) },
    { Guid.NewGuid(), new Dinosaur("Archaeopteryx", 0.001, Diet.Omnivore, 147) },
    { Guid.NewGuid(), new Dinosaur("Anklyosaurus", 4.8, Diet.Herbivore, 66) },
    { Guid.NewGuid(), new Dinosaur("Stegosaurus", 3.8, Diet.Herbivore, 147) },
    { Guid.NewGuid(), new Dinosaur("Hadrosaurus", 3, Diet.Herbivore, 66) },
};

var adapter = new GenericsTabulatorAdapter(data);

var tabulator = new Tabulator();
var table = tabulator.Tabulate(adapter);

Console.WriteLine(table);
```

This will produce the output:
```
------------------------------------------------------------------------------------
|Key                                 |Name             |Weight|Diet     |Extinction|
|------------------------------------+-----------------+------+---------+----------|
|567fb95b-97e3-4a56-aafc-b25edcdd9ba5|Tyrannosaurus Rex|6.7   |Carnivore|66        |
|------------------------------------+-----------------+------+---------+----------|
|d1412bfb-dc97-4f01-8630-aa58d772c33b|Triceratops      |8     |Herbivore|66        |
|------------------------------------+-----------------+------+---------+----------|
|8622ec6c-8a1a-442f-b502-1ea70b4e727a|Apatosaurus      |33    |Herbivore|147       |
|------------------------------------+-----------------+------+---------+----------|
|8b894103-8ba5-4d5a-a961-1ea2b639f05f|Archaeopteryx    |0.001 |Omnivore |147       |
|------------------------------------+-----------------+------+---------+----------|
|e4a4a81d-16a1-42b5-86fb-81ccaad24ea7|Anklyosaurus     |4.8   |Herbivore|66        |
|------------------------------------+-----------------+------+---------+----------|
|fa16e8d4-7292-423c-93f9-c516bc8a6538|Stegosaurus      |3.8   |Herbivore|147       |
|------------------------------------+-----------------+------+---------+----------|
|704f3fc1-13e6-4e72-aba6-8f6f2f798964|Hadrosaurus      |3     |Herbivore|66        |
------------------------------------------------------------------------------------
```

## Public API

### `TextTabulator.Adapters.Generics.DictionaryTabulatorAdapter<T>` class

Class that implements the `ITabulatorAdapter` interface in order to adapt types to be consumed by the `Tabulator.Tabulate` method.

**Constructors**

> `public DictionaryTabulatorAdapter..ctor(IDictionary<TKey, TValue> dictionary, DictionaryTabulatorAdapterOptions? options = null)`

Parameters
- `IDictionary<TKey, TValue> dictionary`: The dictionary to display as a table.
- `DictionaryTabulatorAdapterOptions? options`: Options for the adapter.

- **Methods**

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

### `TextTabulator.Adapters.Generics.DictionaryTabulatorAdapterOptions`

Options to allow configuration of the DictionaryTabulatorAdapter class.

**Constructors**

> `public DictionaryTabulatorAdapterOptions(INameTransform? headerNameTransform = null, SortOrder columnSortOrder = SortOrder.Default)`
Parameters
- `INameTransform? headerNameTransform`: Transform to apply to header names. Passing null will cause the header names to not be altered.
- `SortOrder columnSortOrder`: Specifies the sort order of the columns.

**Properties**

> `INameTransform HeaderNameTransform { get; }`
Gets the transform to apply to header names.

> `SortOrder ColumnSortOrder { get; }`
Gets the sort order of the columns.

### `TextTabulator.Adapters.Generics.ListTabulatorAdapter<T>` class

Class that implements the `ITabulatorAdapter` interface in order to adapt types to be consumed by the `Tabulator.Tabulate` method.

**Constructors**

> `public ListTabulatorAdapter..ctor(IList<T> list, ListTabulatorAdapterOptions? options = null)`

Parameters
- `IList<T> list`: The list to display as a table.
- `ListTabulatorAdapterOptions? options`: Options for the adapter.
- 
- **Methods**

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

### `TextTabulator.Adapters.Generics.ListTabulatorAdapterOptions`

Options to allow configuration of the ListTabulatorAdapter class.

**Constructors**

> `public ListTabulatorAdapterOptions(bool includeIndex = true, INameTransform? headerNameTransform = null, SortOrder columnSortOrder = SortOrder.Default)`
Parameters
- `bool includeIndex`: True to include an index column, false otherwise.
- `INameTransform? headerNameTransform`: Transform to apply to header names. Passing null will cause the header names to not be altered.
- `SortOrder columnSortOrder`: Specifies the sort order of the columns.

**Properties**

> `bool IncludeIndex { get; }`

Gets a value indicating whether to include an index column.

> `INameTransform HeaderNameTransform { get; }`

Gets the transform to apply to header names.

> `SortOrder ColumnSortOrder { get; }`

Gets the sort order of the columns.

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
