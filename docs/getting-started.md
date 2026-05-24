# 빠른 시작

`InfinityValue`를 게임에서 사용하는 가장 일반적인 패턴을 순서대로 설명합니다.

---

## 1. 네임스페이스 추가

```csharp
using Achieve.InfinityValue;
```

별도 초기화는 필요하지 않습니다. 기본 단위 체계(`A`, `B`, `C` … `CZ`)가 바로 사용 가능합니다.

`K / M / B / T` 같은 단위를 선호한다면 앱 시작 시 한 번 설정하세요.

```csharp
void Awake()
{
    InfinityValue.SetUnitNames(new List<string>
    {
        "", "K", "M", "B", "T", "Qa", "Qi", "Sx", "Sp"
    });
}
```

자세한 내용은 [커스텀 단위 이름](/advanced/unit-names) 페이지를 참고하세요.

---

## 2. 값 생성하기

```csharp
// 숫자 리터럴에서 생성 (암묵적 변환)
InfinityValue gold  = 1_000L;
InfinityValue score = 3_500_000_000L;

// double 수식 결과에서 생성
InfinityValue xp = 1.5e12;

// 저장된 문자열에서 생성 (PlayerPrefs 등)
InfinityValue loaded = PlayerPrefs.GetString("gold", "0");

// 오류 처리가 필요한 경우 TryParse 사용
if (!InfinityValue.TryParse(PlayerPrefs.GetString("score"), out InfinityValue savedScore))
    savedScore = InfinityValue.Zero;
```

---

## 3. 산술 연산

```csharp
InfinityValue a = 500_000L;
InfinityValue b = "300A";   // 300,000

InfinityValue sum  = a + b;       // 800,000 → "800"
InfinityValue diff = b - a;       // 음수가 되면 0으로 클램프
InfinityValue mul  = a * 3L;      // 1,500,000 → "1.50A"
InfinityValue pct  = a * 1.25;    // 625,000  → "625"
InfinityValue div  = a / 2L;      // 250,000  → "250"
```

> 뺄셈은 결과가 음수가 될 경우 자동으로 0으로 클램프됩니다. 게임 내 자원 값에서 기대되는 동작입니다.

---

## 4. 비교 연산

```csharp
InfinityValue playerGold = 5_000_000L;
InfinityValue itemCost   = "3A";      // 3,000,000

if (playerGold >= itemCost)
{
    playerGold -= itemCost;
    Debug.Log("아이템 구매 완료! 잔여 골드: " + playerGold); // "2.00A"
}
```

---

## 5. 값 표시하기

`ToString()`은 **가장 높은 단위**를 기준으로 최대 소수 2자리까지 렌더링합니다.

| 저장된 값 | ToString() 출력 |
|---|---|
| 500 | `"500"` |
| 1,500 | `"1.50A"` |
| 1,005,000 | `"1.00B"` |
| 5,300,000,000 | `"5.30B"` |
| 0 | `"0"` |

```csharp
InfinityValue v = 5_300_000_000L;
Debug.Log(v.ToString()); // "5.30B"

// UI 텍스트에 바로 사용
goldText.text = playerGold.ToString();
```

---

## 6. 저장 및 불러오기

```csharp
// 저장
PlayerPrefs.SetString("gold", playerGold.ToString());
PlayerPrefs.Save();

// 불러오기
if (!InfinityValue.TryParse(PlayerPrefs.GetString("gold", "0"), out InfinityValue playerGold))
    playerGold = InfinityValue.Zero;
```

> **다음:** [API 레퍼런스 →](/api/)
