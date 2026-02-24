[![Build](https://github.com/jwelsch/TextTabulator/actions/workflows/build.yml/badge.svg)](https://github.com/jwelsch/TextTabulator/actions/workflows/build.yml)
[![Publish](https://github.com/jwelsch/TextTabulator/actions/workflows/publish.yml/badge.svg)](https://github.com/jwelsch/TextTabulator/actions/workflows/publish.yml)
[![NuGet](https://img.shields.io/nuget/v/Jwelsch.TextTabulator.Adapters.Generics.svg)](https://www.nuget.org/packages/Jwelsch.TextTabulator.Adapters.Generics)

# TextTabulator.Adapters.Generics

This is an auxillary library for TextTabulator that allows TextTabulator to easily consume .NET native generic types.

## Installation

First, install the [TextTabulator main package](https://github.com/jwelsch/TextTabulator) and then this one.

Install the TextTabulator.Adapters.Generics Nuget package in your project.

```
nuget install JWelsch.TextTabulator.Adapters.Generics
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
using TextTabulator.Adapters.Generics;

var data = new Dictionary<Guid, Dinosaur>
{
    { Guid.NewGuid(), new Dinosaur("Tyrannosaurus Rex", 6.7, Diet.Carnivore, 66) },
    { Guid.NewGuid(), new Dinosaur("Triceratops", 8, Diet.Herbivore, 66) },
    { Guid.NewGuid(), new Dinosaur("Apatosaurus", 33, Diet.Herbivore, 147) },
    { Guid.NewGuid(), new Dinosaur("Archaeopteryx", 0.001, Diet.Omnivore, 147) },
    { Guid.NewGuid(), new Dinosaur("Anklyosaurus", 4.8, Diet.Herbivore, 66) },
    { Guid.NewGuid(), new Dinosaur("Stegosaurus", 3.8, Diet.Herbivore, 147) },
    { Guid.NewGuid(), new Dinosaur("Hadrosaurus", 3, Diet.Herbivore, 66) },
};

var adapter = new GenericsTabulatorAdapter(data);

var tabulator = new Tabulator();
var table = tabulator.Tabulate(adapter);

Console.WriteLine(table);
```

This will produce the output:
```
------------------------------------------------------------------------------------
|Key                                 |Name             |Weight|Diet     |Extinction|
|------------------------------------+-----------------+------+---------+----------|
|567fb95b-97e3-4a56-aafc-b25edcdd9ba5|Tyrannosaurus Rex|6.7   |Carnivore|66        |
|------------------------------------+-----------------+------+---------+----------|
|d1412bfb-dc97-4f01-8630-aa58d772c33b|Triceratops      |8     |Herbivore|66        |
|------------------------------------+-----------------+------+---------+----------|
|8622ec6c-8a1a-442f-b502-1ea70b4e727a|Apatosaurus      |33    |Herbivore|147       |
|------------------------------------+-----------------+------+---------+----------|
|8b894103-8ba5-4d5a-a961-1ea2b639f05f|Archaeopteryx    |0.001 |Omnivore |147       |
|------------------------------------+-----------------+------+---------+----------|
|e4a4a81d-16a1-42b5-86fb-81ccaad24ea7|Anklyosaurus     |4.8   |Herbivore|66        |
|------------------------------------+-----------------+------+---------+----------|
|fa16e8d4-7292-423c-93f9-c516bc8a6538|Stegosaurus      |3.8   |Herbivore|147       |
|------------------------------------+-----------------+------+---------+----------|
|704f3fc1-13e6-4e72-aba6-8f6f2f798964|Hadrosaurus      |3     |Herbivore|66        |
------------------------------------------------------------------------------------
```

## Public API

The full public API documentation can be found here: 