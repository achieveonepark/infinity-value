# API Reference

`InfinityValue` is a `partial struct` in the `Achieve.InfinityValue` namespace.

```csharp
using Achieve.InfinityValue;
```

---

## Quick Reference

### Static Members

| Member | Type | Description |
|--------|------|-------------|
| `Zero` | `InfinityValue` | The value `0` |
| `One` | `InfinityValue` | The value `1` |
| `SetUnitNames(List<string>)` | `void` | Replace the global unit name list |
| `TryParse(string, out InfinityValue)` | `bool` | Safe string parsing |

### Instance Members

| Member | Type | Description |
|--------|------|-------------|
| `IsEmpty` | `bool` | `true` when the value is `0` |
| `ToString()` | `string` | Renders as `"5.30B"` |
| `CompareTo(InfinityValue)` | `int` | `IComparable<T>` implementation |
| `Equals(InfinityValue)` | `bool` | `IEquatable<T>` implementation |
| `GetHashCode()` | `int` | Order-independent hash |

---

## Pages

- [Constructors](constructors.md) — how to create `InfinityValue` instances
- [Operators](operators.md) — arithmetic, comparison, implicit conversions
- [Methods](methods.md) — `ToString`, `TryParse`, `SetUnitNames`, `IsEmpty`
- [Type Conversions](conversion.md) — explicit casts to `long`, `float`, `double`
