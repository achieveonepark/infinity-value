using Achieve.InfinityValue;
using UnityEngine;

namespace Achieve.InfinityValue.Samples
{
    public sealed class ContentUnitNamesExample : MonoBehaviour
    {
        [SerializeField] private UnitNameContent currencyUnits;
        [SerializeField] private UnitNameContent damageUnits;

        private readonly InfinityValueUnitNames _fallbackCurrencyUnits =
            new InfinityValueUnitNames(new[] { "", "K", "M", "B", "T" });

        private readonly InfinityValueUnitNames _fallbackDamageUnits =
            new InfinityValueUnitNames(new[] { "", "a", "b", "c", "d" });

        private void Start()
        {
            InfinityValueUnitNames currency = currencyUnits != null
                ? currencyUnits.UnitNames
                : _fallbackCurrencyUnits;

            InfinityValueUnitNames damage = damageUnits != null
                ? damageUnits.UnitNames
                : _fallbackDamageUnits;

            InfinityValue gold = new InfinityValue("5B", currency);
            InfinityValue hitDamage = new InfinityValue("42c", damage);

            Debug.Log($"Gold: {gold}");
            Debug.Log($"Damage: {hitDamage}");
        }
    }
}
