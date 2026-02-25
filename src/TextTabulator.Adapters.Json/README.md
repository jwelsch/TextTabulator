[![Build](https://github.com/jwelsch/TextTabulator/actions/workflows/build.yml/badge.svg)](https://github.com/jwelsch/TextTabulator/actions/workflows/build.yml)
[![Publish](https://github.com/jwelsch/TextTabulator/actions/workflows/publish.yml/badge.svg)](https://github.com/jwelsch/TextTabulator/actions/workflows/publish.yml)
[![NuGet](https://img.shields.io/nuget/v/Jwelsch.TextTabulator.Adapters.Json.svg)](https://www.nuget.org/packages/Jwelsch.TextTabulator.Adapters.Json)

# TextTabulator.Adapters.Json

This is an auxillary library for TextTabulator that uses the [System.Text.Json](https://github.com/dotnet/runtime/tree/main/src/libraries/System.Text.Json) library to parse JSON data. It can then provide the parsed JSON data to TextTabulator for consumption.

## Installation

First, install the [TextTabulator main package](https://github.com/jwelsch/TextTabulator) and then this one.

Install the TextTabulator.Adapters.Json Nuget package in your project.

```
nuget install JWelsch.TextTabulator.Adapters.Json
```

## How to use

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

Follow the link for the [full public API documentation](https://github.com/jwelsch/TextTabulator/main/docs/index-texttabulator.adapters.json.md).
