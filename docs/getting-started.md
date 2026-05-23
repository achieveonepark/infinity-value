# Getting Started

This page walks through the most common patterns for using `InfinityValue` in a game.

---

## 1. Basic Setup

```csharp
using Achieve.InfinityValue;
```

No additional initialization is required. The default unit system (`A`, `B`, `C` … `CZ`) is ready to use out of the box.

If you prefer units like `K / M / B / T`, configure them once at startup:

```csharp
void Awake()
{
    InfinityValue.SetUnitNames(new List<string>
    {
        "", "K", "M", "B", "T", "Qa", "Qi", "Sx", "Sp"
    });
}
```

See [Custom Unit Names](advanced/unit-names.md) for details.

---

## 2. Creating Values

```csharp
// From numeric literals (implicit conversion)
InfinityValue gold  = 1_000L;
InfinityValue score = 3_500_000_000L;

// From double (e.g. large values from formulas)
InfinityValue xp = 1.5e12;

// From a saved string (e.g. PlayerPrefs)
InfinityValue loaded = PlayerPrefs.GetString("gold", "0");

// Safe parse with error handling
if (!InfinityValue.TryParse(PlayerPrefs.GetString("score"), out InfinityValue savedScore))
    savedScore = InfinityValue.Zero;
```

---

## 3. Arithmetic

```csharp
InfinityValue a = 500_000L;
InfinityValue b = "300A";   // 300,000

InfinityValue sum  = a + b;       // 800,000 → "800"
InfinityValue diff = b - a;       // clamps to 0 if negative
InfinityValue mul  = a * 3L;      // 1,500,000 → "1.50A"
InfinityValue pct  = a * 1.25;    // 625,000  → "625"
InfinityValue div  = a / 2L;      // 250,000  → "250"
```

> Subtraction never goes negative — it clamps to zero. This matches the expected behavior for resource values in games.

---

## 4. Comparisons

```csharp
InfinityValue playerGold = 5_000_000L;
InfinityValue itemCost   = "3A";      // 3,000,000

if (playerGold >= itemCost)
{
    playerGold -= itemCost;
    Debug.Log("Item purchased! Remaining: " + playerGold); // "2.00A"
}
```

---

## 5. Displaying Values

`ToString()` renders the **highest unit** with up to 2 decimal places derived from the next lower unit:

| Stored value | ToString() output |
|---|---|
| 500 | `"500"` |
| 1,500 | `"1.50A"` |
| 1,005,000 | `"1.00B"` |
| 5,300,000,000 | `"5.30B"` |
| 0 | `"0"` |

```csharp
InfinityValue v = 5_300_000_000L;
Debug.Log(v.ToString()); // "5.30B"

// Use directly in UI
goldText.text = playerGold.ToString();
```

---

## 6. Saving & Loading

```csharp
// Save
PlayerPrefs.SetString("gold", playerGold.ToString());

// Load
InfinityValue.TryParse(PlayerPrefs.GetString("gold", "0"), out InfinityValue playerGold);
```

> **Next:** [API Reference →](api/README.md)
