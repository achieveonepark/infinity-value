# Methods & Properties

## Static Properties

### Zero
```csharp
public static readonly InfinityValue Zero;
```
The value `0`. Equivalent to `default(InfinityValue)`.

### One
```csharp
public static readonly InfinityValue One;
```

## Static Methods

### TryParse
```csharp
public static bool TryParse(string input, out InfinityValue result)
```
Safe alternative to the string constructor. Returns `false` on unknown unit names.

```csharp
if (!InfinityValue.TryParse(savedString, out var gold))
    gold = InfinityValue.Zero;
```

**Accepted input:**

| Input | Result |
|---|---|
| `"0"` or `""` | `Zero` (returns `true`) |
| `"500"` | 500 |
| `"5B"` | 5,000,000 |
| `"300F 200C"` | 300F + 200C |
| `"999XYZ"` | `false` — unknown unit |

### SetUnitNames
```csharp
public static void SetUnitNames(List<string> unitNames)
```
Replaces the global unit name list. Call once at startup. See [Custom Unit Names](/en/advanced/unit-names).

## Instance Properties

### IsEmpty
```csharp
public bool IsEmpty { get; }
```
`true` when the value is `0`. Allocation-free.

## Instance Methods

### ToString
```csharp
public override string ToString()
```
Renders the highest unit with 2 decimal places from the next lower unit.

| Value | Output |
|---|---|
| `999` | `"999"` |
| `1,500` | `"1.50A"` |
| `5,300,000,000` | `"5.30B"` |
| `0` | `"0"` |
| Exceeds table | `"Infinity"` |

### CompareTo / Equals / GetHashCode
Standard `IComparable<T>` and `IEquatable<T>` implementations, all allocation-free and order-independent (semantic equality by unit index, not internal slot order).
