# Methods & Properties

---

## Static Properties

### Zero

```csharp
public static readonly InfinityValue Zero;
```

The value `0`. Equivalent to `default(InfinityValue)`.

```csharp
InfinityValue score = InfinityValue.Zero;
Debug.Log(score.IsEmpty); // true
```

### One

```csharp
public static readonly InfinityValue One;
```

The value `1`.

```csharp
InfinityValue counter = InfinityValue.One;
```

---

## Static Methods

### TryParse

```csharp
public static bool TryParse(string input, out InfinityValue result)
```

Attempts to parse a string into an `InfinityValue`. Returns `true` on success, `false` if the input contains an unrecognised unit name or invalid number.

Use this instead of the `string` constructor when parsing user-supplied or externally loaded data.

```csharp
// Safe load from PlayerPrefs
string raw = PlayerPrefs.GetString("gold", "0");

if (InfinityValue.TryParse(raw, out InfinityValue gold))
{
    Debug.Log("Loaded: " + gold);
}
else
{
    Debug.LogWarning("Corrupted save — resetting gold.");
    gold = InfinityValue.Zero;
}
```

**Accepted formats:**

| Input string | Result |
|---|---|
| `"0"` | `Zero` |
| `"500"` | `500` |
| `"1A"` | `1,000` |
| `"5B"` | `5,000,000` |
| `"300F 200C"` | `300F + 200C` |
| `"5.30B"` | parsed as `5B 30A` |
| `""` or `null` | `Zero` (returns `true`) |
| `"999XYZ"` | `false` — unknown unit |

---

### SetUnitNames

```csharp
public static void SetUnitNames(List<string> unitNames)
```

Replaces the global unit name list used for display and parsing. Call once during app startup (e.g. `Awake`). See [Custom Unit Names](../advanced/unit-names.md) for a full guide.

```csharp
InfinityValue.SetUnitNames(new List<string>
{
    "", "K", "M", "B", "T"
});
```

---

## Instance Properties

### IsEmpty

```csharp
public bool IsEmpty { get; }
```

Returns `true` when the value is `0` (no internal units stored). Allocation-free.

```csharp
InfinityValue v = InfinityValue.Zero;
Debug.Log(v.IsEmpty); // true

v += 1L;
Debug.Log(v.IsEmpty); // false
```

---

## Instance Methods

### ToString

```csharp
public override string ToString()
```

Renders the value as a human-readable string using the **highest significant unit** with 2 decimal places sourced from the next lower unit.

| Value | Output |
|---|---|
| `0` | `"0"` |
| `999` | `"999"` |
| `1,500` | `"1.50A"` |
| `5,300,000,000` | `"5.30B"` |
| `1,005,000` | `"1.00B"` |
| Exceeds unit table | `"Infinity"` |

```csharp
InfinityValue v = 5_300_000_000L;
Debug.Log(v); // "5.30B"

// Use directly with UI Text
goldLabel.text = playerGold.ToString();
```

---

### CompareTo

```csharp
public int CompareTo(InfinityValue other)
```

`IComparable<InfinityValue>` implementation. Returns negative, zero, or positive. Allocation-free.

```csharp
var list = new List<InfinityValue> { "5B", "200A", "3C" };
list.Sort(); // uses CompareTo
// result: 200A, 5B, 3C
```

---

### Equals

```csharp
public bool Equals(InfinityValue other)
```

`IEquatable<InfinityValue>` implementation. Compares by **semantic value**, not internal slot order. Allocation-free.

```csharp
InfinityValue a = 1_000_000L;
InfinityValue b = "1B";
Debug.Log(a == b); // true
```

---

### GetHashCode

```csharp
public override int GetHashCode()
```

Order-independent hash derived from unit index/value pairs. Safe for use in `Dictionary` and `HashSet`. Allocation-free.
