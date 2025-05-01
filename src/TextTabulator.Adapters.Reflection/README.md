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

The API consits of the `TextTabulator.Adapters.ReflectionTabulatorAdapter<T>` class. `ReflectionTabulatorAdapter<T>` derives from the `IReflectionTabulatorAdapter<T>` to allow easy mocking for testing.

### `TextTabulator.Adapters.Reflection.ReflectionTabulatorAdapter<T>`

The adapter class that accepts an enumeration of objects and presents them in a format that `TextTabulator.Tabulate` can consume.

**Constructors**

> `public ReflectionTabulatorAdapter..ctor(IEnumerable<T> items, TypeMembers typeMembers = TypeMembers.Properties, AccessModifiers accessModifiers = AccessModifiers.Public)`

Parameters
- `IEnumerable<T> items`: The collection of items to display as a table.
- `TypeMembers typeMembers`: The kind of type members to include in the table.
- `AccessModifiers accessModifiers`: Specifies the allowed access modifier(s) of the type members to include in the table.

> `public ReflectionTabulatorAdapter..ctor(T item, TypeMembers typeMembers = TypeMembers.Properties, AccessModifiers accessModifiers = AccessModifiers.Public)`

Parameters
- `T item`: A single item to display in a table.
- `TypeMembers typeMembers`: The kind of type members to include in the table.
- `AccessModifiers accessModifiers`: Specifies the allowed access modifier(s) of the type members to include in the table.

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
