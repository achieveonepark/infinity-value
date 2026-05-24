# 타입 변환

`InfinityValue`는 **암묵적 변환** (손실 없는 방향)과 **명시적 변환** (손실 가능성 있는 방향) 모두를 지원합니다.

---

## 암묵적 변환 (→ InfinityValue)

손실이 없으므로 캐스트 없이 대입할 수 있습니다.

| 원본 타입 | 예제 |
|-----------|------|
| `int` | `InfinityValue v = 1000;` |
| `long` | `InfinityValue v = 1_000_000L;` |
| `float` | `InfinityValue v = 3.0f;` |
| `double` | `InfinityValue v = 1.5e9;` |
| `string` | `InfinityValue v = "500B";` |
| `BigInteger` | `InfinityValue v = new BigInteger(…);` |

---

## 명시적 변환 (InfinityValue →)

캐스트가 필요하며, 큰 값에서는 정밀도 손실이 발생할 수 있습니다.

### (long)

```csharp
public static explicit operator long(InfinityValue value)
```

값을 `long`으로 근사 변환합니다. 하위 두 유효 단위만 사용합니다. `long.MaxValue`(~9.2 × 10^18)를 초과하는 값은 오버플로우됩니다.

```csharp
InfinityValue v = "5B 300A";  // 5,300,000,000
long l = (long)v;
Debug.Log(l); // 5300000000
```

```csharp
InfinityValue large = "100C"; // long 범위 초과 — 결과가 근사값이거나 오버플로우
long l = (long)large;
```

### (double)

```csharp
public static explicit operator double(InfinityValue value)
```

모든 단위를 규모별로 합산하여 `double`로 변환합니다. 유효 자릿수가 많은 값은 정밀도가 손실됩니다.

```csharp
InfinityValue v = "5B 300A";
double d = (double)v;
Debug.Log(d); // 5300000000

// 진행도 계산에 유용
float progress = (float)((double)currentXP / (double)requiredXP);
```

### (float)

```csharp
public static explicit operator float(InfinityValue value)
```

`(double)` 변환 후 `float`으로 좁힙니다. 큰 값에서는 정밀도 손실이 큽니다.

```csharp
InfinityValue v = "5B";
float f = (float)v;
Debug.Log(f); // 5E+09 (근사값)
```

---

## 정밀도 참고 표

| 변환 | 안전한 범위 | 비고 |
|---|---|---|
| `(long)` | ~`9A` (`9.2 × 10^18`) 이하 | 하위 2단위만 사용 |
| `(double)` | ~`1AW` (`10^52`) 이하 | IEEE 754 double — 유효 자릿수 15–17 |
| `(float)` | ~`1H` (`10^24`) 이하 | IEEE 754 float — 유효 자릿수 약 7 |

이 범위를 초과하는 값은 숫자 변환 대신 `ToString()`과 문자열 기반 저장을 사용하세요.
