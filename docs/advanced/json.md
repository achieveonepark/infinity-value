# JSON 직렬화

`InfinityValue`는 **Newtonsoft.Json**이 활성화된 경우 자동 JSON 직렬화를 지원합니다.

---

## JSON 지원 활성화

1. Unity 프로젝트에 Newtonsoft.Json 패키지를 설치합니다 (`com.unity.nuget.newtonsoft-json` 등).
2. 스크립팅 정의 심볼을 추가합니다.
   - **Edit → Project Settings → Player → Scripting Define Symbols**
   - `USE_NEWTONSOFT_JSON` 추가

---

## 동작 방식

`USE_NEWTONSOFT_JSON`이 정의되어 있으면 `[JsonConverter(typeof(InfinityValueConverter))]` 어트리뷰트가 `InfinityValue`에 자동으로 적용됩니다.

- **직렬화**: `ToString()`을 호출 — 예: `"5.30B"`
- **역직렬화**: `new InfinityValue(string)`을 호출 — 저장된 문자열을 다시 파싱

```csharp
using Newtonsoft.Json;

[System.Serializable]
public class PlayerSave
{
    public InfinityValue Gold;
    public InfinityValue Score;
}

// 직렬화
var save = new PlayerSave { Gold = 5_300_000_000L, Score = "12C" };
string json = JsonConvert.SerializeObject(save);
// {"Gold":"5.30B","Score":"12.00C"}

// 역직렬화
var loaded = JsonConvert.DeserializeObject<PlayerSave>(json);
Debug.Log(loaded.Gold);  // "5.30B"
Debug.Log(loaded.Score); // "12.00C"
```

---

## PlayerPrefs로 저장하기 (JSON 미사용)

JSON을 사용하지 않아도 문자열로 직접 저장할 수 있습니다.

```csharp
// 저장
PlayerPrefs.SetString("gold", playerGold.ToString());
PlayerPrefs.Save();

// 불러오기
if (!InfinityValue.TryParse(PlayerPrefs.GetString("gold", "0"), out InfinityValue playerGold))
    playerGold = InfinityValue.Zero;
```

---

## 왕복 안전성 (Round-trip Safety)

직렬화에 `ToString()`을 사용하고 역직렬화에 string 생성자를 사용하기 때문에, 아래 조건이 유지되는 한 왕복은 안전합니다.

- 저장과 불러오기 사이에 **단위 이름 목록이 변경되지 않아야** 합니다.
- 저장된 문자열이 `ToString()`으로 생성된 것이어야 합니다 (항상 유효한 단위 이름을 사용).

앱 버전 업그레이드 시 `SetUnitNames`를 변경하면 기존 저장 데이터 파싱이 실패할 수 있습니다. 처음부터 안정적인 단위 이름 집합을 사용하거나 마이그레이션 전략을 미리 계획하세요.
