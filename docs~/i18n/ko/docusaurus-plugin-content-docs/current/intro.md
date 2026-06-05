---
id: intro
title: 소개
sidebar_position: 1
---

# Infinity Value

Infinity Value는 `long`의 범위를 훨씬 초과하는 숫자가 필요한 방치형·증분형 Unity 게임 패키지입니다.

값은 압축된 `(unitIndex, value)` 쌍으로 저장되어, 일반적인 산술·비교 연산에서 관리 힙 할당이 발생하지 않습니다. 표시와 파싱에는 내장 `A, B, C ... CZ` 단위 이름 또는 콘텐츠 타입별 커스텀 `InfinityValueUnitNames` 인스턴스를 사용할 수 있습니다.

```csharp
using Achieve.InfinityValue;

var goldUnits = new InfinityValueUnitNames(new[]
{
    "", "K", "M", "B", "T", "Qa", "Qi"
});

InfinityValue gold = new InfinityValue(5_300_000_000L, goldUnits);
InfinityValue reward = new InfinityValue("12K", goldUnits);

gold += reward;

Debug.Log(gold.ToString()); // 5.30B
```

## 특징

- Unity 런타임 코드를 위한 구조체 기반 대형 숫자 타입
- 산술: `+`, `-`, `*`, `/`
- 비교: `==`, `!=`, `<`, `>`, `<=`, `>=`
- `InfinityValueUnitNames`를 통한 인스턴스별 단위 이름 지원
- `TryParse`를 이용한 문자열 파싱
- Unity Newtonsoft 패키지 설치 시 선택적 JSON 컨버터 제공
- `Samples~`에 임포트 가능한 Unity 샘플 포함

## 다음 단계

- [시작하기](getting-started.md)
- [API 레퍼런스](api/index.md)
- [단위 이름](api/unit-names.md)
- [샘플](samples.md)
