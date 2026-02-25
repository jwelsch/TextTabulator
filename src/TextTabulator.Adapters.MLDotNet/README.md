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

## Input Data

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

## Public API

Follow the link for the [full public API documentation](https://github.com/jwelsch/TextTabulator/main/docs/index-texttabulator.adapters.mldotnet.md).
