# TextTabulator.Adapters.Json

This is an auxillary library for TextTabulator that uses the [System.Text.Json](https://github.com/dotnet/runtime/tree/main/src/libraries/System.Text.Json) library to parse JSON data. It can then provide the parsed JSON data to TextTabulator for consumption.

## How to use

Install the [TextTabulator main package](https://github.com/jwelsch/TextTabulator) and then this one.

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

## Public API

The API consits of the `TextTabulator.Adapters.Json.JsonTabulatorAdapter` class. `JsonTabulatorAdapter` derives from the `IJsonTabulatorAdapter` to allow easy mocking for testing.

### `TextTabulator.Adapters.Json.JsonTabulatorAdapter`

The adapter class that accepts JSON data and presents the data that it reads in a format that `TextTabulator.Tabulate` can consume.

**Constructors**

> `public JsonTabulatorAdapter..ctor(Func<Stream> jsonStreamProvider, JsonReaderOptions options = default)`

Parameters
- `Func<Stream> jsonStreamProvider`: A method that returns a Stream containing UTF-8 encoded JSON data.
- `JsonReaderOptions options`: Options to pass to the Utf8JsonReader.

> `public JsonTabulatorAdapter..ctor(Stream jsonStream, JsonReaderOptions options = default)`

Parameters
- `Stream jsonStream`: A Stream containing UTF-8 encoded JSON data.
- `JsonReaderOptions options`: Options to pass to the Utf8JsonReader.

> `public JsonTabulatorAdapter..ctor(string json, JsonReaderOptions options = default)`

Parameters
- `string json`: A string containing raw JSON data.
- `JsonReaderOptions options`: Options to pass to the Utf8JsonReader.

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
