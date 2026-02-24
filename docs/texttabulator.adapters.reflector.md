# Reflector

Namespace: TextTabulator.Adapters

```csharp
public class Reflector : IReflector
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Reflector](./texttabulator.adapters.reflector.md)<br>
Implements [IReflector](./texttabulator.adapters.ireflector.md)<br>
Attributes [NullableContextAttribute](./system.runtime.compilerservices.nullablecontextattribute.md), [NullableAttribute](./system.runtime.compilerservices.nullableattribute.md)

## Constructors

### **Reflector(Type)**

```csharp
public Reflector(Type type)
```

#### Parameters

`type` [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>

## Methods

### **GetFieldInfo(String, BindingFlags)**

```csharp
public FieldInfo GetFieldInfo(string fieldName, BindingFlags bindingFlags)
```

#### Parameters

`fieldName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`bindingFlags` [BindingFlags](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.bindingflags)<br>

#### Returns

[FieldInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.fieldinfo)<br>

### **GetFieldInfos(BindingFlags)**

```csharp
public FieldInfo[] GetFieldInfos(BindingFlags bindingFlags)
```

#### Parameters

`bindingFlags` [BindingFlags](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.bindingflags)<br>

#### Returns

[FieldInfo[]](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.fieldinfo)<br>

### **GetPropertyInfo(String, BindingFlags)**

```csharp
public PropertyInfo GetPropertyInfo(string propertyName, BindingFlags bindingFlags)
```

#### Parameters

`propertyName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`bindingFlags` [BindingFlags](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.bindingflags)<br>

#### Returns

[PropertyInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.propertyinfo)<br>

### **GetPropertyInfos(BindingFlags)**

```csharp
public PropertyInfo[] GetPropertyInfos(BindingFlags bindingFlags)
```

#### Parameters

`bindingFlags` [BindingFlags](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.bindingflags)<br>

#### Returns

[PropertyInfo[]](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.propertyinfo)<br>

### **GetMethodInfo(String, BindingFlags)**

```csharp
public MethodInfo GetMethodInfo(string methodName, BindingFlags bindingFlags)
```

#### Parameters

`methodName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`bindingFlags` [BindingFlags](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.bindingflags)<br>

#### Returns

[MethodInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodinfo)<br>

### **GetMethodInfos(BindingFlags)**

```csharp
public MethodInfo[] GetMethodInfos(BindingFlags bindingFlags)
```

#### Parameters

`bindingFlags` [BindingFlags](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.bindingflags)<br>

#### Returns

[MethodInfo[]](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodinfo)<br>

### **GetMemberInfos(MemberType, BindingFlags)**

```csharp
public MemberInfo[] GetMemberInfos(MemberType memberType, BindingFlags bindingFlags)
```

#### Parameters

`memberType` [MemberType](./texttabulator.adapters.membertype.md)<br>

`bindingFlags` [BindingFlags](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.bindingflags)<br>

#### Returns

[MemberInfo[]](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.memberinfo)<br>
