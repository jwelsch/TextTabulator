[![Build](https://github.com/jwelsch/TextTabulator/actions/workflows/build.yml/badge.svg)](https://github.com/jwelsch/TextTabulator/actions/workflows/build.yml)
[![Publish](https://github.com/jwelsch/TextTabulator/actions/workflows/publish.yml/badge.svg)](https://github.com/jwelsch/TextTabulator/actions/workflows/publish.yml)
[![NuGet](https://img.shields.io/nuget/v/Jwelsch.TextTabulator.Adapters.YamlDotNet.svg)](https://www.nuget.org/packages/Jwelsch.TextTabulator.Adapters.YamlDotNet)

# TextTabulator.Adapters.YamlDotNet

This is an auxillary library for TextTabulator that provides an integration with the popular [YamlDotNet](https://github.com/aaubry/YamlDotNet) library that allows TextTabulator to consume YAML data.

## Installation

First, install the [TextTabulator main package](https://github.com/jwelsch/TextTabulator) and then this one.

Install the TextTabulator.Adapters.YamlDotNet Nuget package in your project.

```
nuget install JWelsch.TextTabulator.Adapters.YamlDotNet
```

## How to Use

You can call the code like this:

```
using System.IO;
using TextTabulator;
using TextTabulator.Adapters.YamlDotNet;
using YamlDotNet.Core;

var yamlData =
"""
- name: Tyrannosaurus Rex
  weight: 6.7
  diet: Carnivore
  extinction: 66
- name: Triceratops
  weight: 8
  diet: Herbivore
  extinction: 66
- name: Apatosaurus
  weight: 33
  diet: Herbivore
  extinction: 147
- name: Archaeopteryx
  weight: 0.001
  diet: Omnivore
  extinction: 147
- name: Anklyosaurus
  weight: 4.8
  diet: Herbivore
  extinction: 66
- name: Stegosaurus
  weight: 3.8
  diet: Herbivore
  extinction: 147
- name: Hadrosaurus
  weight: 3
  diet: Herbivore
  extinction: 66
""";

using var textReader = new StringReader(csvData);
var parser = new Parser(textReader);

var yamlAdapter = new YamlDotNetTabulatorAdapter(parser);

var tabulator = new Tabulator();
var table = tabulator.Tabulate(yamlAdapter);

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

## YAML Format

The `YamlTabulatorAdapter` can only parse YAML data in a specific format. The YAML data must be a single sequence of homogeneous YAML mappings only.

The data should be in the following format:

```
- field1: value1A
  field2: value2A
- field1: value1B
  field2: value2B
- field1: value1C
  field2: value2C
```

## Public API

Follow the link for the [full public API documentation](https://github.com/jwelsch/TextTabulator/main/docs/index-texttabulator.adapters.yamldotnet.md).
