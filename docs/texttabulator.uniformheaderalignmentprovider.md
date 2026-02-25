# UniformHeaderAlignmentProvider

Namespace: TextTabulator

Provider that aligns all headers the same way, while allowing the alignment of each value to vary.

```csharp
public class UniformHeaderAlignmentProvider : ICellAlignmentProvider
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [UniformHeaderAlignmentProvider](./texttabulator.uniformheaderalignmentprovider.md)<br>
Implements [ICellAlignmentProvider](./texttabulator.icellalignmentprovider.md)

## Constructors

### **UniformHeaderAlignmentProvider(IEnumerable&lt;IEnumerable&lt;CellAlignment&gt;&gt;, CellAlignment)**

Creates an object of type UniformAlignmentProvider.

```csharp
public UniformHeaderAlignmentProvider(IEnumerable<IEnumerable<CellAlignment>> valueAlignments, CellAlignment uniformHeaderAlignment)
```

#### Parameters

`valueAlignments` [IEnumerable&lt;IEnumerable&lt;CellAlignment&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

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
