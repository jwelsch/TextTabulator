[![Build](https://github.com/jwelsch/TextTabulator/actions/workflows/build.yml/badge.svg)](https://github.com/jwelsch/TextTabulator/actions/workflows/build.yml)
[![Publish](https://github.com/jwelsch/TextTabulator/actions/workflows/publish.yml/badge.svg)](https://github.com/jwelsch/TextTabulator/actions/workflows/publish.yml)
[![NuGet](https://img.shields.io/nuget/v/Jwelsch.TextTabulator.svg)](https://www.nuget.org/packages/Jwelsch.TextTabulator)


# TextTabulator Overview

TextTabulator is a .NET Standard 2.1 library that will format data into a `string` that, when printed, will be in the form of a table. TextTabulator is designed to be simple and lightweight. Just call the `Tabulator.Tabulate` method with a collection of strings and it will do the rest.

Example table:
```
╔═════════════════╤═════════════╤═════════╤══════════╗
║      Name       │Weight (tons)│  Diet   │Extinction║
╠═════════════════╪═════════════╪═════════╪══════════╣
║Tyrannosaurus Rex│          6.7│Carnivore│    66 mya║
╟─────────────────┼─────────────┼─────────┼──────────╢
║Triceratops      │            8│Herbivore│    66 mya║
╟─────────────────┼─────────────┼─────────┼──────────╢
║Apatosaurus      │           33│Herbivore│   147 mya║
╟─────────────────┼─────────────┼─────────┼──────────╢
║Archaeopteryx    │        0.001│ Omnivore│   147 mya║
╟─────────────────┼─────────────┼─────────┼──────────╢
║Anklyosaurus     │          4.8│Herbivore│    66 mya║
╟─────────────────┼─────────────┼─────────┼──────────╢
║Stegosaurus      │          3.8│Herbivore│   147 mya║
╟─────────────────┼─────────────┼─────────┼──────────╢
║Hadrosaurus      │            3│Herbivore│    66 mya║
╚═════════════════╧═════════════╧═════════╧══════════╝
```

Information about using the `TextTabulator` API can be found here: [TextTabulator](https://github.com/jwelsch/TextTabulator/blob/main/src/TextTabulator)

There are also separate libraries that adapt different types of data to be consumed by the `TextTabulator` API. They can be found here:

`TextTabulator.Adapters`
- CsvHelper adapter
    - [Github](https://github.com/jwelsch/TextTabulator/tree/main/src/TextTabulator.Adapters.CsvHelper)
    - [Nuget](https://www.nuget.org/packages/Jwelsch.TextTabulator.Adapters.CsvHelper)
- JSON adapter
    - [Github](https://github.com/jwelsch/TextTabulator/tree/main/src/TextTabulator.Adapters.Json)
    - [Nuget](https://www.nuget.org/packages/Jwelsch.TextTabulator.Adapters.Json)
- XML adapter
    - [Github](https://github.com/jwelsch/TextTabulator/tree/main/src/TextTabulator.Adapters.Xml)
    - [Nuget](https://www.nuget.org/packages/Jwelsch.TextTabulator.Adapters.Xml)
- YamlDotNet adapter
    - [Github](https://github.com/jwelsch/TextTabulator/tree/main/src/TextTabulator.Adapters.YamlDotNet)
    - [Nuget](https://www.nuget.org/packages/Jwelsch.TextTabulator.Adapters.YamlDotNet)
- ML.NET
    - [Github](https://github.com/jwelsch/TextTabulator/tree/main/src/TextTabulator.Adapters.MLDotNet)
    - [Nuget](https://www.nuget.org/packages/Jwelsch.TextTabulator.Adapters.MLDotNet)
- Reflection adapter
    - [Github](https://github.com/jwelsch/TextTabulator/tree/main/src/TextTabulator.Adapters.Reflection)
    - [Nuget](https://www.nuget.org/packages/Jwelsch.TextTabulator.Adapters.Reflection)
