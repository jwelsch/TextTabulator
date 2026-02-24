[![Build](https://github.com/jwelsch/TextTabulator/actions/workflows/build.yml/badge.svg)](https://github.com/jwelsch/TextTabulator/actions/workflows/build.yml)
[![Publish](https://github.com/jwelsch/TextTabulator/actions/workflows/publish.yml/badge.svg)](https://github.com/jwelsch/TextTabulator/actions/workflows/publish.yml)
[![NuGet](https://img.shields.io/nuget/v/Jwelsch.TextTabulator.Adapters.Reflection.svg)](https://www.nuget.org/packages/Jwelsch.TextTabulator.Adapters.Reflection)

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
   public string Name { get; set; }

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

The full public API documentation can be found here: 
