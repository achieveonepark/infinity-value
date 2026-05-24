# Getting Started

## 1. Setup

```csharp
using Achieve.InfinityValue;
```

No extra initialization required. The default unit system (`A`, `B` … `CZ`) is ready to use.

To use custom units like `K / M / B / T`, call once at startup:

```csharp
void Awake()
{
    InfinityValue.SetUnitNames(new List<string>
    {
        "", "K", "M", "B", "T", "Qa", "Qi", "Sx"
    });
}
```

---

## 2. Creating Values

```csharp
InfinityValue gold  = 1_000L;           // from long
InfinityValue score = 1.5e12;           // from double
InfinityValue saved = "300F 200C";      // from string

// Safe parse
if (!InfinityValue.TryParse(PlayerPrefs.GetString("gold"), out var loaded))
    loaded = InfinityValue.Zero;
```

---

## 3. Arithmetic

```csharp
InfinityValue a = 500_000L;
InfinityValue b = "300A";       // 300,000

var sum  = a + b;               // 800,000 → "800"
var diff = b - a;               // clamps to 0 if result < 0
var mul  = a * 3L;              // 1,500,000 → "1.50A"
var pct  = a * 1.25;            // 625,000 → "625"
var div  = a / 2L;              // 250,000 → "250"
```

> Subtraction **clamps to zero** — never goes negative.

---

## 4. Comparisons

```csharp
InfinityValue cost = "3A";

if (playerGold >= cost)
{
    playerGold -= cost;
    Debug.Log("Purchased! Remaining: " + playerGold);
}
```

---

## 5. Display

| Value | `ToString()` |
|---|---|
| `500` | `"500"` |
| `1,500` | `"1.50A"` |
| `5,300,000,000` | `"5.30B"` |
| `0` | `"0"` |

```csharp
goldLabel.text = playerGold.ToString();
```

---

## 6. Save & Load

```csharp
// Save
PlayerPrefs.SetString("gold", playerGold.ToString());

// Load
InfinityValue.TryParse(PlayerPrefs.GetString("gold", "0"), out var playerGold);
```
