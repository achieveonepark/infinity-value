# Type Conversions

## Implicit (to InfinityValue)

| From | Example |
|------|---------|
| `int` | `InfinityValue v = 1000;` |
| `long` | `InfinityValue v = 1_000_000L;` |
| `float` | `InfinityValue v = 3.0f;` |
| `double` | `InfinityValue v = 1.5e9;` |
| `string` | `InfinityValue v = "500B";` |
| `BigInteger` | `InfinityValue v = new BigInteger(…);` |

## Explicit (from InfinityValue)

### (long)
Uses only the lowest two significant units. Overflows for values above `long.MaxValue` (~9.2 × 10^18).

```csharp
InfinityValue v = "5B 300A";
long l = (long)v;  // 5300000000
```

### (double)
Sums all units scaled to their magnitude.

```csharp
double d = (double)v;

// Useful for ratios
float progress = (float)((double)currentXP / (double)requiredXP);
```

### (float)
Delegates to `(double)` then narrows.

## Precision Reference

| Cast | Safe up to | Notes |
|---|---|---|
| `(long)` | ~`9A` | IEEE 64-bit integer range |
| `(double)` | ~`1AW` | 15–17 significant digits |
| `(float)` | ~`1H` | ~7 significant digits |

For larger values, use `ToString()` for persistence rather than numeric casts.
