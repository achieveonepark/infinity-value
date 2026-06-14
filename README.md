# Infinity Value

[![openupm](https://img.shields.io/npm/v/com.achieve.infinity-value?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.achieve.infinity-value/)
[![openupm](https://img.shields.io/badge/dynamic/json?color=brightgreen&label=downloads&query=%24.downloads&suffix=%2Fmonth&url=https%3A%2F%2Fpackage.openupm.com%2Fdownloads%2Fpoint%2Flast-month%2Fcom.achieve.infinity-value)](https://openupm.com/packages/com.achieve.infinity-value/)

Unity idle and incremental games often need values far beyond `long`. Infinity Value stores large numbers as compact unit segments and supports arithmetic, comparison, parsing, formatting, and optional JSON serialization.

## Install

### OpenUPM

After the package is accepted and published on OpenUPM, install it with the OpenUPM CLI:

```bash
openupm add com.achieve.infinity-value
```

Or add the OpenUPM scoped registry manually in `Packages/manifest.json`:

```json
{
  "scopedRegistries": [
    {
      "name": "OpenUPM",
      "url": "https://package.openupm.com",
      "scopes": [
        "com.achieve"
      ]
    }
  ],
  "dependencies": {
    "com.achieve.infinity-value": "1.2.0"
  }
}
```

### Git URL

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
