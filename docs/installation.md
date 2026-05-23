# Installation

## Requirements

- Unity **2022.3** or later
- .NET Standard 2.1+

---

## Via Unity Package Manager (UPM)

1. Open the **Package Manager** window (`Window → Package Manager`).
2. Click the **`+`** button in the top-left corner.
3. Select **Add package from git URL...**
4. Enter the following URL and click **Add**:

```
https://github.com/achieveonepark/InfinityValue.git#1.1.0
```

---

## Via manifest.json

Open `Packages/manifest.json` in your project and add the entry under `dependencies`:

```json
{
  "dependencies": {
    "com.achieve.infinity-value": "https://github.com/achieveonepark/InfinityValue.git#1.1.0"
  }
}
```

---

## Optional: Newtonsoft.Json Support

If you have **Newtonsoft.Json** (e.g. `com.unity.nuget.newtonsoft-json`) installed, add the scripting define symbol to enable automatic JSON serialization:

1. Go to **Edit → Project Settings → Player**.
2. Under **Scripting Define Symbols**, add:
   ```
   USE_NEWTONSOFT_JSON
   ```

See [JSON Serialization](advanced/json.md) for usage details.

---

## Verifying the Install

Add the following to any MonoBehaviour to confirm the package is working:

```csharp
using Achieve.InfinityValue;
using UnityEngine;

public class InstallCheck : MonoBehaviour
{
    void Start()
    {
        InfinityValue v = 1_500_000L;
        Debug.Log(v); // Expected: "1.50A"
    }
}
```

> **Next:** [Getting Started →](getting-started.md)
