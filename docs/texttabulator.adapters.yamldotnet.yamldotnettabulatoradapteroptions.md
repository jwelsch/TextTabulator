# YamlDotNetTabulatorAdapterOptions

Namespace: TextTabulator.Adapters.YamlDotNet

Options to allow configuration of the YamlDotNetTabulatorAdapter class.

```csharp
public class YamlDotNetTabulatorAdapterOptions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [YamlDotNetTabulatorAdapterOptions](./texttabulator.adapters.yamldotnet.yamldotnettabulatoradapteroptions.md)<br>
Attributes [NullableContextAttribute](./system.runtime.compilerservices.nullablecontextattribute.md), [NullableAttribute](./system.runtime.compilerservices.nullableattribute.md)

## Properties

### **NodeNameTransform**

Gets the transform to apply to YAML node names.

```csharp
public INameTransform NodeNameTransform { get; }
```

#### Property Value

INameTransform<br>

## Constructors

### **YamlDotNetTabulatorAdapterOptions(INameTransform)**

Creates an object of type JsonTabulatorAdapterOptions.

```csharp
public YamlDotNetTabulatorAdapterOptions(INameTransform nodeNameTransform)
```

#### Parameters

`nodeNameTransform` INameTransform<br>
Transform to apply to YAML node names. Passing null will cause the YAML node names to not be altered.
