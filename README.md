# TextTabulator

TextTabulator is a library that will format data into a `string` that, when printed, will be in the form of a table. TextTabulator is designed to be simple and lightweight. Just call the `Tabulator.Tabulate` method with a collection of strings and it will do the rest.

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
║Ankyosaurus      │          4.8│Herbivore│    66 mya║
╟─────────────────┼─────────────┼─────────┼──────────╢
║Stegosaurus      │          3.8│Herbivore│   147 mya║
╟─────────────────┼─────────────┼─────────┼──────────╢
║Hadrosaurus      │            3│Herbivore│    66 mya║
╚═════════════════╧═════════════╧═════════╧══════════╝
```

## How to use

There is one public method and a few configuration options. The simplest way to use `TextTabulator` is to call the `Tabulator.Tabulate` method with the default options.

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

var tabulator = new TextTabulator();

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

## Overloads

The `Tabulator.Tabulate` method is overloaded. It can be called with collections of `string` types, `object` types, or `CellValue` delegate types. All three additionally have overloads with or without headers.

```
public string Tabulate(IEnumerable<IEnumerable<string>> rowValues, TabulatorOptions? options = null)
public string Tabulate(IEnumerable<string> headers, IEnumerable<IEnumerable<string>> rowValues, TabulatorOptions? options = null)

public string Tabulate(IEnumerable<IEnumerable<object>> rowValues, TabulatorOptions? options = null)
public string Tabulate(IEnumerable<object> headers, IEnumerable<IEnumerable<object>> rowValues, TabulatorOptions? options = null)

public string Tabulate(IEnumerable<IEnumerable<CellValue>> rowValues, TabulatorOptions? options = null);
public string Tabulate(IEnumerable<CellValue> headers, IEnumerable<IEnumerable<CellValue>> rowValues, TabulatorOptions? options = null)
```

For the `object` type overloads, the type's `ToString` method will be called to generate the content of the cell.

For the `CellValue` delegate type overloads, the delegate will be invoked to generate the content of the cell. It can be used to generate content dynamically. Each `CellValue` will be invoked only once per call to `Tabulator.Tabulate`. The delegate `CellValue` has the signature:
```
public delegate string CellValue();
```

## Tabulation Options

There is a configuration class that can be used to control various aspects of the table called `TableOptions`. It derives from `ITableOptions`, which can be used to implement custom values, if necessary. Most developers will likely use `TableOptions`, though.

### Alignment

If a cell's contents do not span the full width of a column, it can be aligned such that the content will appear consistently on the left, right, or in the center. This can be accomplished by setting the `Alignment` property in an `ITableOptions` object. The `Alignment` property can be set with type that implements `ICellAlignmentProvider`. If the default `ICellAlignmentProvider` is used, all cell contents will be aligned to the left.

There are four `CellAlignment` values:
- `Left`: Aligns text to the left of the cell.
- `Right`: Aligns text to the right of the cell.
- `CenterLeftBias`: Attempts to center text within the cell. If the text cannot be exactly centered, the extra space will appear on the right.
- `CenterRightBias`: Attempts to center text within the cell. If the text cannot be exactly centered, the extra space will appear on the left. 

The alignment can be set using an `ICellAlignmentProvider`.

There are numerous preconfigured `ICellAlignmentProvider` implementations that come built-in.

`UniformAlignmentProvider` aligns all cells the same way.
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

`UniformValueAlignmentProvider` aligns all values the same way, while allowing the alignment of each header to vary.
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

`UniformHeaderUniformValueAlignmentProvider` allows one alignment to be set for all headers and another one to be set for all values.
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

`UniformHeaderUniformColumnAlignmentProvider` aligns all headers the same way, while aligning the values in each column separately. 
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

You can set the alignment provider with the following code:
```
var tabulator = new TextTabulator();
var options = new TabulatorOptions
{
   Alignment = new UniformHeaderUniformColumnAlignmentProvider(new CellAlignment[] { CellAlignment.Left, CellAlignment.Right }, CellAlignment.CenterLeftBias)
};

var table = tabulator.Tabulate(headers, values, options);
```

As a convenience, the last alignment in a collection passed to one of the built-in `ICellAlignmentProvider` implementations will be used if the number of alignments is less than the number of actual cells to align. For example, say that a table has three columns and a `UniformColumnAlignmentProvider` is used. If the `UniformColumnAlignmentProvider` constructor is only given two alignments, the last column will be aligned using the last alignment value in the collection that it was passed.
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

var table = tabulator.Tabulate(headers, values, new TabulatorOptions { Alignment = alignment });

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

The style of the table can be controlled by setting the `Styling` property in an `ITableOptions` object and passing it to the `Tabulator.Tabulate` method. There are two types that implement `ITableStyle` out of the box: `AsciiTableStyling` and `UnicodeTableStyling`. You can override the properties of either to further customize the styling of the table. The default styling is the same as `AsciiTableStyling`.

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

`UnicodeTableStyling` uses character values that are outside of the traditional ASCII range. There are Unicode characters that are specifically designed to create tables, which is what the properties in `UnicodeTableStyling` default to.

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