# ITableStyling

Namespace: TextTabulator

Interface used to implement table styling values.

```csharp
public interface ITableStyling
```

Attributes [NullableContextAttribute](./system.runtime.compilerservices.nullablecontextattribute.md)

## Properties

### **ColumnLeftPadding**

Gets or sets the string used for padding the left side of a column.
 The contents of a cell will be aligned within the left and right padding.

```csharp
public abstract string ColumnLeftPadding { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ColumnRightPadding**

Gets or sets the string used for padding the right side of a column.
 The contents of a cell will be aligned within the left and right padding.

```csharp
public abstract string ColumnRightPadding { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ColumnSeparator**

Gets or sets the character used for the column separator.

```csharp
public abstract char ColumnSeparator { get; set; }
```

#### Property Value

[Char](https://docs.microsoft.com/en-us/dotnet/api/system.char)<br>

### **ValueRowSeparator**

Gets or sets the character used for separating rows of values.

```csharp
public abstract char ValueRowSeparator { get; set; }
```

#### Property Value

[Char](https://docs.microsoft.com/en-us/dotnet/api/system.char)<br>

### **ValueLeftEdgeJoint**

Gets or sets the character used on the left edge of the table where a
 character separating rows of values meets it.

```csharp
public abstract char ValueLeftEdgeJoint { get; set; }
```

#### Property Value

[Char](https://docs.microsoft.com/en-us/dotnet/api/system.char)<br>

### **ValueMiddleJoint**

Gets or sets the character used where a row separator meets a column separator.

```csharp
public abstract char ValueMiddleJoint { get; set; }
```

#### Property Value

[Char](https://docs.microsoft.com/en-us/dotnet/api/system.char)<br>

### **ValueRightEdgeJoint**

Gets or sets the character used on the right edge of the table where a
 character separating rows of values meets it.

```csharp
public abstract char ValueRightEdgeJoint { get; set; }
```

#### Property Value

[Char](https://docs.microsoft.com/en-us/dotnet/api/system.char)<br>

### **HeaderRowSeparator**

Gets or sets the character used for separating the header row from the rows of values.

```csharp
public abstract char HeaderRowSeparator { get; set; }
```

#### Property Value

[Char](https://docs.microsoft.com/en-us/dotnet/api/system.char)<br>

### **HeaderLeftEdgeJoint**

Gets or sets the character used on the left edge of the table where a
 character separating the header row from the rows of values meets it.

```csharp
public abstract char HeaderLeftEdgeJoint { get; set; }
```

#### Property Value

[Char](https://docs.microsoft.com/en-us/dotnet/api/system.char)<br>

### **HeaderMiddleJoint**

Gets or sets the character used where a header separator meets a column separator.

```csharp
public abstract char HeaderMiddleJoint { get; set; }
```

#### Property Value

[Char](https://docs.microsoft.com/en-us/dotnet/api/system.char)<br>

### **HeaderRightEdgeJoint**

Gets or sets the character used on the right edge of the table where a
 character separating the header row from the rows of values meets it.

```csharp
public abstract char HeaderRightEdgeJoint { get; set; }
```

#### Property Value

[Char](https://docs.microsoft.com/en-us/dotnet/api/system.char)<br>

### **LeftEdge**

Gets or sets the character used for the left edge of the table.

```csharp
public abstract char LeftEdge { get; set; }
```

#### Property Value

[Char](https://docs.microsoft.com/en-us/dotnet/api/system.char)<br>

### **RightEdge**

Gets or sets the character used for the right edge of the table.

```csharp
public abstract char RightEdge { get; set; }
```

#### Property Value

[Char](https://docs.microsoft.com/en-us/dotnet/api/system.char)<br>

### **TopEdge**

Gets or sets the character used for the top edge of the table.

```csharp
public abstract char TopEdge { get; set; }
```

#### Property Value

[Char](https://docs.microsoft.com/en-us/dotnet/api/system.char)<br>

### **BottomEdge**

Gets or sets the character used for the bottom edge of the table.

```csharp
public abstract char BottomEdge { get; set; }
```

#### Property Value

[Char](https://docs.microsoft.com/en-us/dotnet/api/system.char)<br>

### **TopLeftCorner**

Gets or sets the character used for the top left corner of the table.

```csharp
public abstract char TopLeftCorner { get; set; }
```

#### Property Value

[Char](https://docs.microsoft.com/en-us/dotnet/api/system.char)<br>

### **TopRightCorner**

Gets or sets the character used for the top right corner of the table.

```csharp
public abstract char TopRightCorner { get; set; }
```

#### Property Value

[Char](https://docs.microsoft.com/en-us/dotnet/api/system.char)<br>

### **BottomRightCorner**

Gets or sets the character used for the bottom right corner of the table.

```csharp
public abstract char BottomRightCorner { get; set; }
```

#### Property Value

[Char](https://docs.microsoft.com/en-us/dotnet/api/system.char)<br>

### **BottomLeftCorner**

Gets or sets the character used for the bottom left corner of the table.

```csharp
public abstract char BottomLeftCorner { get; set; }
```

#### Property Value

[Char](https://docs.microsoft.com/en-us/dotnet/api/system.char)<br>

### **TopEdgeJoint**

Gets or sets the character used where a column separator meets the top table edge.

```csharp
public abstract char TopEdgeJoint { get; set; }
```

#### Property Value

[Char](https://docs.microsoft.com/en-us/dotnet/api/system.char)<br>

### **BottomEdgeJoint**

Gets or sets the character where a column separator meets the bottom table edge.

```csharp
public abstract char BottomEdgeJoint { get; set; }
```

#### Property Value

[Char](https://docs.microsoft.com/en-us/dotnet/api/system.char)<br>
