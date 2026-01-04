using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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

        // 단위 이름의 문자열 표현(예: "A", "B", "K", "M")을 담고 있는 정적 리스트.
        // 이 리스트는 단위 인덱스와 문자열 이름 간의 매핑을 정의합니다.
        private static List<string> _unitNames;

        /// <summary>
        /// 현재 InfinityValue 인스턴스가 0 또는 비어있는 값을 나타내는지 여부를 가져옵니다.
        /// </summary>
        public bool IsEmpty => _unitCount == 0;

        // 사용자 지정 단위 이름이 설정되지 않은 경우 사용되는 기본 단위 이름입니다.
        // 다양한 등급(예: A=1, B=2, ..., CZ=77)에 대한 인덱스-이름 매핑을 정의합니다.
        private static readonly List<string> defaultUnitNames = new List<string>
        {
            "", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M",
            "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
            "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM",
            "AN", "AO", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY", "AZ",
            "BA", "BB", "BC", "BD", "BE", "BF", "BG", "BH", "BI", "BJ", "BK", "BL", "BM",
            "BN", "BO", "BP", "BQ", "BR", "BS", "BT", "BU", "BV", "BW", "BX", "BY", "BZ",
            "CA", "CB", "CC", "CD", "CE", "CF", "CG", "CH", "CI", "CJ", "CK", "CL", "CM",
            "CN", "CO", "CP", "CQ", "CR", "CS", "CT", "CU", "CV", "CW", "CX", "CY", "CZ",
        };

        // 정적 단위 이름 리스트가 null이거나 비어있는 경우 기본값으로 초기화되도록 보장합니다.
        private static void ValidateUnitNames()
        {
            if (_unitNames == null || _unitNames.Count == 0)
            {
                _unitNames = defaultUnitNames;
            }
        }

        /// <summary>
        /// InfinityValue에 대한 사용자 지정 단위 이름을 설정합니다.
        /// 이를 통해 기본 "A, B, C..." 시스템 대신 "K", "M", "B", "T"와 같은 사용자 지정 이름을 사용할 수 있습니다.
        /// </summary>
        /// <param name="unitNames">단위의 인덱스 순서대로 정렬된 문자열 표현 리스트입니다.</param>
        public static void SetUnitNames(List<string> unitNames)
        {
            _unitNames = unitNames;
        }

        /// <summary>
        /// 현재 InfinityValue를 문자열 표현으로 변환합니다.
        /// 가장 높은 유효 단위를 기준으로 값을 표시합니다 (예: "123C 200B" 또는 "500K").
        /// </summary>
        /// <returns>값을 나타내는 문자열입니다.</returns>
        public override string ToString()
        {
            if (_unitCount == 0) return "0";

            // Find the highest unit to display.
            // The units are not guaranteed to be sorted perfectly after all operations.
            (int, long) highestUnit = GetUnit(0);
            for (int i = 1; i < _unitCount; i++)
            {
                var unit = GetUnit(i);
                if (unit.Item1 > highestUnit.Item1)
                {
                    highestUnit = unit;
                }
            }

            ValidateUnitNames();
            if (highestUnit.Item1 >= _unitNames.Count)
            {
                return "Infinity"; // Or some other error representation
            }

            return $"{highestUnit.Item2}{_unitNames[highestUnit.Item1]}";
        }

        /// <summary>
        /// 현재 인스턴스를 다른 InfinityValue와 비교하고, 정렬 순서에서 현재 인스턴스가 다른 인스턴스보다 앞에 오는지, 뒤에 오는지, 또는 같은 위치에 있는지를 나타내는 정수를 반환합니다.
        /// 이 비교 연산은 GC 할당을 유발하지 않습니다.
        /// </summary>
        /// <param name="other">이 인스턴스와 비교할 객체입니다.</param>
        /// <returns>비교 대상의 상대적인 순서를 나타내는 값입니다.</returns>
        public int CompareTo(InfinityValue other)
        {
            // Compare from the highest unit downwards.
            int aCount = this._unitCount;
            int bCount = other._unitCount;

            if (aCount == 0 && bCount == 0) return 0;
            if (aCount == 0) return -1;
            if (bCount == 0) return 1;

            int aMaxIndex = -1, bMaxIndex = -1;
            for (int i = 0; i < aCount; i++)
            {
                if (GetUnit(i).Item1 > aMaxIndex) aMaxIndex = GetUnit(i).Item1;
            }

            for (int i = 0; i < bCount; i++)
            {
                if (other.GetUnit(i).Item1 > bMaxIndex) bMaxIndex = other.GetUnit(i).Item1;
            }

            if (aMaxIndex != bMaxIndex) return aMaxIndex.CompareTo(bMaxIndex);

            // If highest units are the same, compare all units.
            for (int i = aMaxIndex; i >= 0; i--)
            {
                long aValue = 0;
                long bValue = 0;
                for (int j = 0; j < aCount; j++)
                {
                    if (GetUnit(j).Item1 == i) aValue = GetUnit(j).Item2;
                }

                for (int j = 0; j < bCount; j++)
                {
                    if (other.GetUnit(j).Item1 == i) bValue = other.GetUnit(j).Item2;
                }

                if (aValue != bValue) return aValue.CompareTo(bValue);
            }

            return 0;
        }

        /// <summary>
        /// 현재 InfinityValue 인스턴스가 지정된 다른 인스턴스와 같은지 여부를 확인합니다.
        /// 이 동등성 검사는 GC 할당을 유발하지 않습니다.
        /// </summary>
        /// <param name="other">이 인스턴스와 비교할 객체입니다.</param>
        /// <returns>두 인스턴스가 같으면 true이고, 그렇지 않으면 false입니다.</returns>
        public bool Equals(InfinityValue other)
        {
            if (_unitCount != other._unitCount) return false;

            for (int i = 0; i < _unitCount; i++)
            {
                if (GetUnit(i) != other.GetUnit(i))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 현재 인스턴스가 지정된 object와 같은지 여부를 확인합니다.
        /// 값 타입에 대한 박싱(boxing)을 효율적으로 처리합니다.
        /// </summary>
        /// <param name="obj">현재 인스턴스와 비교할 객체입니다.</param>
        /// <returns>지정된 객체가 현재 인스턴스와 같으면 true이고, 그렇지 않으면 false입니다.</returns>
        public override bool Equals(object obj)
        {
            return obj is InfinityValue other && Equals(other);
        }
    }
}