# Constructors

All numeric types support **implicit conversion** — direct assignment without `new`.

## InfinityValue(long)

```csharp
InfinityValue v = 1_500_000L;
Debug.Log(v); // "1.50A"
```

## InfinityValue(double)

Useful for values beyond `long.MaxValue` or formula results. Fractional parts are truncated.

```csharp
InfinityValue v = 1.5e12;
Debug.Log(v); // "1.50C"
```

> `NaN` and `Infinity` produce `Zero`.

## InfinityValue(float)

```csharp
InfinityValue v = 3.0f;
```

## InfinityValue(string)

Parses `<number><unit>` pairs separated by spaces.

```csharp
InfinityValue v = "300F 200C";
```

Invalid unit names return `Zero` silently. Use [`TryParse`](/en/api/methods#tryparse) for error handling.

## InfinityValue(BigInteger)

```csharp
using System.Numerics;
InfinityValue v = new BigInteger(30_000_000_000_000L);
Debug.Log(v); // "30.00D"
```

## Implicit Conversion Summary

```csharp
InfinityValue a = 1000;              // int
InfinityValue b = 1_000_000L;        // long
InfinityValue c = 3.0f;              // float
InfinityValue d = 1.5e9;             // double
InfinityValue e = "500B 300A";       // string
InfinityValue f = new BigInteger(…); // BigInteger
```
