# TextTabulator.Adapters.CsvHelper

This is an auxillary library for TextTabulator that provides an integration with the popular [CsvHelper](https://github.com/JoshClose/CsvHelper) library that allows TextTabulator to consume CSV data.

## How to use

Install the [TextTabulator main package](https://github.com/jwelsch/TextTabulator) and then this one.

You can call the code like this:

```
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

using var textReader = new StringReader(filePath);
using var csvReader = new CsvReader(textReader, CultureInfo.InvariantCulture);
var csvAdapter = new CsvHelperTabulatorAdapter(csvReader, true);

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

The API consits of the `TextTabulator.Adapters.CsvHelper.CsvHelperTabulatorAdapter` class. `CsvHelperTabulatorAdapter` derives from the `ICsvHelperTabulatorAdapter` to allow easy mocking for testing.

### `TextTabulator.Adapters.CsvHelper.CsvHelperTabulatorAdapter`

The adapter class that accepts a `CsvHelper` object and presents the data that it reads in a format that `TextTabulator.Tabulate` can consume.

**Constructors**

> `public CsvHelperTabulatorAdapter..ctor(CsvReader csvReader, bool hasHeaderRow)`

Parameters
- `CsvReader csvReader`: The `CsvReader` object that will read the desired data.
- `bool hasHeaderRow`: True if the CSV data to read has a header row, false if not.

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
