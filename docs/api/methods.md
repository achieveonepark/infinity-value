# 메서드 및 프로퍼티

---

## 정적 프로퍼티

### Zero

```csharp
public static readonly InfinityValue Zero;
```

값 `0`을 나타냅니다. `default(InfinityValue)`와 동일합니다.

```csharp
InfinityValue score = InfinityValue.Zero;
Debug.Log(score.IsEmpty); // true
```

### One

```csharp
public static readonly InfinityValue One;
```

값 `1`을 나타냅니다.

```csharp
InfinityValue counter = InfinityValue.One;
```

---

## 정적 메서드

### TryParse

```csharp
public static bool TryParse(string input, out InfinityValue result)
```

문자열을 `InfinityValue`로 파싱을 시도합니다. 성공 시 `true`, 알 수 없는 단위명이나 잘못된 숫자가 포함된 경우 `false`를 반환합니다.

외부에서 불러온 데이터나 저장된 문자열을 파싱할 때 string 생성자 대신 이 메서드를 사용하세요.

```csharp
// PlayerPrefs에서 안전하게 불러오기
string raw = PlayerPrefs.GetString("gold", "0");

if (InfinityValue.TryParse(raw, out InfinityValue gold))
{
    Debug.Log("불러오기 성공: " + gold);
}
else
{
    Debug.LogWarning("저장 데이터 손상 — 골드를 초기화합니다.");
    gold = InfinityValue.Zero;
}
```

**허용 입력 형식:**

| 입력 문자열 | 결과 |
|---|---|
| `"0"` | `Zero` |
| `"500"` | `500` |
| `"1A"` | `1,000` |
| `"5B"` | `5,000,000` |
| `"300F 200C"` | `300F + 200C` |
| `"5.30B"` | `5B 30A`로 파싱 |
| `""` 또는 `null` | `Zero` (returns `true`) |
| `"999XYZ"` | `false` — 알 수 없는 단위 |

---

### SetUnitNames

```csharp
public static void SetUnitNames(List<string> unitNames)
```

전역 단위 이름 목록을 교체합니다. 앱 시작 시 한 번 호출하세요 (예: `Awake`). 자세한 안내는 [커스텀 단위 이름](/advanced/unit-names) 페이지를 참고하세요.

```csharp
InfinityValue.SetUnitNames(new List<string>
{
    "", "K", "M", "B", "T"
});
```

---

## 인스턴스 프로퍼티

### IsEmpty

```csharp
public bool IsEmpty { get; }
```

값이 `0`인 경우(내부 슬롯에 아무것도 없는 경우) `true`를 반환합니다. 힙 할당이 없습니다.

```csharp
InfinityValue v = InfinityValue.Zero;
Debug.Log(v.IsEmpty); // true

v += 1L;
Debug.Log(v.IsEmpty); // false
```

---

## 인스턴스 메서드

### ToString

```csharp
public override string ToString()
```

**가장 높은 유효 단위**를 기준으로 소수 2자리까지 표현합니다. 소수점 아래는 다음 낮은 단위에서 산출됩니다.

| 값 | 출력 |
|---|---|
| `0` | `"0"` |
| `999` | `"999"` |
| `1,500` | `"1.50A"` |
| `5,300,000,000` | `"5.30B"` |
| `1,005,000` | `"1.00B"` |
| 단위 범위 초과 | `"Infinity"` |

```csharp
InfinityValue v = 5_300_000_000L;
Debug.Log(v); // "5.30B"

// UI 텍스트에 바로 사용
goldLabel.text = playerGold.ToString();
```

---

### CompareTo

```csharp
public int CompareTo(InfinityValue other)
```

`IComparable<InfinityValue>` 구현입니다. 음수, 0, 양수를 반환합니다. 힙 할당이 없습니다.

```csharp
var list = new List<InfinityValue> { "5B", "200A", "3C" };
list.Sort(); // CompareTo 사용
// 결과: 200A, 5B, 3C
```

---

### Equals

```csharp
public bool Equals(InfinityValue other)
```

`IEquatable<InfinityValue>` 구현입니다. 내부 슬롯 순서가 아닌 **의미론적 값**으로 비교합니다. 힙 할당이 없습니다.

```csharp
InfinityValue a = 1_000_000L;
InfinityValue b = "1B";
Debug.Log(a == b); // true
```
