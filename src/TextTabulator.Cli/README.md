# TextTabulator.Cli

TextTabulator.Cli is a .NET application that allows command line access to `TextTabulator` functionality. `TextTabulator` is a .NET Standard 2.1 library that will format data into a `string` that, when printed, will be in the form of a table.

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

The core project can be found here: [TextTabulator](https://github.com/jwelsch/TextTabulator/blob/main/src/TextTabulator)

---

## Command Line Arguments

Command line arguments for the texttabulator-cli.exe.

### -d

Short version of `--data-type`.

### --data-type

This argument is optional. Short notation: `-d`.

This specifies the format of the data in the file specified by `--input-path`. The type of data should be the next argument on the command line. If this argument is not specified, the data format will be inferred from the file extension of `--input-path`. If this is specified, it will take precedence over the file extension.

The following values are valid:

- `CSV`
- `JSON`
- `XML`
- `YAML`
- `YML`

Only certain forms of the data formats are supported.

**CSV**

No restrictions.

**JSON**

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

**XML**

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
</list>
```

**YAML**

```
- field1: value1A
  field2: value2A
- field1: value1B
  field2: value2B
- field1: value1C
  field2: value2C
```

Usage example:
```
texttabulator-cli.exe --input-path C:\Some\Path\data.txt --data-type CSV
```

### -i

Short version of `--input-path`.

### --input-path

This argument is required. Short notation: `-i`

This specifies the file that will be read from, and whose contents will be put into a table. The file path should be the next argument on the command line.

If `--data-type` is not specified, the type of data will be inferred from the file extension. The recognized file extensions are as follows:

- `.csv`: Comma separated values data
- `.json`: JSON data
- `.xml`: XML data
- `.yaml`: YAML data
- `.yml`: YAML data

Usage example:
```
texttabulator-cli.exe --input-path C:\Some\Path\data.csv
```

### -o

Short version of `--output-path`.

### --output-path

This argument is optional. Short notation: `-o`.

This specifies what file path to write the table to. The file path should be the next argument on the command line. The table will be encoded as UTF-8 characters. If this argument is not included, the table will be written to the console.

Usage example:
```
texttabulator-cli.exe --input-path "C:\Some\Path\data.csv" --output-path "C:\Some\Path\table.txt"
```

### -s

Short version of `--styling`.

### --styling

This argument is optional. Short notation: `-s`.

This specifies which characters set to use to create the table. The value should be the next argument on the command line. If this argument is not included, the table will be created using ASCII characters.

Valid values are:

- `ASCII`: Uses characters within the traditional range of ASCII values, which are 0-255.
- `Unicode`: Uses characters outside the traditional range of ASCII values. Sometimes called "Box Drawing" characters, they are in the range U+2500 to U+250C.

Usage example:
```
texttabulator-cli.exe --input-path "C:\Some\Path\data.csv" --styling Unicode
```

ASCII example table:
```
-------------------------
|Header1|Header2|Header3|
|-------+-------+-------|
|value1A|value2A|value3A|
|-------+-------+-------|
|value1B|value2B|value3B|
|-------+-------+-------|
|value1C|value2C|value3C|
-------------------------
```

Unicode example table:
```
╔═══════╤═══════╤═══════╗
║Header1│Header2│Header3║
╠═══════╪═══════╪═══════╣
║value1A│value2A│value3A║
╟───────┼───────┼───────╢
║value1B│value2B│value3B║
╟───────┼───────┼───────╢
║value1C│value2C│value3C║
╚═══════╧═══════╧═══════╝
```
