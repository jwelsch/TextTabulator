# UniformHeaderUniformColumnAlignmentProvider

Namespace: TextTabulator

Provider that aligns all headers the same, while aligning the values in each column separately.

```csharp
public class UniformHeaderUniformColumnAlignmentProvider : ICellAlignmentProvider
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [UniformHeaderUniformColumnAlignmentProvider](./texttabulator.uniformheaderuniformcolumnalignmentprovider.md)<br>
Implements [ICellAlignmentProvider](./texttabulator.icellalignmentprovider.md)

## Constructors

### **UniformHeaderUniformColumnAlignmentProvider(IEnumerable&lt;CellAlignment&gt;, CellAlignment)**

Creates an object of type UniformAlignmentProvider.

```csharp
public UniformHeaderUniformColumnAlignmentProvider(IEnumerable<CellAlignment> uniformColumnAlignments, CellAlignment uniformHeaderAlignment)
```

#### Parameters

`uniformColumnAlignments` [IEnumerable&lt;CellAlignment&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

`uniformHeaderAlignment` [CellAlignment](./texttabulator.cellalignment.md)<br>

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
