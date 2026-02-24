# IReflector

Namespace: TextTabulator.Adapters

```csharp
public interface IReflector
```

Attributes [NullableContextAttribute](./system.runtime.compilerservices.nullablecontextattribute.md)

## Methods

### **GetFieldInfo(String, BindingFlags)**

```csharp
FieldInfo GetFieldInfo(string fieldName, BindingFlags bindingFlags)
```

#### Parameters

`fieldName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`bindingFlags` [BindingFlags](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.bindingflags)<br>

#### Returns

[FieldInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.fieldinfo)<br>

### **GetFieldInfos(BindingFlags)**

```csharp
FieldInfo[] GetFieldInfos(BindingFlags bindingFlags)
```

#### Parameters

`bindingFlags` [BindingFlags](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.bindingflags)<br>

#### Returns

[FieldInfo[]](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.fieldinfo)<br>

### **GetPropertyInfo(String, BindingFlags)**

```csharp
PropertyInfo GetPropertyInfo(string propertyName, BindingFlags bindingFlags)
```

#### Parameters

`propertyName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`bindingFlags` [BindingFlags](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.bindingflags)<br>

#### Returns

[PropertyInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.propertyinfo)<br>

### **GetPropertyInfos(BindingFlags)**

```csharp
PropertyInfo[] GetPropertyInfos(BindingFlags bindingFlags)
```

#### Parameters

`bindingFlags` [BindingFlags](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.bindingflags)<br>

#### Returns

[PropertyInfo[]](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.propertyinfo)<br>

### **GetMethodInfo(String, BindingFlags)**

```csharp
MethodInfo GetMethodInfo(string methodName, BindingFlags bindingFlags)
```

#### Parameters

`methodName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`bindingFlags` [BindingFlags](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.bindingflags)<br>

#### Returns

[MethodInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodinfo)<br>

### **GetMethodInfos(BindingFlags)**

```csharp
MethodInfo[] GetMethodInfos(BindingFlags bindingFlags)
```

#### Parameters

`bindingFlags` [BindingFlags](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.bindingflags)<br>

#### Returns

[MethodInfo[]](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodinfo)<br>

### **GetMemberInfos(MemberType, BindingFlags)**

```csharp
MemberInfo[] GetMemberInfos(MemberType memberType, BindingFlags bindingFlags)
```

#### Parameters

`memberType` [MemberType](./texttabulator.adapters.membertype.md)<br>

`bindingFlags` [BindingFlags](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.bindingflags)<br>

#### Returns

[MemberInfo[]](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.memberinfo)<br>
