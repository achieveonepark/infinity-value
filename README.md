# Infinity Value

Unity idle and incremental games often need values far beyond `long`. Infinity Value stores large numbers as compact unit segments and supports arithmetic, comparison, parsing, formatting, and optional JSON serialization.

## Install

Install from Unity Package Manager with a Git URL:

```text
https://github.com/achieveonepark/infinity-value.git
```

Or add it to `Packages/manifest.json`:

```json
{
  "dependencies": {
    "com.achieve.infinity-value": "https://github.com/achieveonepark/infinity-value.git"
  }
}
```

## Quick Start

```csharp
using Achieve.InfinityValue;

var goldUnits = new InfinityValueUnitNames(new[]
{
    "", "K", "M", "B", "T", "Qa", "Qi"
});

InfinityValue gold = new InfinityValue(5_300_000_000L, goldUnits);
InfinityValue reward = new InfinityValue("12K", goldUnits);

gold += reward;

Debug.Log(gold.ToString()); // 5.30B
```

## Features

- Struct-based large number type for Unity runtime code.
- Stores up to 8 `(unitIndex, value)` pairs.
- Supports `+`, `-`, `*`, `/`, comparison operators, and primitive conversions.
- Supports per-content unit names through `InfinityValueUnitNames`.
- Supports safe parsing through `TryParse`.
- Includes optional Newtonsoft.Json converter when Unity's Newtonsoft package is installed.
- Includes Unity Package Manager samples in `Samples~`.

## Constructors

```csharp
new InfinityValue(long number)
new InfinityValue(long number, InfinityValueUnitNames unitNames)
new InfinityValue(double number)
new InfinityValue(double number, InfinityValueUnitNames unitNames)
new InfinityValue(float number)
new InfinityValue(float number, InfinityValueUnitNames unitNames)
new InfinityValue(BigInteger number)
new InfinityValue(BigInteger number, InfinityValueUnitNames unitNames)
new InfinityValue(string input)
new InfinityValue(string input, InfinityValueUnitNames unitNames)
```

## Parsing

```csharp
if (!InfinityValue.TryParse("5.30B", goldUnits, out var parsed))
    parsed = InfinityValue.Zero.WithUnitNames(goldUnits);
```

## Documentation

The `docs` folder is built as VitePress and is also GitBook-friendly through `README.md` and `SUMMARY.md`.
