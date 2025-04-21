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

The core project can be found here: [TextTabulator](/src/TextTabulator/README.md)

There are also separate libraries that adapt different types of data to be consumed by the `TextTabulator` API. They can be found here:

`TextTabulator.Adapters`
- [CsvHelper adapter](https://github.com/jwelsch/TextTabulator/tree/main/src/TextTabulator.Adapters.CsvHelper)
- [JSON adapter](https://github.com/jwelsch/TextTabulator/tree/main/src/TextTabulator.Adapters.Json)
- [XML adapter](https://github.com/jwelsch/TextTabulator/tree/main/src/TextTabulator.Adapters.Xml)
- [YAML adapter](https://github.com/jwelsch/TextTabulator/tree/main/src/TextTabulator.Adapters.YamlDotNet)
- [Reflection adapter](https://github.com/jwelsch/TextTabulator/tree/main/src/TextTabulator.Adapters.Reflection)

---
Copyright 2025 Justin Welsch