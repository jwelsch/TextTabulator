# ReflectionTabulatorAdapterOptions

Namespace: TextTabulator.Adapters.Reflection

Options to allow configuration of the ReflectionTabulatorAdapter class.

```csharp
public class ReflectionTabulatorAdapterOptions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ReflectionTabulatorAdapterOptions](./texttabulator.adapters.reflection.reflectiontabulatoradapteroptions.md)<br>
Attributes [NullableContextAttribute](./system.runtime.compilerservices.nullablecontextattribute.md), [NullableAttribute](./system.runtime.compilerservices.nullableattribute.md)

## Properties

### **MemberNameTransform**

Gets the transform to apply to type member names.

```csharp
public INameTransform MemberNameTransform { get; }
```

#### Property Value

INameTransform<br>

### **TypeMembers**

Gets which type members to include in the output.

```csharp
public TypeMembers TypeMembers { get; }
```

#### Property Value

[TypeMembers](./texttabulator.adapters.reflection.typemembers.md)<br>

### **AccessModifiers**

Gets the desired access modifier(s) of the type members to include in the output.

```csharp
public AccessModifiers AccessModifiers { get; }
```

#### Property Value

[AccessModifiers](./texttabulator.adapters.reflection.accessmodifiers.md)<br>

### **TypeFormatter**

Gets the formatter to apply to cell values.

```csharp
public ITypeFormatter TypeFormatter { get; }
```

#### Property Value

ITypeFormatter<br>

### **AxesOrientation**

Gets the orientation of the axes. This determines whether type members are represented as rows or columns in the output.

```csharp
public AxesOrientation AxesOrientation { get; }
```

#### Property Value

AxesOrientation<br>

### **ColumnSorter**

Gets the sorter to use for the columns.

```csharp
public IColumnOrderSorter ColumnSorter { get; }
```

#### Property Value

IColumnOrderSorter<br>

## Constructors

### **ReflectionTabulatorAdapterOptions(INameTransform, TypeMembers, AccessModifiers, ITypeFormatter, AxesOrientation, IColumnOrderSorter)**

Creates an object of type ReflectionTabulatorAdapterOptions.

```csharp
public ReflectionTabulatorAdapterOptions(INameTransform memberNameTransform, TypeMembers typeMembers, AccessModifiers accessModifiers, ITypeFormatter typeFormatter, AxesOrientation axesOrientation, IColumnOrderSorter columnSorter)
```

#### Parameters

`memberNameTransform` INameTransform<br>
Transform to apply to type member names. Passing null will cause the member names to not be altered.

`typeMembers` [TypeMembers](./texttabulator.adapters.reflection.typemembers.md)<br>
Specifies which type members to include in the output.

`accessModifiers` [AccessModifiers](./texttabulator.adapters.reflection.accessmodifiers.md)<br>
Specifies the desired access modifier(s) of the type members to include in the output.

`typeFormatter` ITypeFormatter<br>
Formatter to apply to cell values. Passing null will cause the cell values to use default formatting.

`axesOrientation` AxesOrientation<br>
Specifies the orientation of the axes. This determines whether type members are represented as rows or columns in the output.

`columnSorter` IColumnOrderSorter<br>
Specifies the sorter to use for the columns. Passing null will use DefaultColumnOrderSorter.
