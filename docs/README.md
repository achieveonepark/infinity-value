# Infinity Value

**Infinity Value** is a Unity-friendly `struct` for representing arbitrarily large numbers using segmented units — perfect for idle and incremental games.

```csharp
InfinityValue gold = "5.30B";
gold += 200_000_000L;

Debug.Log(gold); // "5.50B"
```

---

## Why Infinity Value?

Standard numeric types (`int`, `long`, `double`) either overflow or lose precision once values grow beyond a trillion. `BigInteger` solves precision but carries heavy GC overhead.

**Infinity Value takes a different approach:**

- Stores values as up to **8 segmented (unitIndex, amount) pairs** — no heap allocation.
- All operations (`+`, `-`, `*`, `/`, comparisons) are **allocation-free**.
- Renders naturally as `"1.23B"`, `"500AA"`, `"12.00CZ"` — no formatting code needed.
- Supports **custom unit names** for localization or alternative notation (K/M/B/T, etc.).

---

## Feature Overview

| Feature | Details |
|---------|---------|
| Max representable value | 999 CZ ≈ 10^316 |
| Internal storage | Up to 8 `(int, long)` slots, value-type only |
| GC allocations | None (arithmetic, comparison, `IsEmpty`) |
| Supported input types | `int`, `long`, `float`, `double`, `string`, `BigInteger` |
| Arithmetic | `+`, `-`, `* long/double`, `/ long/double` |
| ToString format | `"5.30B"`, `"120.05AA"` (highest unit + 2 decimal places) |
| JSON support | Optional via Newtonsoft.Json (`USE_NEWTONSOFT_JSON` define) |

---

## Quick Example

```csharp
using Achieve.InfinityValue;

InfinityValue hp     = 1_000_000L;
InfinityValue damage = "500A";

hp -= damage;
Debug.Log(hp);          // "500.00A"
Debug.Log(hp > 0L);     // true
Debug.Log(hp * 1.5);    // "750.00A"
```

> **Next:** [Installation →](installation.md)
