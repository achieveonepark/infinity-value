# Infinity Value

## Unreleased

## v1.2.2 - 2026.07.11

- (feat) `Pow`, `Sqrt`, and `Log10` added for cost-curve math without overflowing `double`.
- (feat) `AffordableCount` added to solve the geometric-series "buy max" formula for a price curve `baseCost * growthRate^n`.
- (feat) `SerializableInfinityValue` added for exposing `InfinityValue` fields in the Unity Inspector, with a custom `PropertyDrawer` that accepts compact notation (e.g. `5.3B`) and a lossless `ToExactString`/`TryParseExact` round-trip.
- (feat) `InfinityValueCounter` added for async count-up/down UI animations. Concurrent calls from multiple call sites merge into the currently running animation instead of racing or restarting.
- (chore) Added `Editor/Achieve.InfinityValue.Editor.asmdef` for editor-only inspector code.

## v1.2.0 - 2026.06.14

- Added `InfinityValueUnitNames` for per-content unit name instances.
- Added constructor and `TryParse` overloads that accept `InfinityValueUnitNames`.
- Fixed parsing for compact decimal strings such as `5.30B`.
- Rebuilt documentation as GitBook-friendly Markdown plus VitePress deployment.
- Added Unity Package Manager samples under `Samples~`.

## v1.1.0 - 2026.05.23

- (fix) `Equals`/`GetHashCode` now compare by unit index semantics, not internal slot order.
- (fix) `Normalize` handles negative values from subtraction and clamps underflow to zero.
- (fix) `AddOrUpdateUnit` no longer triggers recursive `Normalize`; operators normalize once at the end.
- (perf) Static constructor replaces per-call `ValidateUnitNames` check.
- (perf) String parsing uses dictionary lookup instead of `List.IndexOf`.
- (perf) `GetUnit`/`SetUnit` marked `AggressiveInlining`.
- (perf) `Normalize` uses insertion sort instead of bubble sort.
- (feat) `double` constructor and implicit conversion added.
- (feat) `operator *(InfinityValue, double)` and `operator /(InfinityValue, double)` added.
- (feat) `TryParse` static method added.
- (feat) `Zero` and `One` static properties added.
- (feat) `ToString` now shows 2 decimal places from the next lower unit.
- (feat) `explicit operator double` conversion added.
- (refactor) `explicit operator float` delegates to `double` conversion.

## v1.0.1 - 2026.01.04

- Reduced GC pressure through internal structural optimizations.

## v1.0.0 - 2024.09.17

- Initial release.
