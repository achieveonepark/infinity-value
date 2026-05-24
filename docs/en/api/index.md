# API Reference

```csharp
using Achieve.InfinityValue;
```

## Static Members

| Member | Type | Description |
|--------|------|-------------|
| `Zero` | `InfinityValue` | The value `0` |
| `One` | `InfinityValue` | The value `1` |
| `SetUnitNames(List<string>)` | `void` | Replace the global unit name list |
| `TryParse(string, out InfinityValue)` | `bool` | Safe string parsing |

## Instance Members

| Member | Type | Description |
|--------|------|-------------|
| `IsEmpty` | `bool` | `true` when the value is `0` |
| `ToString()` | `string` | e.g. `"5.30B"` |
| `CompareTo(InfinityValue)` | `int` | `IComparable<T>` |
| `Equals(InfinityValue)` | `bool` | `IEquatable<T>` |
| `GetHashCode()` | `int` | Order-independent hash |

## Pages

- [Constructors](/en/api/constructors)
- [Operators](/en/api/operators)
- [Methods & Properties](/en/api/methods)
- [Type Conversions](/en/api/conversion)
