# CellData

Namespace: TextTabulator

```csharp
public class CellData : ICellData
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [CellData](./texttabulator.celldata.md)<br>
Implements [ICellData](./texttabulator.icelldata.md)

## Properties

### **Column**

```csharp
public int Column { get; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Row**

```csharp
public int Row { get; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Lines**

```csharp
public IReadOnlyList<string> Lines { get; }
```

#### Property Value

[IReadOnlyList&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

### **Width**

```csharp
public int Width { get; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Height**

```csharp
public int Height { get; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

## Constructors

### **CellData(Int32, Int32, IReadOnlyList&lt;String&gt;, Int32, Int32)**

```csharp
public CellData(int column, int row, IReadOnlyList<string> lines, int width, int height)
```

#### Parameters

`column` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`row` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`lines` [IReadOnlyList&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

`width` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`height` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
