# Changelog

## v1.1.0 — 2026-05-23

### Bug Fixes
- `Equals`/`GetHashCode`: now compare by unit index semantics, not internal slot order
- `Normalize`: handles borrow correctly for subtraction underflow (clamps to zero)
- `AddOrUpdateUnit`: no longer triggers recursive `Normalize`

### Performance
- Static constructor replaces per-call `ValidateUnitNames()` check
- String parsing uses `Dictionary<string, int>` (O(1)) instead of `List.IndexOf` (O(n))
- `Normalize` uses insertion sort instead of bubble sort
- `GetUnit`/`SetUnit` marked `AggressiveInlining`

### New Features
- `double` constructor, implicit conversion, `* double`, `/ double` operators
- `TryParse(string, out InfinityValue)` static method
- `Zero` and `One` static properties
- `ToString()` shows 2 decimal places (e.g. `"5.30B"`)
- `explicit operator double`

---

## v1.0.1 — 2026-01-04

- Reduced GC pressure through internal structural optimisations.

---

## v1.0.0 — 2024-09-17

- Initial release.
