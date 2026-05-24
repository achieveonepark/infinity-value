# 생성자

`InfinityValue`는 여러 숫자 타입과 문자열에서 생성할 수 있습니다. 모든 타입은 **암묵적 변환**도 지원하므로 `new` 키워드 없이 직접 대입이 가능합니다.

---

## InfinityValue(long)

```csharp
public InfinityValue(long number)
```

`long` 값에서 생성합니다. 0 이하의 값은 `Zero`를 반환합니다.

```csharp
InfinityValue v = new InfinityValue(1_500_000L);
// 암묵적 변환도 동일:
InfinityValue v = 1_500_000L;

Debug.Log(v); // "1.50A"
```

---

## InfinityValue(double)

```csharp
public InfinityValue(double number)
```

`double` 값에서 생성합니다. `long.MaxValue`를 초과하는 큰 값이나 부동소수점 수식 결과에 적합합니다. 소수 부분은 버림 처리됩니다.

```csharp
InfinityValue v = new InfinityValue(1.5e12);
// 암묵적 변환도 동일:
InfinityValue v = 1.5e12;

Debug.Log(v); // "1.50C"
```

> `NaN` 또는 `Infinity` 입력은 `Zero`를 반환합니다.

---

## InfinityValue(float)

```csharp
public InfinityValue(float number)
```

`double` 생성자에 위임합니다. 소수 부분은 버림 처리됩니다.

```csharp
InfinityValue v = 3.0f;
Debug.Log(v); // "3"
```

---

## InfinityValue(int)

```csharp
public InfinityValue(int number)
```

`int` 값에서 생성합니다. 내부적으로 `long`으로 변환됩니다.

```csharp
InfinityValue v = 1000;
Debug.Log(v); // "1.00A"
```

---

## InfinityValue(string)

```csharp
public InfinityValue(string input)
```

포맷된 문자열을 파싱합니다. 허용 형식은 `<숫자><단위>` 쌍을 공백으로 구분한 것입니다 (예: `"5B"`, `"300F 200C"`).

```csharp
InfinityValue v = new InfinityValue("300F 200C");
// 암묵적 변환도 동일:
InfinityValue v = "300F 200C";
```

단위 이름은 현재 설정된 단위 목록(기본: `A`–`CZ`)과 매칭됩니다.

잘못된 입력은 조용히 `Zero`를 반환합니다. 오류 감지가 필요하면 [`TryParse`](/api/methods#tryparse)를 사용하세요.

---

## InfinityValue(BigInteger)

`System.Numerics.BigInteger`에서의 암묵적 변환입니다. 내부 8슬롯 한계(≈ 10^316)를 초과하는 값은 상위 자릿수가 손실됩니다.

```csharp
using System.Numerics;

InfinityValue v = new BigInteger(30_000_000_000_000L);
Debug.Log(v); // "30.00D"
```

---

## 암묵적 변환 요약

모든 지원 타입은 직접 대입이 가능합니다.

```csharp
InfinityValue a = 1000;              // int
InfinityValue b = 1_000_000L;        // long
InfinityValue c = 3.0f;              // float
InfinityValue d = 1.5e9;             // double
InfinityValue e = "500B 300A";       // string
InfinityValue f = new BigInteger(…); // BigInteger
```
