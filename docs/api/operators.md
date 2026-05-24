# 연산자

모든 연산자는 힙 할당이 없습니다 (GC 부담 없음).

---

## 산술 연산자

### 덧셈 `+`

```csharp
InfinityValue result = a + b;
InfinityValue result = a + 1_000L;  // long 오버로드
```

### 뺄셈 `-`

```csharp
InfinityValue result = a - b;
InfinityValue result = a - 500L;    // long 오버로드
```

> 뺄셈은 **0으로 클램프**됩니다. `b > a` 이면 결과는 `Zero`입니다.

```csharp
InfinityValue a = 100L;
InfinityValue b = 500L;
Debug.Log(a - b); // "0"
```

### 곱셈 `*`

```csharp
// 정수 배수
InfinityValue result = a * 3L;
InfinityValue result = a * 3;       // int 오버로드

// 소수 배수 (버프, 퍼센트 증가 등)
InfinityValue result = a * 1.5;     // double 오버로드
InfinityValue result = a * 0.5;     // 절반으로 줄이기
```

### 나눗셈 `/`

```csharp
// 정수 제수
InfinityValue result = a / 4L;
InfinityValue result = a / 4;       // int 오버로드

// 소수 제수
InfinityValue result = a / 2.0;     // double 오버로드
```

> 0으로 나누면 `DivideByZeroException`이 발생합니다.

---

## 비교 연산자

두 `InfinityValue` 인스턴스 간, 또는 `InfinityValue`와 `long` 간의 비교를 모두 지원합니다. 모두 힙 할당이 없습니다.

```csharp
bool gt  = a > b;
bool lt  = a < b;
bool gte = a >= b;
bool lte = a <= b;
bool eq  = a == b;
bool neq = a != b;

// long과의 비교
bool hasEnough = playerGold >= 1_000_000L;
bool isEmpty   = score == 0L;
```

---

## 사용 예제

```csharp
InfinityValue gold = 5_000_000L;  // "5.00A"

// 골드 획득
gold += 300_000L;                  // "5.30A"

// 10% 보너스 적용
gold = gold * 1.1;                 // "5.83A"

// 균등 분배
InfinityValue half = gold / 2L;    // "2.91A"

// 임계값 확인
if (gold >= 10_000_000L)
    UnlockPrestige();

// 안전한 구매 처리
InfinityValue cost = "2A";
if (gold >= cost)
    gold -= cost;
```
