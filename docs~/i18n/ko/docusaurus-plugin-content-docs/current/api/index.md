---
id: index
title: API 레퍼런스
sidebar_position: 1
---

# API 레퍼런스

## 주요 타입

| 타입 | 설명 |
| --- | --- |
| `InfinityValue` | 대형 숫자 값 타입 |
| `InfinityValueUnitNames` | 파싱 및 표시를 위한 인스턴스 단위 접미사 테이블 |
| `InfinityValueConverter` | 선택적 Newtonsoft.Json 컨버터 |

## 생성자

```csharp
new InfinityValue(long number)
new InfinityValue(long number, InfinityValueUnitNames unitNames)

new InfinityValue(double number)
new InfinityValue(double number, InfinityValueUnitNames unitNames)

new InfinityValue(float number)
new InfinityValue(float number, InfinityValueUnitNames unitNames)

new InfinityValue(BigInteger number)
new InfinityValue(BigInteger number, InfinityValueUnitNames unitNames)

new InfinityValue(string input)
new InfinityValue(string input, InfinityValueUnitNames unitNames)
```

## 정적 값

```csharp
InfinityValue.Zero
InfinityValue.One
```

커스텀 단위 컨텍스트가 필요한 경우 `WithUnitNames`를 사용합니다:

```csharp
InfinityValue zeroGold = InfinityValue.Zero.WithUnitNames(goldUnits);
```

## 파싱

```csharp
InfinityValue.TryParse(string input, out InfinityValue result)
InfinityValue.TryParse(string input, InfinityValueUnitNames unitNames, out InfinityValue result)
```

## 인스턴스 멤버

```csharp
bool IsEmpty { get; }
InfinityValueUnitNames UnitNames { get; }
InfinityValue WithUnitNames(InfinityValueUnitNames unitNames)
string ToString()
int CompareTo(InfinityValue other)
bool Equals(InfinityValue other)
int GetHashCode()
```

## 연산자

```csharp
a + b
a - b
a * 10L
a * 1.5
a / 2L
a / 2.0

a == b
a != b
a < b
a > b
a <= b
a >= b
```

## 변환

```csharp
InfinityValue value = 1000;
InfinityValue parsed = "500A";

long asLong = (long)value;
double asDouble = (double)value;
float asFloat = (float)value;
```
