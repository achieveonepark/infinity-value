using System;

namespace Achieve.InfinityValue
{
    public partial struct InfinityValue
    {
        #region Comparison Operators
        /// <summary>첫 번째 값이 두 번째 값보다 큰지 여부를 확인합니다.</summary>
        public static bool operator >(InfinityValue a, InfinityValue b) => a.CompareTo(b) > 0;
        /// <summary>첫 번째 값이 두 번째 값보다 작은지 여부를 확인합니다.</summary>
        public static bool operator <(InfinityValue a, InfinityValue b) => a.CompareTo(b) < 0;
        /// <summary>첫 번째 값이 두 번째 값보다 크거나 같은지 여부를 확인합니다.</summary>
        public static bool operator >=(InfinityValue a, InfinityValue b) => a.CompareTo(b) >= 0;
        /// <summary>첫 번째 값이 두 번째 값보다 작거나 같은지 여부를 확인합니다.</summary>
        public static bool operator <=(InfinityValue a, InfinityValue b) => a.CompareTo(b) <= 0;
        /// <summary>두 값이 같은지 여부를 확인합니다.</summary>
        public static bool operator ==(InfinityValue a, InfinityValue b) => a.Equals(b);
        /// <summary>두 값이 다른지 여부를 확인합니다.</summary>
        public static bool operator !=(InfinityValue a, InfinityValue b) => !a.Equals(b);

        // long 타입과의 비교 연산자 오버로드
        public static bool operator >(InfinityValue a, long b) => a > new InfinityValue(b);
        public static bool operator <(InfinityValue a, long b) => a < new InfinityValue(b);
        public static bool operator >=(InfinityValue a, long b) => a >= new InfinityValue(b);
        public static bool operator <=(InfinityValue a, long b) => a <= new InfinityValue(b);
        public static bool operator ==(InfinityValue a, long b) => a == new InfinityValue(b);
        public static bool operator !=(InfinityValue a, long b) => a != new InfinityValue(b);
        public static bool operator >(long a, InfinityValue b) => new InfinityValue(a) > b;
        public static bool operator <(long a, InfinityValue b) => new InfinityValue(a) < b;
        public static bool operator >=(long a, InfinityValue b) => new InfinityValue(a) >= b;
        public static bool operator <=(long a, InfinityValue b) => new InfinityValue(a) <= b;
        public static bool operator ==(long a, InfinityValue b) => new InfinityValue(a) == b;
        public static bool operator !=(long a, InfinityValue b) => new InfinityValue(a) != b;
        #endregion

        #region Arithmetic Operators
        /// <summary>두 개의 InfinityValue를 더합니다.</summary>
        public static InfinityValue operator +(InfinityValue a, InfinityValue b)
        {
            var result = a;
            for (int i = 0; i < b._unitCount; i++)
            {
                var unit = b.GetUnit(i);
                result.AddOrUpdateUnit(unit.Item1, unit.Item2);
            }
            result.Normalize();
            return result;
        }

        /// <summary>한 InfinityValue에서 다른 InfinityValue를 뺍니다. 결과가 음수이면 0을 반환합니다.</summary>
        public static InfinityValue operator -(InfinityValue a, InfinityValue b)
        {
            var result = a;
            for (int i = 0; i < b._unitCount; i++)
            {
                var unit = b.GetUnit(i);
                result.AddOrUpdateUnit(unit.Item1, -unit.Item2);
            }
            result.Normalize();
            return result;
        }

        /// <summary>InfinityValue에 long 값을 곱합니다.</summary>
        public static InfinityValue operator *(InfinityValue a, long b)
        {
            if (b == 0) return default;
            var result = new InfinityValue();
            for (int i = 0; i < a._unitCount; i++)
            {
                var unit = a.GetUnit(i);
                result.AddOrUpdateUnit(unit.Item1, unit.Item2 * b);
            }
            result.Normalize();
            return result;
        }

        /// <summary>InfinityValue에 double 값을 곱합니다. 소수 배율(예: 1.5배)에 유용합니다.</summary>
        public static InfinityValue operator *(InfinityValue a, double b)
        {
            if (b <= 0) return default;
            var result = new InfinityValue();
            for (int i = 0; i < a._unitCount; i++)
            {
                var unit = a.GetUnit(i);
                result.AddOrUpdateUnit(unit.Item1, (long)(unit.Item2 * b));
            }
            result.Normalize();
            return result;
        }

        /// <summary>InfinityValue를 long 값으로 나눕니다.</summary>
        public static InfinityValue operator /(InfinityValue a, long b)
        {
            if (b == 0) throw new DivideByZeroException();

            var result = new InfinityValue();
            long remainder = 0;

            for (int i = a._unitCount - 1; i >= 0; i--)
            {
                var unit = a.GetUnit(i);
                long currentValue = unit.Item2 + remainder * 1000;
                long divisionResult = currentValue / b;
                remainder = currentValue % b;

                if (divisionResult > 0)
                    result.AddUnit(unit.Item1, divisionResult);
            }

            result.Normalize();
            return result;
        }

        /// <summary>InfinityValue를 double 값으로 나눕니다.</summary>
        public static InfinityValue operator /(InfinityValue a, double b)
        {
            if (b == 0) throw new DivideByZeroException();
            return a * (1.0 / b);
        }

        /// <summary>InfinityValue에 int 값을 곱합니다.</summary>
        public static InfinityValue operator *(InfinityValue a, int b) => a * (long)b;
        /// <summary>InfinityValue를 int 값으로 나눕니다.</summary>
        public static InfinityValue operator /(InfinityValue a, int b) => a / (long)b;
        /// <summary>InfinityValue에 long 값을 더합니다.</summary>
        public static InfinityValue operator +(InfinityValue a, long b) => a + new InfinityValue(b);
        /// <summary>InfinityValue에서 long 값을 뺍니다.</summary>
        public static InfinityValue operator -(InfinityValue a, long b) => a - new InfinityValue(b);
        #endregion

        #region Conversion Operators
        /// <summary>
        /// InfinityValue를 long으로 명시적으로 변환합니다.
        /// 값이 long의 최대값을 초과하는 경우 정확도가 손실될 수 있습니다.
        /// </summary>
        public static explicit operator long(InfinityValue value)
        {
            if (value._unitCount == 0) return 0;

            int lowestUnitIndex = int.MaxValue;
            for (int i = 0; i < value._unitCount; i++)
            {
                var unit = value.GetUnit(i);
                if (unit.Item1 < lowestUnitIndex) lowestUnitIndex = unit.Item1;
            }

            long result = 0;
            for (int i = 0; i < value._unitCount; i++)
            {
                var unit = value.GetUnit(i);
                int diff = unit.Item1 - lowestUnitIndex;
                if (diff >= 2) continue; // long 범위 초과 근사

                long multiplier = 1;
                for (int j = 0; j < diff; j++) multiplier *= 1000;
                result += unit.Item2 * multiplier;
            }
            return result;
        }

        /// <summary>
        /// InfinityValue를 double로 명시적으로 변환합니다.
        /// 큰 값의 경우 정확도가 손실될 수 있습니다.
        /// </summary>
        public static explicit operator double(InfinityValue value)
        {
            if (value._unitCount == 0) return 0;

            double result = 0;
            for (int i = 0; i < value._unitCount; i++)
            {
                var unit = value.GetUnit(i);
                result += unit.Item2 * Math.Pow(1000, unit.Item1);
            }
            return result;
        }

        /// <summary>
        /// InfinityValue를 float으로 명시적으로 변환합니다.
        /// 큰 값의 경우 정확도가 손실될 수 있습니다.
        /// </summary>
        public static explicit operator float(InfinityValue value) => (float)(double)value;
        #endregion
    }
}
