# Custom Unit Names

By default, `InfinityValue` uses an alphabetical unit system (`A`, `B`, `C` … `CZ`). You can replace this with any naming convention your game requires.

---

## Default Unit Table

The built-in list contains 105 names (index 0 = no suffix):

```
(none), A, B, C, D, E, F, G, H, I, J, K, L, M,
N, O, P, Q, R, S, T, U, V, W, X, Y, Z,
AA, AB, AC, ... AZ,
BA, BB, BC, ... BZ,
CA, CB, CC, ... CZ
```

Each unit represents a value 1,000× the previous one:

| Unit index | Unit name | Scale |
|---|---|---|
| 0 | *(none)* | × 1 |
| 1 | A | × 1,000 |
| 2 | B | × 1,000,000 |
| 3 | C | × 10^9 |
| … | … | … |
| 104 | CZ | ≈ 10^312 |

---

## Replacing the Unit Names

Call `SetUnitNames` once at startup with your custom list. **Index 0 must be an empty string** (the no-suffix tier).

```csharp
void Awake()
{
    InfinityValue.SetUnitNames(new List<string>
    {
        "",    // index 0: raw number (e.g. "500")
        "K",   // index 1: thousands
        "M",   // index 2: millions
        "B",   // index 3: billions
        "T",   // index 4: trillions
        "Qa",  // index 5: quadrillions
        "Qi",  // index 6: quintillions
        "Sx",  // index 7: sextillions
    });
}
```

After this call, values render using the new names and string parsing recognises them:

```csharp
InfinityValue gold = 5_300_000_000L;
Debug.Log(gold); // "5.30B"  (billions)

InfinityValue v = "12K";  // parsed as 12,000
```

---

## Localised Unit Names

You can load unit names from a localisation file and call `SetUnitNames` at startup or whenever the language changes:

```csharp
// Example: load from a ScriptableObject
public UnitNamesConfig unitConfig; // assign in Inspector

void Awake()
{
    InfinityValue.SetUnitNames(unitConfig.names);
}
```

---

## Important Notes

- `SetUnitNames` affects **all** `InfinityValue` instances globally — it is a static setting.
- Call it **before** any `InfinityValue` is constructed or parsed from strings.
- Changing unit names at runtime will cause previously rendered strings and saved strings to mismatch. Only change names at startup.
- The list must have at least 2 entries (index 0 and index 1). More entries extend the maximum representable value.
