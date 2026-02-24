# Tabulator

Namespace: TextTabulator

Main class that performs the tabulation of data.

```csharp
public class Tabulator
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Tabulator](./texttabulator.tabulator.md)<br>
Attributes [NullableContextAttribute](./system.runtime.compilerservices.nullablecontextattribute.md), [NullableAttribute](./system.runtime.compilerservices.nullableattribute.md)

## Constructors

### **Tabulator()**

```csharp
public Tabulator()
```

## Methods

### **Tabulate(ITabulatorAdapter, TableCallback, TabulatorOptions)**

Tabulates data and makes callbacks with elements of the table.

```csharp
public void Tabulate(ITabulatorAdapter adapter, TableCallback callback, TabulatorOptions options)
```

#### Parameters

`adapter` ITabulatorAdapter<br>
Adapter object that the method can get data from.

`callback` [TableCallback](./texttabulator.tablecallback.md)<br>
Callback received when an element of the table is constructed.

`options` [TabulatorOptions](./texttabulator.tabulatoroptions.md)<br>
Options specifying how the table should be constructed.

### **Tabulate(IEnumerable&lt;IEnumerable&lt;CellValue&gt;&gt;, TableCallback, TabulatorOptions)**

Tabulates data and makes callbacks with elements of the table.

```csharp
public void Tabulate(IEnumerable<IEnumerable<CellValue>> rowValues, TableCallback callback, TabulatorOptions options)
```

#### Parameters

`rowValues` [IEnumerable&lt;IEnumerable&lt;CellValue&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
Enumeration containing CellValues delegates for each value in each row.

`callback` [TableCallback](./texttabulator.tablecallback.md)<br>
Callback received when an element of the table is constructed.

`options` [TabulatorOptions](./texttabulator.tabulatoroptions.md)<br>
Options specifying how the table should be constructed.

### **Tabulate(IEnumerable&lt;CellValue&gt;, IEnumerable&lt;IEnumerable&lt;CellValue&gt;&gt;, TableCallback, TabulatorOptions)**

Tabulates data and makes callbacks with elements of the table.

```csharp
public void Tabulate(IEnumerable<CellValue> headers, IEnumerable<IEnumerable<CellValue>> rowValues, TableCallback callback, TabulatorOptions options)
```

#### Parameters

`headers` [IEnumerable&lt;CellValue&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
Enumeration containing CellValues delegates for each header.

`rowValues` [IEnumerable&lt;IEnumerable&lt;CellValue&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
Enumeration containing CellValues delegates for each value in each row.

`callback` [TableCallback](./texttabulator.tablecallback.md)<br>
Callback received when an element of the table is constructed.

`options` [TabulatorOptions](./texttabulator.tabulatoroptions.md)<br>
Options specifying how the table should be constructed.

### **Tabulate(IEnumerable&lt;IEnumerable&lt;Object&gt;&gt;, TableCallback, TabulatorOptions)**

Tabulates data and makes callbacks with elements of the table.

```csharp
public void Tabulate(IEnumerable<IEnumerable<object>> rowValues, TableCallback callback, TabulatorOptions options)
```

#### Parameters

`rowValues` [IEnumerable&lt;IEnumerable&lt;Object&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
Enumeration containing objects for each value in each row. Each object's ToString() method will be called to generate the value displayed in the table.

`callback` [TableCallback](./texttabulator.tablecallback.md)<br>
Callback received when an element of the table is constructed.

`options` [TabulatorOptions](./texttabulator.tabulatoroptions.md)<br>
Options specifying how the table should be constructed.

### **Tabulate(IEnumerable&lt;Object&gt;, IEnumerable&lt;IEnumerable&lt;Object&gt;&gt;, TableCallback, TabulatorOptions)**

Tabulates data and makes callbacks with elements of the table.

```csharp
public void Tabulate(IEnumerable<object> headers, IEnumerable<IEnumerable<object>> rowValues, TableCallback callback, TabulatorOptions options)
```

#### Parameters

`headers` [IEnumerable&lt;Object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
Enumeration containing objects for each header. Each object's ToString() method will be called to generate the value displayed in the table.

`rowValues` [IEnumerable&lt;IEnumerable&lt;Object&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
Enumeration containing objects for each value in each row. Each object's ToString() method will be called to generate the value displayed in the table.

`callback` [TableCallback](./texttabulator.tablecallback.md)<br>
Callback received when an element of the table is constructed.

`options` [TabulatorOptions](./texttabulator.tabulatoroptions.md)<br>
Options specifying how the table should be constructed.

### **Tabulate(IEnumerable&lt;IEnumerable&lt;String&gt;&gt;, TableCallback, TabulatorOptions)**

Tabulates data and makes callbacks with elements of the table.

```csharp
public void Tabulate(IEnumerable<IEnumerable<string>> rowValues, TableCallback callback, TabulatorOptions options)
```

#### Parameters

`rowValues` [IEnumerable&lt;IEnumerable&lt;String&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
Enumeration containing strings for each value in each row.

`callback` [TableCallback](./texttabulator.tablecallback.md)<br>
Callback received when an element of the table is constructed.

`options` [TabulatorOptions](./texttabulator.tabulatoroptions.md)<br>
Options specifying how the table should be constructed.

### **Tabulate(IEnumerable&lt;String&gt;, IEnumerable&lt;IEnumerable&lt;String&gt;&gt;, TableCallback, TabulatorOptions)**

Tabulates data and makes callbacks with elements of the table.

```csharp
public void Tabulate(IEnumerable<string> headers, IEnumerable<IEnumerable<string>> rowValues, TableCallback callback, TabulatorOptions options)
```

#### Parameters

`headers` [IEnumerable&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
Enumeration containing strings for each header.

`rowValues` [IEnumerable&lt;IEnumerable&lt;String&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

`callback` [TableCallback](./texttabulator.tablecallback.md)<br>
Callback received when an element of the table is constructed.

`options` [TabulatorOptions](./texttabulator.tabulatoroptions.md)<br>
Options specifying how the table should be constructed.

### **Tabulate(ITabulatorAdapter, TabulatorOptions)**

Tabulates data and outputs a string representation of a table.

```csharp
public string Tabulate(ITabulatorAdapter adapter, TabulatorOptions options)
```

#### Parameters

`adapter` ITabulatorAdapter<br>
Adapter object that the method can get data from.

`options` [TabulatorOptions](./texttabulator.tabulatoroptions.md)<br>
Options specifying how the table should be constructed.

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
String representation of a table.

### **Tabulate(IEnumerable&lt;IEnumerable&lt;CellValue&gt;&gt;, TabulatorOptions)**

Tabulates data and outputs a string representation of a table.

```csharp
public string Tabulate(IEnumerable<IEnumerable<CellValue>> rowValues, TabulatorOptions options)
```

#### Parameters

`rowValues` [IEnumerable&lt;IEnumerable&lt;CellValue&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
Enumeration containing CellValues delegates for each value in each row.

`options` [TabulatorOptions](./texttabulator.tabulatoroptions.md)<br>
Options specifying how the table should be constructed.

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
String representation of a table.

### **Tabulate(IEnumerable&lt;CellValue&gt;, IEnumerable&lt;IEnumerable&lt;CellValue&gt;&gt;, TabulatorOptions)**

Tabulates data and outputs a string representation of a table.

```csharp
public string Tabulate(IEnumerable<CellValue> headers, IEnumerable<IEnumerable<CellValue>> rowValues, TabulatorOptions options)
```

#### Parameters

`headers` [IEnumerable&lt;CellValue&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
Enumeration containing CellValues delegates for each header.

`rowValues` [IEnumerable&lt;IEnumerable&lt;CellValue&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
Enumeration containing CellValues delegates for each value in each row.

`options` [TabulatorOptions](./texttabulator.tabulatoroptions.md)<br>
Options specifying how the table should be constructed.

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
String representation of a table.

### **Tabulate(IEnumerable&lt;IEnumerable&lt;Object&gt;&gt;, TabulatorOptions)**

Tabulates data and outputs a string representation of a table.

```csharp
public string Tabulate(IEnumerable<IEnumerable<object>> rowValues, TabulatorOptions options)
```

#### Parameters

`rowValues` [IEnumerable&lt;IEnumerable&lt;Object&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
Enumeration containing objects for each value in each row. Each object's ToString() method will be called to generate the value displayed in the table.

`options` [TabulatorOptions](./texttabulator.tabulatoroptions.md)<br>
Options specifying how the table should be constructed.

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
String representation of a table.

### **Tabulate(IEnumerable&lt;Object&gt;, IEnumerable&lt;IEnumerable&lt;Object&gt;&gt;, TabulatorOptions)**

Tabulates data and outputs a string representation of a table.

```csharp
public string Tabulate(IEnumerable<object> headers, IEnumerable<IEnumerable<object>> rowValues, TabulatorOptions options)
```

#### Parameters

`headers` [IEnumerable&lt;Object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
Enumeration containing objects for each header. Each object's ToString() method will be called to generate the value displayed in the table.

`rowValues` [IEnumerable&lt;IEnumerable&lt;Object&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
Enumeration containing objects for each value in each row. Each object's ToString() method will be called to generate the value displayed in the table.

`options` [TabulatorOptions](./texttabulator.tabulatoroptions.md)<br>
Options specifying how the table should be constructed.

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
String representation of a table.

### **Tabulate(IEnumerable&lt;IEnumerable&lt;String&gt;&gt;, TabulatorOptions)**

Tabulates data and outputs a string representation of a table.

```csharp
public string Tabulate(IEnumerable<IEnumerable<string>> rowValues, TabulatorOptions options)
```

#### Parameters

`rowValues` [IEnumerable&lt;IEnumerable&lt;String&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
Enumeration containing strings for each value in each row.

`options` [TabulatorOptions](./texttabulator.tabulatoroptions.md)<br>
Options specifying how the table should be constructed.

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
String representation of a table.

### **Tabulate(IEnumerable&lt;String&gt;, IEnumerable&lt;IEnumerable&lt;String&gt;&gt;, TabulatorOptions)**

Tabulates data and outputs a string representation of a table.

```csharp
public string Tabulate(IEnumerable<string> headers, IEnumerable<IEnumerable<string>> rowValues, TabulatorOptions options)
```

#### Parameters

`headers` [IEnumerable&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
Enumeration containing strings for each header.

`rowValues` [IEnumerable&lt;IEnumerable&lt;String&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

`options` [TabulatorOptions](./texttabulator.tabulatoroptions.md)<br>
Options specifying how the table should be constructed.

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
String representation of a table.

#### Exceptions

[Exception](https://docs.microsoft.com/en-us/dotnet/api/system.exception)<br>
Thrown when the number of headers does not match the number of values in each row.
