# UniformHeaderUniformValueAlignmentProvider

Namespace: TextTabulator

Provider that allows a single alignment to be set for all headers and another one to be set for all values.

```csharp
public class UniformHeaderUniformValueAlignmentProvider : ICellAlignmentProvider
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [UniformHeaderUniformValueAlignmentProvider](./texttabulator.uniformheaderuniformvaluealignmentprovider.md)<br>
Implements [ICellAlignmentProvider](./texttabulator.icellalignmentprovider.md)

## Constructors

### **UniformHeaderUniformValueAlignmentProvider(CellAlignment, CellAlignment)**

Creates an object of type UniformAlignmentProvider.

```csharp
public UniformHeaderUniformValueAlignmentProvider(CellAlignment uniformHeaderAlignment, CellAlignment uniformValueAlignment)
```

#### Parameters

`uniformHeaderAlignment` [CellAlignment](./texttabulator.cellalignment.md)<br>

`uniformValueAlignment` [CellAlignment](./texttabulator.cellalignment.md)<br>

## Methods

### **GetHeaderAlignment(Int32)**

Returns the alignment for the header in the specified column.

```csharp
public CellAlignment GetHeaderAlignment(int columnIndex)
```

#### Parameters

`columnIndex` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
Zero-based index of the column to return the alignment for.

#### Returns

[CellAlignment](./texttabulator.cellalignment.md)<br>
Alignment of the header.

### **GetValueAlignment(Int32, Int32)**

Returns the alignment for the value at the specified column and row.

```csharp
public CellAlignment GetValueAlignment(int columnIndex, int rowIndex)
```

#### Parameters

`columnIndex` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
Zero-based index of the row to return the alignment for.

`rowIndex` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
Zero-based index of the column to return the alignment for.

#### Returns

[CellAlignment](./texttabulator.cellalignment.md)<br>
Alignment of the value.
