# TabulatorOptions

Namespace: TextTabulator

Used for specifying options used when constructing the table.

```csharp
public class TabulatorOptions : ITabulatorOptions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [TabulatorOptions](./texttabulator.tabulatoroptions.md)<br>
Implements [ITabulatorOptions](./texttabulator.itabulatoroptions.md)<br>
Attributes [NullableContextAttribute](./system.runtime.compilerservices.nullablecontextattribute.md), [NullableAttribute](./system.runtime.compilerservices.nullableattribute.md)

## Properties

### **Styling**

Gets or sets the styling of the table.

```csharp
public ITableStyling Styling { get; set; }
```

#### Property Value

[ITableStyling](./texttabulator.itablestyling.md)<br>

### **CellAlignment**

Gets or sets the alignment of cells in the table.

```csharp
public ICellAlignmentProvider CellAlignment { get; set; }
```

#### Property Value

[ICellAlignmentProvider](./texttabulator.icellalignmentprovider.md)<br>

### **NewLine**

Gets or sets the characters to use for new lines in the table.

```csharp
public string NewLine { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **IncludeNonPrintableCharacters**

Gets or sets whether to include non-printable characters in the generated table. Setting false will cause any
 non-printable characters to be removed. Setting true will cause any non-printable characters to be left in the table.
 This has no effect on tab ('\t'), carriage return ('\r'), or new line ('\n') characters.

```csharp
public bool IncludeNonPrintableCharacters { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **TabLength**

Gets or sets the number of spaces to substitute for each tab character in the table. If this is set to 0, tab
 characters will be used in the table. If this is set to a positive number, tab characters will be replaced with
 the specified number of spaces. If this is set to a negative number, tab characters will be removed.

```csharp
public int TabLength { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

## Constructors

### **TabulatorOptions()**

```csharp
public TabulatorOptions()
```
