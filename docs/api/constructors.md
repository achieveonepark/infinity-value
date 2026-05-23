# Constructors

`InfinityValue` can be constructed from several numeric and string types. All numeric types also support **implicit conversion**, so you can assign them directly without `new`.

---

## InfinityValue(long)

```csharp
public InfinityValue(long number)
```

Creates a value from a `long`. Values ≤ 0 produce `Zero`.

```csharp
InfinityValue v = new InfinityValue(1_500_000L);
// or implicitly:
InfinityValue v = 1_500_000L;

Debug.Log(v); // "1.50A"
```

---

## InfinityValue(double)

```csharp
public InfinityValue(double number)
```

Creates a value from a `double`. Useful for very large values that exceed `long.MaxValue` or for results of floating-point formulas. Fractional parts are truncated.

```csharp
InfinityValue v = new InfinityValue(1.5e12);
// or implicitly:
InfinityValue v = 1.5e12;

Debug.Log(v); // "1.50C"
```

> `NaN` and `Infinity` inputs produce `Zero`.

---

## InfinityValue(float)

```csharp
public InfinityValue(float number)
```

Delegates to the `double` constructor. Fractional parts are truncated.

```csharp
InfinityValue v = 3.0f;
Debug.Log(v); // "3"
```

---

## InfinityValue(string)

```csharp
public InfinityValue(string input)
```

Parses a formatted string. The expected format is one or more `<number><unit>` pairs separated by spaces (e.g. `"5B"`, `"300F 200C"`).

```csharp
InfinityValue v = new InfinityValue("300F 200C");
// or implicitly:
InfinityValue v = "300F 200C";
```

Unit names are matched against the current unit name list (default: `A`–`CZ`).

Invalid input returns `Zero` silently. Use [`TryParse`](methods.md#tryparse) when you need error detection.

---

## InfinityValue(BigInteger)

Implicit conversion from `System.Numerics.BigInteger`. Values above the 8-unit limit (≈ 10^316) lose the highest digits.

```csharp
using System.Numerics;

InfinityValue v = new BigInteger(30_000_000_000_000L);
Debug.Log(v); // "30.00D"
```

---

## Implicit Conversion Summary

All of the above types support direct assignment:

```csharp
InfinityValue a = 1000;              // int
InfinityValue b = 1_000_000L;        // long
InfinityValue c = 3.0f;              // float
InfinityValue d = 1.5e9;             // double
InfinityValue e = "500B 300A";       // string
InfinityValue f = new BigInteger(…); // BigInteger
```
