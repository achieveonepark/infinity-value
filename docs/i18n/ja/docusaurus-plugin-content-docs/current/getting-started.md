---
id: getting-started
title: 使い始める
sidebar_position: 2
---

# 使い始める

## インストール

Unity Package Manager からパッケージを追加します:

```text
https://github.com/achieveonepark/infinity-value.git
```

または `Packages/manifest.json` に直接追加します:

```json
{
  "dependencies": {
    "com.achieve.infinity-value": "https://github.com/achieveonepark/infinity-value.git"
  }
}
```

## 値の作成

```csharp
using Achieve.InfinityValue;

InfinityValue a = 1000;
InfinityValue b = new InfinityValue(1_500_000L);
InfinityValue c = new InfinityValue(1.5e12);
InfinityValue d = new InfinityValue("300B 200A");
```

## コンテンツごとの単位名を使う

システムごとに異なるサフィックスが必要な場合は `InfinityValueUnitNames` を使用します。

```csharp
var currencyUnits = new InfinityValueUnitNames(new[]
{
    "", "K", "M", "B", "T", "Qa", "Qi"
});

var damageUnits = new InfinityValueUnitNames(new[]
{
    "", "a", "b", "c", "d", "e", "f"
});

InfinityValue gold = new InfinityValue("12K", currencyUnits);
InfinityValue damage = new InfinityValue("12a", damageUnits);
```

各 `InfinityValue` は渡された単位名インスタンスを内部に保持します。算術演算では左辺の単位名が維持されます:

```csharp
InfinityValue totalGold = gold + new InfinityValue(5000, currencyUnits);
Debug.Log(totalGold); // 17.00K
```

## 安全なパース

```csharp
if (!InfinityValue.TryParse("5B", currencyUnits, out var parsed))
    parsed = InfinityValue.Zero.WithUnitNames(currencyUnits);
```

## セーブ＆ロード

表示文字列として保存し、ロード時に同じ単位名セットを使うのが最もシンプルな永続化方法です:

```csharp
PlayerPrefs.SetString("gold", gold.ToString());

string raw = PlayerPrefs.GetString("gold", "0");
InfinityValue.TryParse(raw, currencyUnits, out gold);
```
