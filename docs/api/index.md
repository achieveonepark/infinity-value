# API 개요

`InfinityValue`는 C# `struct`로 구현된 값 타입입니다. 힙 할당 없이 방치형 게임의 대형 숫자를 처리합니다.

---

## 정적 멤버

| 멤버 | 종류 | 설명 |
|------|------|------|
| `Zero` | 프로퍼티 | 값 0을 나타내는 인스턴스 |
| `One` | 프로퍼티 | 값 1을 나타내는 인스턴스 |
| `TryParse(string, out InfinityValue)` | 메서드 | 문자열을 파싱하여 변환 |
| `SetUnitNames(List<string>)` | 메서드 | 전역 단위 이름 배열 설정 |

---

## 인스턴스 멤버

| 멤버 | 종류 | 설명 |
|------|------|------|
| `IsEmpty` | 프로퍼티 | 값이 0이거나 초기화되지 않은 경우 `true` |
| `ToString()` | 메서드 | `"5.30B"` 형태의 문자열 반환 |
| `CompareTo(InfinityValue)` | 메서드 | `IComparable<T>` 구현 |
| `Equals(InfinityValue)` | 메서드 | `IEquatable<T>` 구현 |
| `GetHashCode()` | 메서드 | Dictionary/HashSet 안전 해시 |

---

## 생성자

| 생성자 | 설명 |
|--------|------|
| `InfinityValue(int)` | int에서 생성 |
| `InfinityValue(long)` | long에서 생성 |
| `InfinityValue(float)` | float에서 생성 |
| `InfinityValue(double)` | double에서 생성 |
| `InfinityValue(string)` | 문자열(`"5.30B"`)에서 생성 |
| `InfinityValue(BigInteger)` | BigInteger에서 생성 |

자세한 내용은 [생성자](/api/constructors) 페이지를 참고하세요.

---

## 연산자

| 연산자 | 피연산자 타입 | 설명 |
|--------|--------------|------|
| `+`, `-` | `InfinityValue` | 덧셈 / 뺄셈 (뺄셈은 0 클램프) |
| `*`, `/` | `long`, `double` | 곱셈 / 나눗셈 |
| `==`, `!=` | `InfinityValue` | 동등 비교 |
| `<`, `>`, `<=`, `>=` | `InfinityValue` | 크기 비교 |

자세한 내용은 [연산자](/api/operators) 페이지를 참고하세요.

---

## 명시적 변환

`(long)`, `(float)`, `(double)` 캐스트를 통해 기본 타입으로 변환할 수 있습니다. 큰 값은 정밀도 손실이 발생할 수 있습니다.

자세한 내용은 [타입 변환](/api/conversion) 페이지를 참고하세요.
