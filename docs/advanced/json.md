# JSON Serialization

`InfinityValue` supports automatic JSON serialization via **Newtonsoft.Json** when enabled.

---

## Enabling JSON Support

1. Install the Newtonsoft.Json package in your Unity project (e.g. via `com.unity.nuget.newtonsoft-json`).
2. Add the scripting define symbol:
   - **Edit → Project Settings → Player → Scripting Define Symbols**
   - Add: `USE_NEWTONSOFT_JSON`

---

## How It Works

When `USE_NEWTONSOFT_JSON` is defined, the `[JsonConverter(typeof(InfinityValueConverter))]` attribute is applied to `InfinityValue`. The converter:

- **Serializes** by calling `ToString()` — e.g. `"5.30B"`
- **Deserializes** by calling `new InfinityValue(string)` — parses the stored string back

```csharp
using Newtonsoft.Json;

[System.Serializable]
public class PlayerSave
{
    public InfinityValue Gold;
    public InfinityValue Score;
}

// Serialize
var save = new PlayerSave { Gold = 5_300_000_000L, Score = "12C" };
string json = JsonConvert.SerializeObject(save);
// {"Gold":"5.30B","Score":"12.00C"}

// Deserialize
var loaded = JsonConvert.DeserializeObject<PlayerSave>(json);
Debug.Log(loaded.Gold);  // "5.30B"
Debug.Log(loaded.Score); // "12.00C"
```

---

## Saving to PlayerPrefs (without JSON)

If you prefer not to use JSON, save values directly as strings:

```csharp
// Save
PlayerPrefs.SetString("gold", playerGold.ToString());
PlayerPrefs.Save();

// Load
if (!InfinityValue.TryParse(PlayerPrefs.GetString("gold", "0"), out InfinityValue playerGold))
    playerGold = InfinityValue.Zero;
```

---

## Round-trip Safety

Because serialization uses `ToString()` and deserialization uses the string constructor, the round-trip is safe as long as:

- The **unit name list has not changed** between save and load.
- The stored string was produced by `ToString()` (which always uses valid unit names).

If you change `SetUnitNames` between app versions, existing saves will fail to parse — plan for migration or use a stable unit name set from the start.
