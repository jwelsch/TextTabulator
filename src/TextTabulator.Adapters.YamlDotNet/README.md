# TextTabulator.Adapters.YamlDotNet

This is an auxillary library for TextTabulator that provides an integration with the popular [YamlDotNet](https://github.com/aaubry/YamlDotNet) library that allows TextTabulator to consume YAML data.

## Installation

First, install the [TextTabulator main package](https://github.com/jwelsch/TextTabulator) and then this one.

Install the TextTabulator.Adapters.YamlDotNet Nuget package in your project.

```
nuget install JWelsch.TextTabulator.Adapters.YamlDotNet
```

## How to use

You can call the code like this:

```
using TextTabulator;
using TextTabulator.Adapters.YamlDotNet;

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
var csvAdapter = new YamlDotNetTabulatorAdapter(csvReader, true);

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

The API consits of the `TextTabulator.Adapters.YamlDotNet.YamlDotNetTabulatorAdapter` class. `YamlDotNetTabulatorAdapter` derives from the `IYamlDotNetTabulatorAdapter` to allow easy mocking for testing.

### `TextTabulator.Adapters.YamlDotNet.YamlDotNetTabulatorAdapter`

The adapter class that accepts a `YamlDotNet` object and presents the data that it reads in a format that `TextTabulator.Tabulate` can consume.

**Constructors**


---
Copyright 2025 Justin Welsch