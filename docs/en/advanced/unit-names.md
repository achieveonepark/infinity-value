# Custom Unit Names

## Default System

105 names (index 0 = no suffix):
```
(none), A, B, C … Z,  AA, AB … AZ,  BA … BZ,  CA … CZ
```

| Index | Name | Scale |
|---|---|---|
| 0 | *(none)* | × 1 |
| 1 | A | × 1,000 |
| 2 | B | × 1,000,000 |
| … | … | … |
| 104 | CZ | ≈ 10^312 |

## Replacing Unit Names

Call `SetUnitNames` once at startup. **Index 0 must be an empty string.**

```csharp
void Awake()
{
    InfinityValue.SetUnitNames(new List<string>
    {
        "", "K", "M", "B", "T", "Qa", "Qi", "Sx"
    });
}
```

After this, values display with the new names and string parsing recognises them:
```csharp
InfinityValue v = 5_300_000_000L;
Debug.Log(v); // "5.30B"  (billions)
```

## Notes

- `SetUnitNames` is a **global static** — affects all instances.
- Call **before** any `InfinityValue` is constructed or parsed.
- Changing names at runtime will cause saved strings to mismatch — only change at startup.
