using System.Globalization;
using System.Text;

namespace Achieve.InfinityValue
{
    public partial struct InfinityValue
    {
        /// <summary>
        /// 정밀도 손실 없이 현재 값을 문자열로 직렬화합니다. <see cref="ToString"/>과 달리
        /// 표시용 반올림을 하지 않으며, <see cref="TryParseExact"/>로 정확히 복원할 수 있습니다.
        /// 단위 이름은 포함하지 않으므로 필요하다면 복원 후 <see cref="WithUnitNames"/>를 적용하세요.
        /// </summary>
        public string ToExactString()
        {
            if (_unitCount == 0) return "0";

            var sb = new StringBuilder();
            for (int i = 0; i < _unitCount; i++)
            {
                var unit = GetUnit(i);
                if (i > 0) sb.Append(';');
                sb.Append(unit.Item1.ToString(CultureInfo.InvariantCulture))
                  .Append(':')
                  .Append(unit.Item2.ToString(CultureInfo.InvariantCulture));
            }
            return sb.ToString();
        }

        /// <summary>
        /// <see cref="ToExactString"/>으로 만든 문자열을 정밀도 손실 없이 복원합니다.
        /// </summary>
        public static bool TryParseExact(string exact, InfinityValueUnitNames unitNames, out InfinityValue result)
        {
            var names = unitNames ?? InfinityValueUnitNames.Default;
            result = default;
            result._unitNames = names;

            if (string.IsNullOrEmpty(exact) || exact == "0") return true;

            foreach (var pair in exact.Split(';'))
            {
                int sep = pair.IndexOf(':');
                if (sep <= 0 || sep == pair.Length - 1)
                {
                    result = default;
                    return false;
                }

                if (!int.TryParse(pair.Substring(0, sep), NumberStyles.Integer, CultureInfo.InvariantCulture, out int index) ||
                    !long.TryParse(pair.Substring(sep + 1), NumberStyles.Integer, CultureInfo.InvariantCulture, out long value))
                {
                    result = default;
                    return false;
                }

                result.AddOrUpdateUnit(index, value);
            }

            result.Normalize();
            return true;
        }
    }
}
