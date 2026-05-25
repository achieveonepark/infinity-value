using Achieve.InfinityValue;
using UnityEngine;

namespace Achieve.InfinityValue.Samples
{
    [CreateAssetMenu(fileName = "Unit Name Content", menuName = "Infinity Value/Unit Name Content")]
    public sealed class UnitNameContent : ScriptableObject
    {
        [SerializeField] private string[] names =
        {
            "", "K", "M", "B", "T", "Qa", "Qi"
        };

        private InfinityValueUnitNames _unitNames;

        public InfinityValueUnitNames UnitNames
        {
            get
            {
                if (_unitNames == null)
                    _unitNames = new InfinityValueUnitNames(names);

                return _unitNames;
            }
        }

        public InfinityValue Create(long value) => new InfinityValue(value, UnitNames);

        public InfinityValue Create(string value) => new InfinityValue(value, UnitNames);

        public bool TryParse(string raw, out InfinityValue value)
            => InfinityValue.TryParse(raw, UnitNames, out value);

        private void OnValidate()
        {
            _unitNames = null;
        }
    }
}
