# Operators

All operators are allocation-free.

## Arithmetic

```csharp
var sum  = a + b;
var sum2 = a + 1_000L;      // long overload

var diff = a - b;            // clamps to 0 if b > a
var diff2 = a - 500L;

var mul  = a * 3L;           // integer multiplier
var pct  = a * 1.5;          // double multiplier — 50% increase
var half = a * 0.5;

var div  = a / 4L;
var div2 = a / 2.0;          // double divisor
```

> Division by zero throws `DivideByZeroException`.

## Comparison

```csharp
bool gt  = a > b;
bool gte = a >= 1_000_000L;  // with long
bool eq  = a == InfinityValue.Zero;
```

Works between two `InfinityValue` instances and between `InfinityValue` and `long`.

## Example: Idle Game Loop

```csharp
InfinityValue gold = 5_000_000L;   // "5.00A"

gold += 300_000L;                   // "5.30A"
gold  = gold * 1.1;                 // +10% bonus → "5.83A"

InfinityValue cost = "2A";
if (gold >= cost)
    gold -= cost;                   // safe purchase

InfinityValue half = gold / 2L;    // split
```
