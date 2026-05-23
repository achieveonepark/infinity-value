# Changelog

## v1.1.0 — 2026-05-23

### Bug Fixes
- **`Equals` / `GetHashCode`**: now compare by unit index semantics rather than internal slot order. Previously, two identical values with units stored in different slots could be incorrectly treated as unequal.
- **Subtraction underflow**: `Normalize` now propagates borrow correctly when subtraction would yield a negative result. The value clamps to zero instead of storing invalid negative internals.
- **Recursive `Normalize`**: `AddOrUpdateUnit` no longer triggers a recursive `Normalize` call. Each arithmetic operator now performs a single `Normalize` pass at the end.

### Performance
- Static constructor replaces the per-call `ValidateUnitNames()` check — unit names are initialised exactly once.
- String parsing now uses a `Dictionary<string, int>` lookup (O(1)) instead of `List.IndexOf` (O(n)).
- `Normalize` now uses **insertion sort** instead of bubble sort — better for small, nearly-sorted arrays.
- `GetUnit` / `SetUnit` are marked `AggressiveInlining`.

### New Features
- `double` constructor, implicit conversion, and `operator *(double)` / `operator /(double)` — enables fractional multipliers like `value * 1.5`.
- `TryParse(string, out InfinityValue)` — safe alternative to the string constructor with explicit failure handling.
- `Zero` and `One` static properties.
- `ToString()` now shows **2 decimal places** from the next lower unit (e.g. `"5.30B"` instead of `"5B"`).
- `explicit operator double` conversion added.
- `explicit operator float` now delegates to the `double` conversion path.

---

## v1.0.1 — 2026-01-04

- Reduced GC pressure through internal structural optimisations.

---

## v1.0.0 — 2024-09-17

- Initial release.
