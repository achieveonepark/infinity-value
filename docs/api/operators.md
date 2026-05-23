# Operators

All operators are allocation-free (no GC pressure).

---

## Arithmetic Operators

### Addition `+`

```csharp
InfinityValue result = a + b;
InfinityValue result = a + 1_000L;  // long overload
```

### Subtraction `-`

```csharp
InfinityValue result = a - b;
InfinityValue result = a - 500L;    // long overload
```

> Subtraction **clamps to zero** — the result is never negative. If `b > a`, the result is `Zero`.

```csharp
InfinityValue a = 100L;
InfinityValue b = 500L;
Debug.Log(a - b); // "0"
```

### Multiplication `*`

```csharp
// Integer multiplier
InfinityValue result = a * 3L;
InfinityValue result = a * 3;       // int overload

// Fractional multiplier (e.g. buffs, percentage increases)
InfinityValue result = a * 1.5;     // double overload
InfinityValue result = a * 0.5;     // halve the value
```

### Division `/`

```csharp
// Integer divisor
InfinityValue result = a / 4L;
InfinityValue result = a / 4;       // int overload

// Fractional divisor
InfinityValue result = a / 2.0;     // double overload
```

> Division by zero throws `DivideByZeroException`.

---

## Comparison Operators

All comparison operators work between two `InfinityValue` instances and between `InfinityValue` and `long`. These are **allocation-free**.

```csharp
bool gt  = a > b;
bool lt  = a < b;
bool gte = a >= b;
bool lte = a <= b;
bool eq  = a == b;
bool neq = a != b;

// With long
bool hasEnough = playerGold >= 1_000_000L;
bool isEmpty   = score == 0L;
```

---

## Operator Examples

```csharp
InfinityValue gold = 5_000_000L;  // "5.00A"

// Earn gold
gold += 300_000L;                  // "5.30A"

// Apply 10% bonus
gold = gold * 1.1;                 // "5.83A"

// Split equally
InfinityValue half = gold / 2L;    // "2.91A"

// Check threshold
if (gold >= 10_000_000L)
    UnlockPrestige();

// Safe purchase
InfinityValue cost = "2A";
if (gold >= cost)
    gold -= cost;
```
