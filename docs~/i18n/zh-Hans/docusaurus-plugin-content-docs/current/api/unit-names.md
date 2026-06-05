---
id: unit-names
title: 单位名称
sidebar_position: 2
---

# 单位名称

`InfinityValueUnitNames` 是一个实例对象，替代了旧的全局配置方式，让每个内容系统都能拥有自己的后缀表。

## 默认单位

默认表以空基础单位开始，后续依次为 `A`、`B`、`C` 直到 `CZ`。

```csharp
InfinityValue value = new InfinityValue(1_500_000L);
Debug.Log(value); // 1.50B
```

## 自定义单位

索引 0 必须是空字符串，因为它代表基础数值层级。

```csharp
var units = new InfinityValueUnitNames(new[]
{
    "", "K", "M", "B", "T", "Qa", "Qi", "Sx"
});

InfinityValue value = new InfinityValue(5_300_000_000L, units);
Debug.Log(value); // 5.30B
```

## 多内容表

不同内容可以在不修改全局状态的情况下保持各自不同的名称。

```csharp
var currencyUnits = new InfinityValueUnitNames(new[] { "", "K", "M", "B" });
var damageUnits = new InfinityValueUnitNames(new[] { "", "a", "b", "c" });

InfinityValue gold = new InfinityValue("12K", currencyUnits);
InfinityValue damage = new InfinityValue("12a", damageUnits);
```

## 解析

始终使用创建字符串时所用的同一单位表进行解析。

```csharp
if (InfinityValue.TryParse("42M", currencyUnits, out var value))
    Debug.Log(value);
```

## 验证

`InfinityValueUnitNames` 在以下情况下会抛出异常：

- 名称列表为 null
- 列表为空
- 索引 0 不是空字符串
- 某个单位名称为 null
- 单位名称重复出现
