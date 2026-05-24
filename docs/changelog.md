# 변경 이력

## v1.1.0 — 2026-05-23

### 버그 수정
- **`Equals` / `GetHashCode`**: 이제 내부 슬롯 순서가 아닌 단위 인덱스 의미론적 값으로 비교합니다. 이전에는 동일한 값이라도 슬롯 순서가 달라 불일치로 판정될 수 있었습니다.
- **뺄셈 언더플로우**: `Normalize`가 뺄셈 결과가 음수가 될 경우 빌림(borrow)을 올바르게 전파합니다. 이제 잘못된 음수 내부 상태 대신 0으로 클램프됩니다.
- **재귀 `Normalize`**: `AddOrUpdateUnit`이 더 이상 재귀적으로 `Normalize`를 호출하지 않습니다. 각 산술 연산자 마지막에 `Normalize`가 한 번만 실행됩니다.

### 성능 개선
- 정적 생성자가 호출 시마다 수행되던 `ValidateUnitNames()` 검사를 대체합니다. 단위 이름이 정확히 한 번만 초기화됩니다.
- 문자열 파싱이 `List.IndexOf`(O(n)) 대신 `Dictionary<string, int>` 조회(O(1))를 사용합니다.
- `Normalize`가 버블 정렬 대신 **삽입 정렬**을 사용합니다. 작고 거의 정렬된 배열에서 더 빠릅니다.
- `GetUnit` / `SetUnit`에 `AggressiveInlining` 적용.

### 신규 기능
- `double` 생성자, 암묵적 변환, `operator *(double)` / `operator /(double)` 추가 — `value * 1.5` 같은 소수 배수 연산 가능.
- `TryParse(string, out InfinityValue)` — 명시적 실패 처리를 위한 string 생성자의 안전한 대안.
- `Zero`와 `One` 정적 프로퍼티 추가.
- `ToString()`이 다음 하위 단위에서 소수 **2자리**를 표시합니다 (예: `"5B"` 대신 `"5.30B"`).
- `explicit operator double` 변환 추가.
- `explicit operator float`이 이제 `double` 변환 경로를 통해 처리됩니다.

---

## v1.0.1 — 2026-01-04

- 내부 구조 최적화를 통해 GC 압력을 감소시켰습니다.

---

## v1.0.0 — 2024-09-17

- 최초 릴리즈.
