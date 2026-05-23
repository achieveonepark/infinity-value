# Type Conversions

`InfinityValue` supports both **implicit** (lossless direction) and **explicit** (potentially lossy direction) conversions.

---

## Implicit Conversions (to InfinityValue)

These are lossless — you can assign without a cast.

| From type | Example |
|-----------|---------|
| `int` | `InfinityValue v = 1000;` |
| `long` | `InfinityValue v = 1_000_000L;` |
| `float` | `InfinityValue v = 3.0f;` |
| `double` | `InfinityValue v = 1.5e9;` |
| `string` | `InfinityValue v = "500B";` |
| `BigInteger` | `InfinityValue v = new BigInteger(…);` |

---

## Explicit Conversions (from InfinityValue)

These require a cast and may lose precision for large values.

### (long)

```csharp
public static explicit operator long(InfinityValue value)
```

Approximates the value as a `long`. Only the lowest two significant units are used. Values above `long.MaxValue` (~9.2 × 10^18) will overflow.

```csharp
InfinityValue v = "5B 300A";  // 5,300,000,000
long l = (long)v;
Debug.Log(l); // 5300000000
```

```csharp
InfinityValue large = "100C"; // 100,000,000,000,000 — exceeds long range
long l = (long)large;         // result is approximate / overflows
```

### (double)

```csharp
public static explicit operator double(InfinityValue value)
```

Converts to `double` by summing all units scaled to their magnitude. Loses precision for values with many significant digits.

```csharp
InfinityValue v = "5B 300A";
double d = (double)v;
Debug.Log(d); // 5300000000
```

```csharp
// Useful for progress calculations
float progress = (float)((double)currentXP / (double)requiredXP);
```

### (float)

```csharp
public static explicit operator float(InfinityValue value)
```

Delegates to `(double)` and then narrows to `float`. Significant precision loss for large values.

```csharp
InfinityValue v = "5B";
float f = (float)v;
Debug.Log(f); // 5E+09 (approximately)
```

---

## Precision Notes

| Conversion | Safe range | Notes |
|---|---|---|
| `(long)` | up to ~`9A` (`9.2 × 10^18`) | Uses lowest 2 units only |
| `(double)` | up to ~`1AW` (`10^52`) | IEEE 754 double has 15–17 significant digits |
| `(float)` | up to ~`1H` (`10^24`) | IEEE 754 float has ~7 significant digits |

For values beyond these ranges, use `ToString()` and string-based persistence rather than numeric conversions.
