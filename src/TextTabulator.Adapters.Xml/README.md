[![Build](https://github.com/jwelsch/TextTabulator/actions/workflows/build.yml/badge.svg)](https://github.com/jwelsch/TextTabulator/actions/workflows/build.yml)
[![Publish](https://github.com/jwelsch/TextTabulator/actions/workflows/publish.yml/badge.svg)](https://github.com/jwelsch/TextTabulator/actions/workflows/publish.yml)
[![NuGet](https://img.shields.io/nuget/v/Jwelsch.TextTabulator.Adapters.Xml.svg)](https://www.nuget.org/packages/Jwelsch.TextTabulator.Adapters.Xml)

# TextTabulator.Adapters.Xml

This is an auxillary library for TextTabulator that uses [System.Xml.XmlReader](https://github.com/dotnet/runtime/blob/main/src/libraries/System.Private.Xml/src/System/Xml/Core/XmlReader.cs) to parse XML data. It can then provide the parsed XML data to TextTabulator for consumption.

## Installation

First, install the [TextTabulator main package](https://github.com/jwelsch/TextTabulator) and then this one.

Install the TextTabulator.Adapters.Xml Nuget package in your project.

```
nuget install JWelsch.TextTabulator.Adapters.Xml
```

## How to use

You can call the code like this:

```
using TextTabulator;
using TextTabulator.Adapters.Xml;

var xmlData =
"""
<?xml version="1.0" encoding="UTF-8"?>
<dinosaurs>
    <dinosaur>
        <name>Tyrannosaurus Rex</name>
        <weight>6.7</weight>
        <diet>Carnivore</diet>
        <extinction>66</extinction>
    </dinosaur>
    <dinosaur>
        <name>Triceratops</name>
        <weight>8</weight>
        <diet>Herbivore</diet>
        <extinction>66</extinction>
    </dinosaur>
    <dinosaur>
        <name>Apatosaurus</name>
        <weight>33</weight>
        <diet>Herbivore</diet>
        <extinction>147</extinction>
    </dinosaur>
    <dinosaur>
        <name>Archaeopteryx</name>
        <weight>0.001</weight>
        <diet>Omnivore</diet>
        <extinction>147</extinction>
    </dinosaur>
    <dinosaur>
        <name>Anklyosaurus</name>
        <weight>4.8</weight>
        <diet>Herbivore</diet>
        <extinction>66</extinction>
    </dinosaur>
    <dinosaur>
        <name>Stegosaurus</name>
        <weight>3.8</weight>
        <diet>Herbivore</diet>
        <extinction>147</extinction>
    </dinosaur>
    <dinosaur>
        <name>Hadrosaurus</name>
        <weight>3</weight>
        <diet>Herbivore</diet>
        <extinction>66</extinction>
    </dinosaur>
</dinosaurs>
""";

var xmlAdapter = new XmlTabulatorAdapter(xmlData, true);

var tabulator = new Tabulator();
var table = tabulator.Tabulate(xmlAdapter);

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

## XML Format

The `XmlTabulatorAdapter` can only parse XML data in a specific format. The XML data must be a node that is a list of homogenous XML nodes only. If the XML document root is not a node that is a list an exception will be thrown. If the XML nodes are not homogenous an exception will be thrown.

The data should be in the following format:

```
<?xml version="1.0" encoding="UTF-8"?>
<list>
    <object>
        <value1>value1A</value1>
        <value2>value2A</value2>
    </object>
    <object>
        <value1>value1B</value1>
        <value2>value2B</value2>
    </object>
    <object>
        <value1>value1C</value1>
        <value2>value2C</value2>
    </object>
    ...
</list>
```

## Public API

The full public API documentation can be found here: 
