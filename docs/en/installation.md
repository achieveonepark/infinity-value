# Installation

## Requirements

- Unity **2022.3** or later
- .NET Standard 2.1+

---

## Via Unity Package Manager (UPM)

1. Open **Package Manager** (`Window → Package Manager`).
2. Click **`+`** → **Add package from git URL...**
3. Enter and click **Add**:

```
https://github.com/achieveonepark/InfinityValue.git#1.1.0
```

## Via manifest.json

Add to `Packages/manifest.json` under `dependencies`:

```json
{
  "dependencies": {
    "com.achieve.infinity-value": "https://github.com/achieveonepark/InfinityValue.git#1.1.0"
  }
}
```

---

## Optional: Newtonsoft.Json Support

Add the scripting define symbol to enable automatic JSON serialization:

**Edit → Project Settings → Player → Scripting Define Symbols:**
```
USE_NEWTONSOFT_JSON
```

See [JSON Serialization](/en/advanced/json) for details.

---

## Verify

```csharp
using Achieve.InfinityValue;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Start()
    {
        InfinityValue v = 1_500_000L;
        Debug.Log(v); // "1.50A"
    }
}
```
