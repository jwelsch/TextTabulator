# TextTabulator Project

This is the core library of `TextTabulator`, and is what does the actual table formation.

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

A final category of overload accepts an `ITabulatorAdapter` interface.

### String type overloads

```
public string Tabulate(IEnumerable<IEnumerable<string>> rowValues, TabulatorOptions? options = null)
public string Tabulate(IEnumerable<string> headers, IEnumerable<IEnumerable<string>> rowValues, TabulatorOptions? options = null)
```

For the `string` type overloads, the value of each string object will be used as each cell's content. Each `string` will be read once for each call to `Tabulator.Tabulate`.

### Object type overloads

```
public string Tabulate(IEnumerable<IEnumerable<object>> rowValues, TabulatorOptions? options = null)
public string Tabulate(IEnumerable<object> headers, IEnumerable<IEnumerable<object>> rowValues, TabulatorOptions? options = null)
```

For the `object` type overloads, the type's `ToString` method will be called to generate the content of the cell. Each `object` will have its `ToString` method called once for each call to `Tabulator.Tabulate`.

### CellValue delegate type overloads

```
public string Tabulate(IEnumerable<IEnumerable<CellValue>> rowValues, TabulatorOptions? options = null);
public string Tabulate(IEnumerable<CellValue> headers, IEnumerable<IEnumerable<CellValue>> rowValues, TabulatorOptions? options = null)
```

For the `CellValue` delegate type overloads, the delegate will be invoked to generate the content of the cell. It can be used to generate content dynamically. Each `CellValue` will be invoked only once per call to `Tabulator.Tabulate`.

The delegate `CellValue` has the signature:

```
public delegate string CellValue();
```

### `ITabulatorAdapter` type overloads

```
public string Tabulte(ITabulatorAdapter adapter, TabulatorOptions? options = null)
```

This overload allows `Tabulator.Tabulate` to more easily integrate with other types of formats of data. Currently supported adapters include CSV and reflection over objects. More information can be found in each adapter's project:
- [CsvHelper adapter](../TextTabulator.Adapters.CsvHelper)
- [Reflection adapter](../TextTabulator.Adapters.Reflection)

## Tabulation Options

There is a configuration class, called `TableOptions`, that can be used to control various aspects of the table. It derives from `ITableOptions`, which can be used to implement custom values, if necessary. Most developers will likely use `TableOptions`, though.

### Alignment

If a cell's contents do not span the full width of a column, it can be aligned such that the content will appear consistently on the left, right, or in the center. This can be accomplished by setting the `CellAlignment` property in an `ITableOptions` object. The `CellAlignment` property can be set with type that implements `ICellAlignmentProvider`. If the default `ICellAlignmentProvider` is used, all cell contents will be aligned to the left.

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

The style of the table structure can be controlled by setting the `Styling` property in an `ITableOptions` object and passing it to the `Tabulator.Tabulate` method. There are two types that implement `ITableStyle` out of the box: `AsciiTableStyling` and `UnicodeTableStyling`. You can set the properties of either to further customize the styling of the table. The default table styling is the same as `AsciiTableStyling`.

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

### New Lines

By default `TextTabulator.Tabulate` uses the `Environment.NewLine` for new lines. However, characters to use for new lines can be set by assigning a value to `TableOptions.NewLines`.

Example:

```
var tabulator = new TextTabulator();
var options = new TabulatorOptions
{
   NewLine = "\n",
};

var table = tabulator.Tabulate(headers, values, options);
```
