---
id: getting-started
title: 시작하기
sidebar_position: 2
---

# 시작하기

## 설치

Unity Package Manager에서 패키지를 추가합니다:

```text
https://github.com/achieveonepark/infinity-value.git
```

또는 `Packages/manifest.json`에 직접 추가합니다:

```json
{
  "dependencies": {
    "com.achieve.infinity-value": "https://github.com/achieveonepark/infinity-value.git"
  }
}
```

## 값 생성

```csharp
using Achieve.InfinityValue;

InfinityValue a = 1000;
InfinityValue b = new InfinityValue(1_500_000L);
InfinityValue c = new InfinityValue(1.5e12);
InfinityValue d = new InfinityValue("300B 200A");
```

## 콘텐츠별 단위 이름 사용

시스템마다 다른 접미사가 필요한 경우 `InfinityValueUnitNames`를 사용합니다.

```csharp
var currencyUnits = new InfinityValueUnitNames(new[]
{
    "", "K", "M", "B", "T", "Qa", "Qi"
});

var damageUnits = new InfinityValueUnitNames(new[]
{
    "", "a", "b", "c", "d", "e", "f"
});

InfinityValue gold = new InfinityValue("12K", currencyUnits);
InfinityValue damage = new InfinityValue("12a", damageUnits);
```

각 `InfinityValue`는 전달된 단위 이름 인스턴스를 내부에 유지합니다. 산술 연산은 좌항의 단위 이름을 따릅니다:

```csharp
InfinityValue totalGold = gold + new InfinityValue(5000, currencyUnits);
Debug.Log(totalGold); // 17.00K
```

## 안전한 파싱

```csharp
if (!InfinityValue.TryParse("5B", currencyUnits, out var parsed))
    parsed = InfinityValue.Zero.WithUnitNames(currencyUnits);
```

## 저장 및 불러오기

표시 문자열을 저장하고, 불러올 때 동일한 단위 이름 세트를 사용하는 방식이 가장 간단합니다:

```csharp
PlayerPrefs.SetString("gold", gold.ToString());

string raw = PlayerPrefs.GetString("gold", "0");
InfinityValue.TryParse(raw, currencyUnits, out gold);
```
