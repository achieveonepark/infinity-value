# Unit Names

`InfinityValueUnitNames` is an instance object. It replaces the old global configuration style and lets each content system own its own suffix table.

## Default Units

The default table starts with an empty base unit and continues with `A`, `B`, `C` through `CZ`.

```csharp
InfinityValue value = new InfinityValue(1_500_000L);
Debug.Log(value); // 1.50B
```

## Custom Units

Index 0 must be an empty string because it represents the base number tier.

```csharp
var units = new InfinityValueUnitNames(new[]
{
    "", "K", "M", "B", "T", "Qa", "Qi", "Sx"
});

InfinityValue value = new InfinityValue(5_300_000_000L, units);
Debug.Log(value); // 5.30B
```

## Multiple Content Tables

Different content can keep different names without touching global state.

```csharp
var currencyUnits = new InfinityValueUnitNames(new[] { "", "K", "M", "B" });
var damageUnits = new InfinityValueUnitNames(new[] { "", "a", "b", "c" });

InfinityValue gold = new InfinityValue("12K", currencyUnits);
InfinityValue damage = new InfinityValue("12a", damageUnits);
```

## Parsing

Always parse saved strings with the same unit table that created them.

```csharp
if (InfinityValue.TryParse("42M", currencyUnits, out var value))
    Debug.Log(value);
```

## Validation

`InfinityValueUnitNames` throws an exception when:

- The name list is null.
- The list is empty.
- Index 0 is not an empty string.
- A unit name is null.
- A unit name appears more than once.
