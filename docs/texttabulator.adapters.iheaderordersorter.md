# IHeaderOrderSorter

Namespace: TextTabulator.Adapters

Interface for sorting the headers themselves in a specific order.

```csharp
public interface IHeaderOrderSorter
```

Attributes [NullableContextAttribute](./system.runtime.compilerservices.nullablecontextattribute.md)

## Methods

### **Sort(IEnumerable&lt;String&gt;)**

Called to sort the header names.

```csharp
IEnumerable<string> Sort(IEnumerable<string> headerNames)
```

#### Parameters

`headerNames` [IEnumerable&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
The header names to sort.

#### Returns

[IEnumerable&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
The sorted header names.
