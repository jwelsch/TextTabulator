# ColumnOrderSorter

Namespace: TextTabulator.Adapters

A column order sorter. Use the static members for built-in sorting options, or create a new instance with a custom sorting function.

```csharp
public class ColumnOrderSorter : IColumnOrderSorter
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ColumnOrderSorter](./texttabulator.adapters.columnordersorter.md)<br>
Implements [IColumnOrderSorter](./texttabulator.adapters.icolumnordersorter.md)<br>
Attributes [NullableContextAttribute](./system.runtime.compilerservices.nullablecontextattribute.md), [NullableAttribute](./system.runtime.compilerservices.nullableattribute.md)

## Fields

### **Default**

The default column order sorter.

```csharp
public static DefaultColumnOrderSorter Default;
```

### **Ascending**

Sorts the columns in ascending alphabetical order.

```csharp
public static AscendingColumnOrderSorter Ascending;
```

### **Descending**

Sorts the columns in descending alphabetical order.

```csharp
public static DescendingColumnOrderSorter Descending;
```

## Constructors

### **ColumnOrderSorter(Func&lt;IEnumerable&lt;String&gt;, IEnumerable&lt;String&gt;&gt;)**

Creates an object of type ColumnOrderSorter.

```csharp
public ColumnOrderSorter(Func<IEnumerable<string>, IEnumerable<string>> customSorter)
```

#### Parameters

`customSorter` [Func&lt;IEnumerable&lt;String&gt;, IEnumerable&lt;String&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-2)<br>
Custom sorting function. It takes the unsorted column names as an argument and returns the sorted column names.

## Methods

### **Sort(IEnumerable&lt;String&gt;)**

Called to sort the column names.

```csharp
public IEnumerable<string> Sort(IEnumerable<string> columnNames)
```

#### Parameters

`columnNames` [IEnumerable&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
The column names to sort.

#### Returns

[IEnumerable&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
The sorted column names.
