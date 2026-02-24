# UniformAlignmentProvider

Namespace: TextTabulator

Provider that aligns all cells the same.

```csharp
public class UniformAlignmentProvider : ICellAlignmentProvider
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [UniformAlignmentProvider](./texttabulator.uniformalignmentprovider.md)<br>
Implements [ICellAlignmentProvider](./texttabulator.icellalignmentprovider.md)

## Constructors

### **UniformAlignmentProvider(CellAlignment)**

Creates an object of type UniformAlignmentProvider.

```csharp
public UniformAlignmentProvider(CellAlignment uniformAlignment)
```

#### Parameters

`uniformAlignment` [CellAlignment](./texttabulator.cellalignment.md)<br>
Alignment to use for all cells in the table, including headers.

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
