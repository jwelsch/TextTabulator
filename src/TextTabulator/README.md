[![Build](https://github.com/jwelsch/TextTabulator/actions/workflows/build.yml/badge.svg)](https://github.com/jwelsch/TextTabulator/actions/workflows/build.yml)
[![Publish](https://github.com/jwelsch/TextTabulator/actions/workflows/publish.yml/badge.svg)](https://github.com/jwelsch/TextTabulator/actions/workflows/publish.yml)
[![NuGet](https://img.shields.io/nuget/v/Jwelsch.TextTabulator.svg)](https://www.nuget.org/packages/Jwelsch.TextTabulator)

# TextTabulator Project

This is the core library of `TextTabulator`, and is what does the actual table formation.

## Installation

Install the TextTabulator Nuget package in your project.

```
nuget install JWelsch.TextTabulator
```

## How to use

There is one public method, `Tabulator.Tabulate`, and a few configuration options. The simplest way to use `TextTabulator` is to call the `Tabulator.Tabulate` method with the default options.

```
using TextTabulator;

var headers = new string[]
{
   "Header1",
   "Header2",
   "Header3",
};

var values = new string[][]
{
   new string[] { "value1A", "value2A", "value3A", },
   new string[] { "value1B", "value2B", "value3B", },
   new string[] { "value1C", "value2C", "value3C", },
};

var tabulator = new Tabulator();

var table = tabulator.Tabulate(headers, values);

Console.WriteLine(table);
```

The output of the above code would be:

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

## Public API

Follow the link for the [full public API documentation](https://github.com/jwelsch/TextTabulator/main/docs/index-texttabulator.md).

### Adapters

`TextTabulator` exposes the interface `ITabulatorAdapter`, which can be implemented to allow the `Tabulator.Tabulate` method to consume different types of data. There are existing implementations available for popular data types. Alternatively, this interface can be used to provide a custom implementation.

The `ITabulatorAdapter` inteface can be found in a separate package:
- [Github](https://github.com/jwelsch/TextTabulator/tree/main/src/TextTabulator.Adapters)
- [Nuget](https://www.nuget.org/packages/Jwelsch.TextTabulator.Adapters)

Existing implementations of `ITabulatorAdapter` for various common data formats are distributed in separate Nuget packages. More information can be found in their respective projects.
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
- Generics adapter
    - [Github](https://github.com/jwelsch/TextTabulator/tree/main/src/TextTabulator.Adapters.Generics)
    - [Nuget](https://www.nuget.org/packages/Jwelsch.TextTabulator.Adapters.Generics)

## Tabulation Options

Various aspects of the generated table can be controlled by the configuration class `TabulatorOptions`. Examples of what the specific options values produce can be found below.

### Alignment

If a cell's contents do not span the full width of a column, it can be aligned such that the content will appear consistently on the left, right, or in the center. This can be accomplished by setting the `CellAlignment` property in an `ITabulatorOptions` object. The `CellAlignment` property can be set with type that implements `ICellAlignmentProvider`. If the default `ICellAlignmentProvider` is used, all cell contents will be aligned to the left.

There are four `CellAlignment` values:
- `Left`: Aligns content to the left of the cell.
- `Right`: Aligns content to the right of the cell.
- `CenterLeftBias`: Attempts to center content within the cell. If the content cannot be exactly centered, the extra space will appear on the right.
- `CenterRightBias`: Attempts to center content within the cell. If the content cannot be exactly centered, the extra space will appear on the left.

There are numerous preconfigured `ICellAlignmentProvider` implementations that come built-in.

`UniformAlignmentProvider` aligns all cells the same.

```
----------------------------------
|Header    |Header2   |ZZZHeader3|
|----------+----------+----------|
|Value1A   |Value2A   |Value3A   |
|----------+----------+----------|
|Value1B   |YYYValue2B|Value3B   |
|----------+----------+----------|
|XXXValue1C|Value2C   |Value3C   |
----------------------------------
```

`IndividualCellAlignmentProvider` allows each cell to be aligned separately.

```
----------------------------------
|  Header  |  Header2 |ZZZHeader3|
|----------+----------+----------|
|  Value1A | Value2A  |  Value3A |
|----------+----------+----------|
|Value1B   |YYYValue2B|Value3B   |
|----------+----------+----------|
|XXXValue1C|   Value2C|Value3C   |
----------------------------------
```

`UniformColumnAlignmentProvider` aligns all cells in a column the same.

```
----------------------------------
|Header    |  Header2 |ZZZHeader3|
|----------+----------+----------|
|Value1A   |  Value2A |   Value3A|
|----------+----------+----------|
|Value1B   |YYYValue2B|   Value3B|
|----------+----------+----------|
|XXXValue1C|  Value2C |   Value3C|
----------------------------------
```

`UniformValueAlignmentProvider` aligns all values the same, while allowing the alignment of each header to vary.

```
----------------------------------
|Header    |  Header2 |ZZZHeader3|
|----------+----------+----------|
| Value1A  | Value2A  | Value3A  |
|----------+----------+----------|
| Value1B  |YYYValue2B| Value3B  |
|----------+----------+----------|
|XXXValue1C| Value2C  | Value3C  |
----------------------------------
```

`UniformHeaderAlignmentProvider` aligns all headers the same way, while allowing the alignment of each value to vary.

```
----------------------------------
|  Header  | Header2  |ZZZHeader3|
|----------+----------+----------|
| Value1A  |  Value2A |   Value3A|
|----------+----------+----------|
|  Value1B |YYYValue2B|  Value3B |
|----------+----------+----------|
|XXXValue1C|   Value2C|Value3C   |
----------------------------------
```

`UniformHeaderUniformValueAlignmentProvider` allows a single alignment to be set for all headers and another one to be set for all values.

```
----------------------------------
|Header    |Header2   |ZZZHeader3|
|----------+----------+----------|
|   Value1A|   Value2A|   Value3A|
|----------+----------+----------|
|   Value1B|YYYValue2B|   Value3B|
|----------+----------+----------|
|XXXValue1C|   Value2C|   Value3C|
----------------------------------
```

`UniformHeaderUniformColumnAlignmentProvider` aligns all headers the same, while aligning the values in each column separately.

```
----------------------------------
|  Header  | Header2  |ZZZHeader3|
|----------+----------+----------|
|Value1A   |  Value2A |   Value3A|
|----------+----------+----------|
|Value1B   |YYYValue2B|   Value3B|
|----------+----------+----------|
|XXXValue1C|  Value2C |   Value3C|
----------------------------------
```

You can set the `ICellAlignmentProvider` with the following code:

```
var tabulator = new TextTabulator();
var options = new TabulatorOptions
{
   CellAlignment = new UniformHeaderUniformColumnAlignmentProvider(new CellAlignment[] { CellAlignment.Left, CellAlignment.Right }, CellAlignment.CenterLeftBias)
};

var table = tabulator.Tabulate(headers, values, options);
```

As a convenience, the last `CellAlignment` value in a collection passed to one of the built-in `ICellAlignmentProvider` implementations will be used if the number of alignments is less than the number of actual cells to align. For example, say that a table has three columns and a `UniformColumnAlignmentProvider` is used. If the `UniformColumnAlignmentProvider` constructor is only given two column alignments, the last column will be aligned using the last alignment value in the collection that it was passed.

```
// Given these headers.
var headers = new string[] { "Header", "Header2", "ZZZHeader3" };

// Given these values.
var values = new string[][]
{
   new string[] { "Value1A", "Value2A", "Value3A" },
   new string[] { "Value1B", "YYYValue2B", "Value3B" },
   new string[] { "XXXValue1C", "Value2C", "Value3C" },
};

// Using this UniformColumnAlignmentProvider.
var alignment = new UniformColumnAlignmentProvider(new CellAlignment[] { CellAlignment.Left, CellAlignment.Right });

var tabulator = new Tabulator();

var table = tabulator.Tabulate(headers, values, new TabulatorOptions { CellAlignment = alignment });

Console.WriteLine(table);
```

The output will look like this:

```
----------------------------------
|Header    |   Header2|ZZZHeader3|
|----------+----------+----------|
|Value1A   |   Value2A|   Value3A|
|----------+----------+----------|
|Value1B   |YYYValue2B|   Value3B|
|----------+----------+----------|
|XXXValue1C|   Value2C|   Value3C|
----------------------------------
```

### Styling

The style of the table structure can be controlled by setting the `Styling` property in an `ITabulatorOptions` object and passing it to the `Tabulator.Tabulate` method. There are two types that implement `ITableStyle` out of the box: `AsciiTableStyling` and `UnicodeTableStyling`. You can set the properties of either to further customize the styling of the table. The default table styling is the same as `AsciiTableStyling`.

`AsciiTableStyling` will only use traditional ASCII characters to build the table. Note that this does not mean that the characters are ASCII encoded, the actual characters are encoded as standard Unicode, like all .NET characters. This only uses characters within the traditional ASCII 1-byte range of 0-255.

The default styling uses `AsciiTableStyling`, but you can also set this styling with the following code:

```
var tabulator = new TextTabulator();
var options = new TabulatorOptions
{
   Styling = new AsciiTableStyling()
};

var table = tabulator.Tabulate(headers, values, options);
```

An example table looks like:

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

`UnicodeTableStyling` uses character values that are outside of the traditional ASCII range. There are Unicode characters that are specifically designed to create tables. Sometimes called "Box Drawing" characters, they are in the range U+2500 to U+250C. These are what the properties in `UnicodeTableStyling` default to.

You can set this styling with the following code:

```
var tabulator = new TextTabulator();
var options = new TabulatorOptions
{
   Styling = new UnicodeTableStyling()
};

var table = tabulator.Tabulate(headers, values, options);
```

An example table looks like:

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

### New Lines

By default `TextTabulator.Tabulate` uses the `Environment.NewLine` for new lines. However, characters to use for new lines can be set by assigning a value to `TabulatorOptions.NewLines`.

Example:

```
var tabulator = new TextTabulator();
var options = new TabulatorOptions
{
   NewLine = "\n",
};

var table = tabulator.Tabulate(headers, values, options);
```

### Include Non-Printable Characters

Setting false will cause any non-printable characters to not be included in the table. Setting true will cause any non-printable characters to be left in the table. This has no effect on tab ('\t'), carriage return ('\r'), or new line ('\n') characters. By default, this value is set to false.

Setting this to false can alleviate issues with non-printable characters causing the table to be misaligned.

Example:

```
var tabulator = new TextTabulator();
var options = new TabulatorOptions
{
   IncludeNonPrintableCharacters = true,
};

var table = tabulator.Tabulate(headers, values, options);
```

### Tab Length

Sets the number of spaces to substitute for each tab character in the table. If this is set to 0, tab characters will be used in the table. If this is set to a positive number, tab characters will be replaced with the specified number of spaces. If this is set to a negative number, tab characters will be removed. The default value for this property is 0.

Setting this to -1 or a positive number can alleviate issues with tab characters causing the table to be misaligned.

Example:

```
var tabulator = new TextTabulator();
var options = new TabulatorOptions
{
   TabLength = 4,
};

var table = tabulator.Tabulate(headers, values, options);
```
