# JSON Serialization

## Enabling

1. Install Newtonsoft.Json (e.g. `com.unity.nuget.newtonsoft-json`).
2. Add the scripting define: **Edit → Project Settings → Player → Scripting Define Symbols:**
   ```
   USE_NEWTONSOFT_JSON
   ```

## Usage

The `InfinityValueConverter` is automatically applied via `[JsonConverter]`. It serializes via `ToString()` and deserializes via the string constructor.

```csharp
using Newtonsoft.Json;

[System.Serializable]
public class PlayerSave
{
    public InfinityValue Gold;
    public InfinityValue Score;
}

var save = new PlayerSave { Gold = 5_300_000_000L, Score = "12C" };
string json = JsonConvert.SerializeObject(save);
// {"Gold":"5.30B","Score":"12.00C"}

var loaded = JsonConvert.DeserializeObject<PlayerSave>(json);
Debug.Log(loaded.Gold);  // "5.30B"
```

## PlayerPrefs (without JSON)

```csharp
// Save
PlayerPrefs.SetString("gold", playerGold.ToString());

// Load
InfinityValue.TryParse(PlayerPrefs.GetString("gold", "0"), out var playerGold);
```

## Round-trip Notes

Serialization uses `ToString()` and deserialization uses the string constructor — safe as long as the **unit name list has not changed** between save and load.
