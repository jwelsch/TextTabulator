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

Follow the link for the [full public API documentation](https://github.com/jwelsch/TextTabulator/main/docs/texttabulator.adapters.csvhelper.md).
