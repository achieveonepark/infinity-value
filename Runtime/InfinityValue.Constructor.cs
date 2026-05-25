using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Achieve.InfinityValue
{
    public partial struct InfinityValue
    {
        /// <summary>long 타입의 숫자를 사용하여 InfinityValue 인스턴스를 생성합니다.</summary>
        public InfinityValue(long number) : this(number, InfinityValueUnitNames.Default) { }

        /// <summary>long 타입의 숫자와 단위 이름 집합을 사용하여 InfinityValue 인스턴스를 생성합니다.</summary>
        public InfinityValue(long number, InfinityValueUnitNames unitNames)
        {
            _unit0 = _unit1 = _unit2 = _unit3 = _unit4 = _unit5 = _unit6 = _unit7 = default;
            _unitCount = 0;
            _unitNames = unitNames ?? InfinityValueUnitNames.Default;

            if (number <= 0) return;

            long rem = number;
            int index = 0;
            while (rem > 0 && index < 8)
            {
                long part = rem % 1000;
                if (part > 0) AddUnit(index, part);
                rem /= 1000;
                index++;
            }
        }

        /// <summary>double 타입의 숫자를 사용하여 InfinityValue 인스턴스를 생성합니다. 소수점 이하는 버려집니다.</summary>
        public InfinityValue(double number) : this(number, InfinityValueUnitNames.Default) { }

        /// <summary>double 타입의 숫자와 단위 이름 집합을 사용하여 InfinityValue 인스턴스를 생성합니다.</summary>
        public InfinityValue(double number, InfinityValueUnitNames unitNames)
        {
            _unit0 = _unit1 = _unit2 = _unit3 = _unit4 = _unit5 = _unit6 = _unit7 = default;
            _unitCount = 0;
            _unitNames = unitNames ?? InfinityValueUnitNames.Default;

            if (number <= 0 || double.IsNaN(number) || double.IsInfinity(number)) return;

            double rem = Math.Floor(number);
            int index = 0;
            while (rem >= 1.0 && index < 8)
            {
                long part = (long)(rem % 1000);
                if (part > 0) AddUnit(index, part);
                rem = Math.Floor(rem / 1000);
                index++;
            }
        }

        /// <summary>float 타입의 숫자를 사용하여 InfinityValue 인스턴스를 생성합니다. 소수점 이하는 버려집니다.</summary>
        public InfinityValue(float number) : this((double)number, InfinityValueUnitNames.Default) { }

        /// <summary>float 타입의 숫자와 단위 이름 집합을 사용하여 InfinityValue 인스턴스를 생성합니다.</summary>
        public InfinityValue(float number, InfinityValueUnitNames unitNames) : this((double)number, unitNames) { }

        /// <summary>BigInteger 타입의 숫자를 사용하여 InfinityValue 인스턴스를 생성합니다.</summary>
        public InfinityValue(BigInteger number) : this(number, InfinityValueUnitNames.Default) { }

        /// <summary>BigInteger 타입의 숫자와 단위 이름 집합을 사용하여 InfinityValue 인스턴스를 생성합니다.</summary>
        public InfinityValue(BigInteger number, InfinityValueUnitNames unitNames)
        {
            _unit0 = _unit1 = _unit2 = _unit3 = _unit4 = _unit5 = _unit6 = _unit7 = default;
            _unitCount = 0;
            _unitNames = unitNames ?? InfinityValueUnitNames.Default;

            int index = 0;
            while (number > 0 && index < 8)
            {
                var rem = number % 1000;
                if (rem > 0) AddUnit(index, (long)rem);
                number /= 1000;
                index++;
            }

            Normalize();
        }

        /// <summary>
        /// 문자열 표현(예: "1.23B", "500A")을 파싱하여 InfinityValue 인스턴스를 생성합니다.
        /// 파싱에 실패하면 0을 반환합니다.
        /// </summary>
        public InfinityValue(string input) : this(input, InfinityValueUnitNames.Default) { }

        /// <summary>
        /// 문자열 표현과 단위 이름 집합을 사용하여 InfinityValue 인스턴스를 생성합니다.
        /// 파싱에 실패하면 0을 반환합니다.
        /// </summary>
        public InfinityValue(string input, InfinityValueUnitNames unitNames)
        {
            _unit0 = _unit1 = _unit2 = _unit3 = _unit4 = _unit5 = _unit6 = _unit7 = default;
            _unitCount = 0;
            _unitNames = unitNames ?? InfinityValueUnitNames.Default;

            if (string.IsNullOrEmpty(input) || input == "0") return;

            if (!TryParse(input, UnitNames, out var parsed)) return;

            this = parsed;
        }

        /// <summary>
        /// 문자열을 기본 단위 이름 집합으로 InfinityValue 파싱을 시도합니다.
        /// </summary>
        /// <param name="input">파싱할 문자열입니다.</param>
        /// <param name="result">파싱 성공 시 결과값입니다.</param>
        /// <returns>파싱에 성공하면 true, 실패하면 false입니다.</returns>
        public static bool TryParse(string input, out InfinityValue result)
            => TryParse(input, InfinityValueUnitNames.Default, out result);

        /// <summary>
        /// 문자열을 지정된 단위 이름 집합으로 InfinityValue 파싱을 시도합니다.
        /// </summary>
        /// <param name="input">파싱할 문자열입니다.</param>
        /// <param name="unitNames">파싱에 사용할 단위 이름 집합입니다.</param>
        /// <param name="result">파싱 성공 시 결과값입니다.</param>
        /// <returns>파싱에 성공하면 true, 실패하면 false입니다.</returns>
        public static bool TryParse(string input, InfinityValueUnitNames unitNames, out InfinityValue result)
        {
            var names = unitNames ?? InfinityValueUnitNames.Default;
            result = default;
            result._unitNames = names;

            if (string.IsNullOrEmpty(input) || input == "0") return true;

            int pos = 0;
            bool parsedAny = false;
            while (pos < input.Length)
            {
                if (!char.IsDigit(input[pos]))
                {
                    pos++;
                    continue;
                }

                int numStart = pos;
                while (pos < input.Length && char.IsDigit(input[pos])) pos++;

                if (!long.TryParse(input.Substring(numStart, pos - numStart), out long value))
                    return false;

                int fractionStart = -1;
                int fractionLength = 0;
                if (pos < input.Length && input[pos] == '.')
                {
                    pos++;
                    fractionStart = pos;
                    while (pos < input.Length && char.IsDigit(input[pos])) pos++;
                    fractionLength = pos - fractionStart;

                    if (fractionLength == 0)
                        return false;
                }

                int unitStart = pos;
                while (pos < input.Length && char.IsLetter(input[pos])) pos++;

                int unitIndex = 0;
                if (pos > unitStart)
                {
                    string unitName = input.Substring(unitStart, pos - unitStart);
                    if (!names.TryGetIndex(unitName, out unitIndex))
                        return false;
                }

                result.AddUnit(unitIndex, value);

                if (fractionLength > 0)
                {
                    if (unitIndex == 0)
                        return false;

                    long lowerUnitValue = ReadFractionAsLowerUnit(input, fractionStart, fractionLength);
                    if (lowerUnitValue > 0)
                        result.AddUnit(unitIndex - 1, lowerUnitValue);
                }

                parsedAny = true;
            }

            if (!parsedAny) return false;

            result.Normalize();
            return true;
        }

        private static long ReadFractionAsLowerUnit(string input, int start, int length)
        {
            int digits = Math.Min(length, 3);
            long value = 0;

            for (int i = 0; i < digits; i++)
                value = value * 10 + input[start + i] - '0';

            for (int i = digits; i < 3; i++)
                value *= 10;

            return value;
        }

        private void AddUnit(int index, long value)
        {
            if (_unitCount >= 8) return;
            switch (_unitCount)
            {
                case 0: _unit0 = (index, value); break;
                case 1: _unit1 = (index, value); break;
                case 2: _unit2 = (index, value); break;
                case 3: _unit3 = (index, value); break;
                case 4: _unit4 = (index, value); break;
                case 5: _unit5 = (index, value); break;
                case 6: _unit6 = (index, value); break;
                case 7: _unit7 = (index, value); break;
            }
            _unitCount++;
        }

        private void RemoveAt(int pos)
        {
            for (int j = pos; j < _unitCount - 1; j++)
                SetUnit(j, GetUnit(j + 1));
            _unitCount--;
        }

        private void InsertAt(int pos, (int, long) unit)
        {
            if (_unitCount >= 8) return;
            for (int j = _unitCount; j > pos; j--)
                SetUnit(j, GetUnit(j - 1));
            SetUnit(pos, unit);
            _unitCount++;
        }

        // 단위들을 정규화합니다:
        // 1. 단위 인덱스 기준 오름차순 정렬 (삽입 정렬)
        // 2. 같은 인덱스 병합
        // 3. 올림(carry) 및 내림(borrow) 처리 - 재귀 없이 좌->우 순회
        // 4. 0 이하의 단위 제거
        private void Normalize()
        {
            for (int i = 1; i < _unitCount; i++)
            {
                var key = GetUnit(i);
                int j = i - 1;
                while (j >= 0 && GetUnit(j).Item1 > key.Item1)
                {
                    SetUnit(j + 1, GetUnit(j));
                    j--;
                }
                SetUnit(j + 1, key);
            }

            for (int i = 0; i < _unitCount - 1;)
            {
                var a = GetUnit(i);
                var b = GetUnit(i + 1);
                if (a.Item1 == b.Item1)
                {
                    SetUnit(i, (a.Item1, a.Item2 + b.Item2));
                    RemoveAt(i + 1);
                }
                else
                {
                    i++;
                }
            }

            for (int i = 0; i < _unitCount; i++)
            {
                var (idx, val) = GetUnit(i);

                if (val < 0)
                {
                    long borrow = (-val + 999) / 1000;
                    SetUnit(i, (idx, val + borrow * 1000));

                    if (i + 1 < _unitCount)
                    {
                        var next = GetUnit(i + 1);
                        if (next.Item1 == idx + 1)
                            SetUnit(i + 1, (next.Item1, next.Item2 - borrow));
                        else
                            InsertAt(i + 1, (idx + 1, -borrow));
                    }
                    else
                    {
                        SetUnit(i, (idx, 0));
                    }
                }
                else if (val >= 1000)
                {
                    long carry = val / 1000;
                    SetUnit(i, (idx, val % 1000));

                    if (i + 1 < _unitCount)
                    {
                        var next = GetUnit(i + 1);
                        if (next.Item1 == idx + 1)
                            SetUnit(i + 1, (next.Item1, next.Item2 + carry));
                        else
                            InsertAt(i + 1, (idx + 1, carry));
                    }
                    else
                    {
                        InsertAt(i + 1, (idx + 1, carry));
                    }
                }
            }

            for (int i = 0; i < _unitCount;)
            {
                if (GetUnit(i).Item2 <= 0)
                    RemoveAt(i);
                else
                    i++;
            }
        }

        // 연산자에서 사용: 해당 인덱스가 있으면 값을 더하고, 없으면 새 슬롯에 추가합니다.
        // Normalize를 호출하지 않습니다. 연산자가 마지막에 한 번 호출합니다.
        private void AddOrUpdateUnit(int index, long value)
        {
            for (int i = 0; i < _unitCount; i++)
            {
                if (GetUnit(i).Item1 == index)
                {
                    var u = GetUnit(i);
                    SetUnit(i, (u.Item1, u.Item2 + value));
                    return;
                }
            }
            AddUnit(index, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private (int, long) GetUnit(int unit)
        {
            switch (unit)
            {
                case 0: return _unit0;
                case 1: return _unit1;
                case 2: return _unit2;
                case 3: return _unit3;
                case 4: return _unit4;
                case 5: return _unit5;
                case 6: return _unit6;
                case 7: return _unit7;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetUnit(int unit, (int, long) value)
        {
            switch (unit)
            {
                case 0: _unit0 = value; break;
                case 1: _unit1 = value; break;
                case 2: _unit2 = value; break;
                case 3: _unit3 = value; break;
                case 4: _unit4 = value; break;
                case 5: _unit5 = value; break;
                case 6: _unit6 = value; break;
                case 7: _unit7 = value; break;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>int에서 InfinityValue로의 암시적 변환을 정의합니다.</summary>
        public static implicit operator InfinityValue(int value) => new InfinityValue((long)value);
        /// <summary>long에서 InfinityValue로의 암시적 변환을 정의합니다.</summary>
        public static implicit operator InfinityValue(long value) => new InfinityValue(value);
        /// <summary>float에서 InfinityValue로의 암시적 변환을 정의합니다.</summary>
        public static implicit operator InfinityValue(float value) => new InfinityValue(value);
        /// <summary>double에서 InfinityValue로의 암시적 변환을 정의합니다.</summary>
        public static implicit operator InfinityValue(double value) => new InfinityValue(value);
        /// <summary>string에서 InfinityValue로의 암시적 변환을 정의합니다.</summary>
        public static implicit operator InfinityValue(string value) => new InfinityValue(value);
        /// <summary>BigInteger에서 InfinityValue로의 암시적 변환을 정의합니다.</summary>
        public static implicit operator InfinityValue(BigInteger value) => new InfinityValue(value);
    }
}
