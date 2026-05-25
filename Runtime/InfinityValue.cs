#if USE_NEWTONSOFT_JSON
using Newtonsoft.Json;
#endif
using System;
using System.Runtime.CompilerServices;

namespace Achieve.InfinityValue
{
#if USE_NEWTONSOFT_JSON
    [JsonConverter(typeof(InfinityValueConverter))]
#endif
    /// <summary>
    /// 매우 큰 값을 표현하기 위한 구조체로, 높은 성능과 낮은 GC(가비지 컬렉션) 오버헤드를 목표로 설계되었습니다.
    /// 값은 내부적으로 최대 8개의 (단위 인덱스, 값) 쌍으로 저장됩니다.
    /// </summary>
    public partial struct InfinityValue
        : IEquatable<InfinityValue>, IComparable<InfinityValue>
    {
        // 내부 저장소. 최대 8개의 서로 다른 (단위 인덱스, 값) 쌍을 저장합니다.
        private (int, long) _unit0, _unit1, _unit2, _unit3, _unit4, _unit5, _unit6, _unit7;

        // 현재 구조체에 저장된 활성 단위-값 쌍의 수 (0-8).
        private int _unitCount;

        private InfinityValueUnitNames _unitNames;

        /// <summary>0을 나타내는 InfinityValue입니다.</summary>
        public static readonly InfinityValue Zero = default;

        /// <summary>1을 나타내는 InfinityValue입니다.</summary>
        public static readonly InfinityValue One = new InfinityValue(1L);

        /// <summary>현재 InfinityValue 인스턴스가 0 또는 비어있는 값을 나타내는지 여부를 가져옵니다.</summary>
        public bool IsEmpty => _unitCount == 0;

        /// <summary>현재 값이 문자열 변환과 파싱에 사용하는 단위 이름 집합입니다.</summary>
        public InfinityValueUnitNames UnitNames => _unitNames ?? InfinityValueUnitNames.Default;

        /// <summary>
        /// 같은 숫자 값을 유지하면서 표시/파싱 컨텍스트만 다른 단위 이름 집합으로 교체합니다.
        /// </summary>
        /// <param name="unitNames">이 값이 사용할 단위 이름 집합입니다.</param>
        public InfinityValue WithUnitNames(InfinityValueUnitNames unitNames)
        {
            var copy = this;
            copy._unitNames = unitNames ?? InfinityValueUnitNames.Default;
            return copy;
        }

        /// <summary>
        /// 현재 InfinityValue를 문자열로 변환합니다.
        /// 가장 높은 단위를 기준으로 소수점 2자리까지 표시합니다 (예: "1.23B", "500A").
        /// </summary>
        public override string ToString()
        {
            if (_unitCount == 0) return "0";

            var highest = GetHighestUnit();
            var unitNames = UnitNames;
            if (highest.Item1 >= unitNames.Count) return "Infinity";

            if (highest.Item1 == 0)
                return highest.Item2.ToString();

            string suffix = unitNames[highest.Item1];
            long nextVal = GetValueForIndex(highest.Item1 - 1);
            return $"{highest.Item2}.{(int)(nextVal / 10):D2}{suffix}";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private (int, long) GetHighestUnit()
        {
            var best = GetUnit(0);
            for (int i = 1; i < _unitCount; i++)
            {
                var u = GetUnit(i);
                if (u.Item1 > best.Item1) best = u;
            }
            return best;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private long GetValueForIndex(int unitIndex)
        {
            for (int i = 0; i < _unitCount; i++)
            {
                var u = GetUnit(i);
                if (u.Item1 == unitIndex) return u.Item2;
            }
            return 0;
        }

        /// <summary>
        /// 현재 인스턴스를 다른 InfinityValue와 비교합니다. GC 할당을 유발하지 않습니다.
        /// </summary>
        public int CompareTo(InfinityValue other)
        {
            if (_unitCount == 0 && other._unitCount == 0) return 0;
            if (_unitCount == 0) return -1;
            if (other._unitCount == 0) return 1;

            int aMax = GetHighestUnit().Item1;
            int bMax = other.GetHighestUnit().Item1;
            if (aMax != bMax) return aMax.CompareTo(bMax);

            for (int i = aMax; i >= 0; i--)
            {
                long aVal = GetValueForIndex(i);
                long bVal = other.GetValueForIndex(i);
                if (aVal != bVal) return aVal.CompareTo(bVal);
            }
            return 0;
        }

        /// <summary>
        /// 현재 InfinityValue 인스턴스가 지정된 다른 인스턴스와 같은지 여부를 확인합니다.
        /// 단위 슬롯 순서에 관계없이 의미적으로 동등한지 비교합니다. GC 할당을 유발하지 않습니다.
        /// </summary>
        public bool Equals(InfinityValue other)
        {
            if (_unitCount != other._unitCount) return false;
            for (int i = 0; i < _unitCount; i++)
            {
                var u = GetUnit(i);
                if (other.GetValueForIndex(u.Item1) != u.Item2) return false;
            }
            return true;
        }

        /// <summary>
        /// 현재 인스턴스가 지정된 object와 같은지 여부를 확인합니다.
        /// </summary>
        public override bool Equals(object obj) => obj is InfinityValue other && Equals(other);

        /// <summary>
        /// 현재 인스턴스의 해시 코드를 반환합니다. 단위 슬롯 순서에 독립적입니다. GC 할당을 유발하지 않습니다.
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 0;
                for (int i = 0; i < _unitCount; i++)
                {
                    var u = GetUnit(i);
                    // XOR은 교환법칙이 성립하므로 슬롯 순서에 독립적인 해시를 만듭니다.
                    hash ^= u.Item1 * 397 ^ (int)(u.Item2 ^ (u.Item2 >> 32));
                }
                return hash;
            }
        }
    }
}
