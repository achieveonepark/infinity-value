---
id: unit-names
title: 단위 이름
sidebar_position: 2
---

# 단위 이름

`InfinityValueUnitNames`는 인스턴스 객체입니다. 기존 전역 설정 방식을 대체하며, 각 콘텐츠 시스템이 자체 접미사 테이블을 소유할 수 있습니다.

## 기본 단위

기본 테이블은 빈 기본 단위로 시작하고 `A`, `B`, `C`에서 `CZ`까지 이어집니다.

```csharp
InfinityValue value = new InfinityValue(1_500_000L);
Debug.Log(value); // 1.50B
```

## 커스텀 단위

인덱스 0은 기본 숫자 단위를 나타내므로 반드시 빈 문자열이어야 합니다.

```csharp
var units = new InfinityValueUnitNames(new[]
{
    "", "K", "M", "B", "T", "Qa", "Qi", "Sx"
});

InfinityValue value = new InfinityValue(5_300_000_000L, units);
Debug.Log(value); // 5.30B
```

## 다중 콘텐츠 테이블

콘텐츠마다 전역 상태를 건드리지 않고 서로 다른 이름을 유지할 수 있습니다.

```csharp
var currencyUnits = new InfinityValueUnitNames(new[] { "", "K", "M", "B" });
var damageUnits = new InfinityValueUnitNames(new[] { "", "a", "b", "c" });

InfinityValue gold = new InfinityValue("12K", currencyUnits);
InfinityValue damage = new InfinityValue("12a", damageUnits);
```

## 파싱

저장된 문자열은 항상 생성 시 사용한 단위 테이블로 파싱해야 합니다.

```csharp
if (InfinityValue.TryParse("42M", currencyUnits, out var value))
    Debug.Log(value);
```

## 유효성 검사

`InfinityValueUnitNames`는 다음 경우 예외를 발생시킵니다:

- 이름 목록이 null인 경우
- 목록이 비어있는 경우
- 인덱스 0이 빈 문자열이 아닌 경우
- 단위 이름이 null인 경우
- 단위 이름이 중복된 경우
