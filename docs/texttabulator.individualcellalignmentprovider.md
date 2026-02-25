# IndividualCellAlignmentProvider

Namespace: TextTabulator

Provider that allows each cell to be aligned separately.

```csharp
public class IndividualCellAlignmentProvider : ICellAlignmentProvider
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [IndividualCellAlignmentProvider](./texttabulator.individualcellalignmentprovider.md)<br>
Implements [ICellAlignmentProvider](./texttabulator.icellalignmentprovider.md)<br>
Attributes [NullableContextAttribute](./system.runtime.compilerservices.nullablecontextattribute.md), [NullableAttribute](./system.runtime.compilerservices.nullableattribute.md)

## Constructors

### **IndividualCellAlignmentProvider(IEnumerable&lt;CellAlignment&gt;, IEnumerable&lt;IEnumerable&lt;CellAlignment&gt;&gt;)**

Creates an object of type UniformAlignmentProvider.

```csharp
public IndividualCellAlignmentProvider(IEnumerable<CellAlignment> headerAlignments, IEnumerable<IEnumerable<CellAlignment>> valueAlignments)
```

#### Parameters

`headerAlignments` [IEnumerable&lt;CellAlignment&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

`valueAlignments` [IEnumerable&lt;IEnumerable&lt;CellAlignment&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

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
