using System;
using System.Collections.Generic;

namespace Achieve.InfinityValue
{
    /// <summary>
    /// InfinityValue 인스턴스가 문자열 파싱과 표시에서 사용할 단위 이름 집합입니다.
    /// 전역 상태를 바꾸지 않고 컨텐츠별 단위 체계를 독립적으로 유지할 수 있습니다.
    /// </summary>
    public sealed class InfinityValueUnitNames
    {
        private static readonly string[] DefaultNames =
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

        private readonly string[] _names;
        private readonly Dictionary<string, int> _lookup;

        /// <summary>기본 A, B, C ... CZ 단위 이름 집합입니다.</summary>
        public static readonly InfinityValueUnitNames Default = new InfinityValueUnitNames(DefaultNames);

        /// <summary>단위 이름 개수입니다.</summary>
        public int Count => _names.Length;

        /// <summary>단위 인덱스에 해당하는 이름을 반환합니다.</summary>
        public string this[int index] => _names[index];

        /// <summary>단위 이름 목록의 읽기 전용 뷰입니다.</summary>
        public IReadOnlyList<string> Names => _names;

        /// <summary>
        /// 인덱스 순서대로 정렬된 단위 이름 집합을 생성합니다.
        /// 인덱스 0은 단위 없는 숫자를 의미하므로 빈 문자열이어야 합니다.
        /// </summary>
        public InfinityValueUnitNames(IEnumerable<string> names)
        {
            if (names == null) throw new ArgumentNullException(nameof(names));

            _names = CopyNames(names);
            if (_names.Length == 0)
                throw new ArgumentException("Unit names must contain at least the base unit.", nameof(names));
            if (_names[0] != string.Empty)
                throw new ArgumentException("Unit name at index 0 must be an empty string.", nameof(names));

            _lookup = new Dictionary<string, int>(_names.Length);
            for (int i = 0; i < _names.Length; i++)
            {
                string name = _names[i] ?? throw new ArgumentException("Unit names cannot contain null.", nameof(names));
                if (_lookup.ContainsKey(name))
                    throw new ArgumentException($"Duplicate unit name: {name}", nameof(names));
                _lookup.Add(name, i);
            }
        }

        /// <summary>문자열 단위 이름을 단위 인덱스로 변환합니다.</summary>
        public bool TryGetIndex(string unitName, out int index) => _lookup.TryGetValue(unitName, out index);

        private static string[] CopyNames(IEnumerable<string> names)
        {
            var list = new List<string>();
            foreach (string name in names)
                list.Add(name);
            return list.ToArray();
        }
    }
}
