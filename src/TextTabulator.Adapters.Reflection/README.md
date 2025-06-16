[![Build](https://github.com/jwelsch/TextTabulator/actions/workflows/build.yml/badge.svg)](https://github.com/jwelsch/TextTabulator/actions/workflows/build.yml)
[![Publish](https://github.com/jwelsch/TextTabulator/actions/workflows/publish.yml/badge.svg)](https://github.com/jwelsch/TextTabulator/actions/workflows/publish.yml)
[![NuGet](https://img.shields.io/nuget/v/Jwelsch.TextTabulator.Adapters.Reflection.svg)](https://www.nuget.org/packages/Jwelsch.TextTabulator.Adapters.Reflection)

# TextTabulator.Adapters.Reflection

This is an auxillary library for TextTabulator that provides a way to display the values of objects using `TextTabulator`.

## Installation

First, install the [TextTabulator main package](https://github.com/jwelsch/TextTabulator) and then this one.

Install the TextTabulator.Adapters.Reflection Nuget package in your project.

```
nuget install JWelsch.TextTabulator.Adapters.Reflection
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
   public string Name { get; set; };

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
using TextTabulator.Adapters.Reflection;

var data = new Dinosaur[]
{
   new Dinosaur("Tyrannosaurus Rex", 6.7, Diet.Carnivore, 66),
   new Dinosaur("Triceratops", 8, Diet.Herbivore, 66),
   new Dinosaur("Apatosaurus", 33, Diet.Herbivore, 147),
   new Dinosaur("Archaeopteryx", 0.001, Diet.Omnivore, 147),
   new Dinosaur("Anklyosaurus", 4.8, Diet.Herbivore, 66),
   new Dinosaur("Stegosaurus", 3.8, Diet.Herbivore, 147),
   new Dinosaur("Hadrosaurus", 3, Diet.Herbivore, 66),
};

var reflectionAdapter = new ReflectionTabulatorAdapter<Dinosaur>(data);

var tabulator = new Tabulator();
var table = tabulator.Tabulate(reflectionAdapter);

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

## Public API

The API consists of the `TextTabulator.Adapters.ReflectionTabulatorAdapter<T>` class. `ReflectionTabulatorAdapter<T>` derives from the `IReflectionTabulatorAdapter<T>` to allow easy mocking for testing.

### `TextTabulator.Adapters.Reflection.ReflectionTabulatorAdapter<T>`

Class that implements the `ITabulatorAdapter` interface in order to adapt types to be consumed by the `Tabulator.Tabulate` method.

**Constructors**

> `public ReflectionTabulatorAdapter..ctor(IEnumerable<T> items, ReflectionTabulatorAdapterOptions? options = null)`

Parameters
- `IEnumerable<T> items`: The collection of items to display as a table.
- `ReflectionTabulatorAdapterOptions? options`: Options for the adapter.

> `public ReflectionTabulatorAdapter..ctor(T item, ReflectionTabulatorAdapterOptions? options = null)`

Parameters
- `T item`: A single item to display in a table.
- `ReflectionTabulatorAdapterOptions? options`: Options for the adapter.

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

### `TextTabulator.Adapters.Reflection.ReflectionTabulatorAdapterOptions`

Options to allow configuration of the ReflectionTabulatorAdapter class.

**Constructors**

> `public ReflectionTabulatorAdapterOptions(INameTransform? memberNameTransform = null, TypeMembers typeMembers = TypeMembers.Properties, AccessModifiers accessModifiers = AccessModifiers.Public)`

Parameters
- `INameTransform? memberNameTransform`: Transform to apply to type member names. Passing null will cause the member names to not be altered.
- `TypeMembers typeMembers`: Specifies which type members to include in the output.
- `AccessModifiers accessModifiers`: Specifies the desired access modifier(s) of the type members to include in the output.

**Properties**

> `INameTransform MemeberNameTransform { get; }`

Gets the transform to apply to type member names.

> `TypeMembers TypeMembers { get; }`

Gets which type members to include in the output.

> `AccessModifiers AccessModifiers { get; }`

Gets the desired access modifier(s) of the type members to include in the output.

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

### `TextTabulator.Adapters.Reflection.TypeMembers`

Enumeration that specifies the type members to include in the table.

Values
- `Properties`: Include property members.
- `Fields`: Include all field members, except backing fields.
- `PropertiesAndFields` = `Properties` | `Fields`: Include both property members and field members (except backing fields).

### `TextTabulator.Adapters.Reflection.AccessModifiers`

Enumeration that specifies the allowed access modifier(s) of the type members to include in the table.

Values
- `Public`: Members with the public access modifier.
- `NonPublic`: Members with a non-public access modifier.
- `PublicAndNonPublic`= `Public` | `NonPublic`: Members with both public and non-public access modifiers.
