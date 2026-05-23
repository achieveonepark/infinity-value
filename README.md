# Infinity Value

## Install

> Note: Check the version after `#` in the GitHub URL for the latest changes listed in the Changelog.

### Install via Unity Package Manager (UPM)
1. Open UPM and click the `+` button in the top left.
2. Select **Install package from git URL...**
3. Enter the following and click **Install**:
```
https://github.com/achieveonepark/InfinityValue.git#1.1.0
```

### Manual Addition

Open the `manifest.json` file in your Unity project's `Packages` folder and add the following line under `dependencies`:

```json
"com.achieve.infinity-value": "https://github.com/achieveonepark/InfinityValue.git#1.1.0"
```

---

## Description

A Unity-friendly struct for representing arbitrarily large numbers using segmented units (e.g. `"5.30B"`, `"120AA"`), designed for idle/incremental games.

- Internally stores up to 8 `(unitIndex, value)` pairs — no heap allocation, no GC pressure.
- All comparison and arithmetic operators supported with zero GC overhead.
- `ToString()` renders the highest significant unit with 2 decimal places (e.g. `"5.30B"`, `"120.05AA"`).
- Custom unit name systems supported via `SetUnitNames()`.
- If the Newtonsoft.Json package is installed (`USE_NEWTONSOFT_JSON` define), a `JsonConverter` is automatically registered for seamless serialization.

---

## Supported Constructors

| Type | Example |
|------|---------|
| `int` | `new InfinityValue(1000)` |
| `long` | `new InfinityValue(1_000_000L)` |
| `float` | `new InfinityValue(3.0f)` |
| `double` | `new InfinityValue(1.5e9)` |
| `BigInteger` | `new InfinityValue(new BigInteger(...))` |
| `string` | `new InfinityValue("300F 200C")` |

All types also support **implicit conversion**:
```csharp
InfinityValue v = 1000L;
InfinityValue v = 1.5e9;
InfinityValue v = "300F 200C";
```

---

## Supported Operators

| Category | Operators |
|----------|-----------|
| Arithmetic | `+`, `-`, `* long`, `* double`, `/ long`, `/ double` |
| Comparison | `==`, `!=`, `<`, `>`, `<=`, `>=` (with `InfinityValue` and `long`) |
| Conversion | `(long)`, `(float)`, `(double)` |

---

## How to Use

```csharp
using System.Collections.Generic;
using System.Numerics;
using Achieve.InfinityValue;

// (Optional) Use custom unit names instead of the default A, B, C... system
InfinityValue.SetUnitNames(new List<string>
{
    "", "K", "M", "B", "T", "Qa", "Qi", "Sx"
});

// Construction
InfinityValue a = 1000;                          // from int
InfinityValue b = "300F 200C";                   // from formatted string
InfinityValue c = 3.0f;                          // from float
InfinityValue d = 1.5e12;                        // from double
InfinityValue e = new BigInteger(30_000_000_000_000L);

// Safe string parsing
if (InfinityValue.TryParse("500B 200A", out InfinityValue parsed))
    Debug.Log(parsed); // "500.20B"

// Arithmetic
InfinityValue sum  = a + b;
InfinityValue diff = b - a;  // clamps to zero if result would be negative
InfinityValue mul  = b * 3L;
InfinityValue pct  = b * 1.25;  // 25% increase
InfinityValue div  = b / 2L;

// Comparison
bool isGreater = a > b;
bool isZero    = a == InfinityValue.Zero;

// Display — shows highest unit with 2 decimal places
Debug.Log(e.ToString()); // e.g. "30.00B"
Debug.Log(InfinityValue.Zero.ToString()); // "0"

// Explicit conversion (approximate for large values)
long   lv = (long)a;
double dv = (double)e;

// Static constants
InfinityValue zero = InfinityValue.Zero;
InfinityValue one  = InfinityValue.One;
```

---

## Default Unit Names

```
(none), A, B, C, ..., Z,
AA, AB, ..., AZ,
BA, BB, ..., BZ,
CA, CB, ..., CZ
```
Total: 105 units (supports values up to 999 CZ ≈ 10^316).
