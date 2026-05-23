# Infinity Value

## v1.1.0 - 2026.05.23
- (fix) `Equals`/`GetHashCode` now compare by unit index semantics, not internal slot order
- (fix) `Normalize` handles negative values (borrow) from subtraction — underflow clamps to zero
- (fix) `AddOrUpdateUnit` no longer triggers recursive `Normalize`; operators normalize once at the end
- (perf) Static constructor replaces per-call `ValidateUnitNames` check
- (perf) String parsing now uses `Dictionary` lookup instead of `List.IndexOf`
- (perf) `GetUnit`/`SetUnit` marked `AggressiveInlining`
- (perf) `Normalize` uses insertion sort instead of bubble sort
- (feat) `double` constructor and implicit conversion added
- (feat) `operator *(InfinityValue, double)` and `operator /(InfinityValue, double)` added
- (feat) `TryParse` static method added
- (feat) `Zero` and `One` static properties added
- (feat) `ToString` now shows 2 decimal places from the next lower unit (e.g. "5.30B")
- (feat) `explicit operator double` conversion added
- (refactor) `explicit operator float` delegates to `double` conversion

## v1.0.1 - 2026.01.04
- (fix) Improvements to be less GC-riding

## v1.0.0 - 2024.09.17
- 🎉 Release!
