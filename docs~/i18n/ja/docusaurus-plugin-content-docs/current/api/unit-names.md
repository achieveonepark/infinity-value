---
id: unit-names
title: 単位名
sidebar_position: 2
---

# 単位名

`InfinityValueUnitNames` はインスタンスオブジェクトです。旧来のグローバル設定方式を置き換え、各コンテンツシステムが独自のサフィックステーブルを持てるようにします。

## デフォルト単位

デフォルトテーブルは空のベース単位から始まり、`A`、`B`、`C` から `CZ` まで続きます。

```csharp
InfinityValue value = new InfinityValue(1_500_000L);
Debug.Log(value); // 1.50B
```

## カスタム単位

インデックス 0 はベース数値ティアを表すため、空文字列でなければなりません。

```csharp
var units = new InfinityValueUnitNames(new[]
{
    "", "K", "M", "B", "T", "Qa", "Qi", "Sx"
});

InfinityValue value = new InfinityValue(5_300_000_000L, units);
Debug.Log(value); // 5.30B
```

## 複数のコンテンツテーブル

コンテンツごとにグローバル状態を変更せずに異なる名前を保持できます。

```csharp
var currencyUnits = new InfinityValueUnitNames(new[] { "", "K", "M", "B" });
var damageUnits = new InfinityValueUnitNames(new[] { "", "a", "b", "c" });

InfinityValue gold = new InfinityValue("12K", currencyUnits);
InfinityValue damage = new InfinityValue("12a", damageUnits);
```

## パース

保存した文字列は必ず、それを作成した単位テーブルでパースしてください。

```csharp
if (InfinityValue.TryParse("42M", currencyUnits, out var value))
    Debug.Log(value);
```

## バリデーション

`InfinityValueUnitNames` は次の場合に例外をスローします:

- 名前リストが null の場合
- リストが空の場合
- インデックス 0 が空文字列でない場合
- 単位名が null の場合
- 単位名が重複している場合
