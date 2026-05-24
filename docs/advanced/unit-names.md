# 커스텀 단위 이름

기본적으로 `InfinityValue`는 알파벳 단위 체계(`A`, `B`, `C` … `CZ`)를 사용합니다. 게임에 맞는 이름 체계로 자유롭게 교체할 수 있습니다.

---

## 기본 단위 표

내장 목록은 105개 항목으로 구성됩니다 (인덱스 0 = 단위 없음).

```
(없음), A, B, C, D, E, F, G, H, I, J, K, L, M,
N, O, P, Q, R, S, T, U, V, W, X, Y, Z,
AA, AB, AC, ... AZ,
BA, BB, BC, ... BZ,
CA, CB, CC, ... CZ
```

각 단위는 이전 단위의 1,000배를 나타냅니다.

| 단위 인덱스 | 단위 이름 | 규모 |
|---|---|---|
| 0 | *(없음)* | × 1 |
| 1 | A | × 1,000 |
| 2 | B | × 1,000,000 |
| 3 | C | × 10^9 |
| … | … | … |
| 104 | CZ | ≈ 10^312 |

---

## 단위 이름 교체하기

앱 시작 시 `SetUnitNames`를 한 번 호출하여 커스텀 목록으로 교체합니다. **인덱스 0은 반드시 빈 문자열**이어야 합니다 (단위 없는 숫자 티어).

```csharp
void Awake()
{
    InfinityValue.SetUnitNames(new List<string>
    {
        "",    // 인덱스 0: 원시 숫자 (예: "500")
        "K",   // 인덱스 1: 천 (thousands)
        "M",   // 인덱스 2: 백만 (millions)
        "B",   // 인덱스 3: 십억 (billions)
        "T",   // 인덱스 4: 조 (trillions)
        "Qa",  // 인덱스 5: 천조 (quadrillions)
        "Qi",  // 인덱스 6: 백경 (quintillions)
        "Sx",  // 인덱스 7: 십해 (sextillions)
    });
}
```

설정 후에는 새 단위 이름으로 표시되고 문자열 파싱도 해당 이름을 인식합니다.

```csharp
InfinityValue gold = 5_300_000_000L;
Debug.Log(gold); // "5.30B" (십억)

InfinityValue v = "12K";  // 12,000으로 파싱
```

---

## 로컬라이제이션 연동

언어별 단위 이름을 외부 설정에서 불러와 `SetUnitNames`를 호출할 수도 있습니다.

```csharp
// 예: ScriptableObject에서 불러오기
public UnitNamesConfig unitConfig; // Inspector에서 할당

void Awake()
{
    InfinityValue.SetUnitNames(unitConfig.names);
}
```

---

## 주의 사항

- `SetUnitNames`는 **모든** `InfinityValue` 인스턴스에 전역으로 적용됩니다.
- `InfinityValue`가 생성되거나 문자열에서 파싱되기 **전에** 호출해야 합니다.
- 런타임 중 단위 이름을 변경하면 기존에 렌더링된 문자열과 저장된 문자열이 불일치할 수 있습니다. 반드시 시작 시에만 변경하세요.
- 목록에는 최소 2개의 항목 (인덱스 0과 1)이 있어야 합니다. 항목이 많을수록 표현 가능한 최대값이 커집니다.
