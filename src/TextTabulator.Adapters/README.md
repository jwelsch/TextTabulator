[![Build](https://github.com/jwelsch/TextTabulator/actions/workflows/build.yml/badge.svg)](https://github.com/jwelsch/TextTabulator/actions/workflows/build.yml)
[![Publish](https://github.com/jwelsch/TextTabulator/actions/workflows/publish.yml/badge.svg)](https://github.com/jwelsch/TextTabulator/actions/workflows/publish.yml)
[![NuGet](https://img.shields.io/nuget/v/Jwelsch.TextTabulator.Adapters.svg)](https://www.nuget.org/packages/Jwelsch.TextTabulator.Adapters)

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
- ML.NET
    - [Github](https://github.com/jwelsch/TextTabulator/tree/main/src/TextTabulator.Adapters.MLDotNet)
    - [Nuget](https://www.nuget.org/packages/Jwelsch.TextTabulator.Adapters.MLDotNet)
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
using System.IO;
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

Follow the link for the [full public API documentation](https://github.com/jwelsch/TextTabulator/main/docs/index-texttabulator.adapters.md).
