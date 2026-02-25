# HeaderOrderSorter

Namespace: TextTabulator.Adapters

A header order sorter. Use the static members for built-in sorting options, or create a new instance with a custom sorting function.

```csharp
public class HeaderOrderSorter : IHeaderOrderSorter
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [HeaderOrderSorter](./texttabulator.adapters.headerordersorter.md)<br>
Implements [IHeaderOrderSorter](./texttabulator.adapters.iheaderordersorter.md)<br>
Attributes [NullableContextAttribute](./system.runtime.compilerservices.nullablecontextattribute.md), [NullableAttribute](./system.runtime.compilerservices.nullableattribute.md)

## Fields

### **Default**

The default header order sorter.

```csharp
public static DefaultHeaderOrderSorter Default;
```

### **Ascending**

Sorts the headers in ascending alphabetical order.

```csharp
public static AscendingHeaderOrderSorter Ascending;
```

### **Descending**

Sorts the headers in descending alphabetical order.

```csharp
public static DescendingHeaderOrderSorter Descending;
```

## Constructors

### **HeaderOrderSorter(Func&lt;IEnumerable&lt;String&gt;, IEnumerable&lt;String&gt;&gt;)**

Creates an object of type HeaderOrderSorter.

```csharp
public HeaderOrderSorter(Func<IEnumerable<string>, IEnumerable<string>> customSorter)
```

#### Parameters

`customSorter` [Func&lt;IEnumerable&lt;String&gt;, IEnumerable&lt;String&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-2)<br>
Custom sorting function. It takes the unsorted header names as an argument and returns the sorted header names.

## Methods

### **Sort(IEnumerable&lt;String&gt;)**

Called to sort the header names.

```csharp
public IEnumerable<string> Sort(IEnumerable<string> headerNames)
```

#### Parameters

`headerNames` [IEnumerable&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
The header names to sort.

#### Returns

[IEnumerable&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
The sorted header names.
