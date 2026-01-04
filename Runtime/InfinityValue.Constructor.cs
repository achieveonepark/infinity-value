using System;
using System.Collections.Generic;
using System.Numerics;

namespace Achieve.InfinityValue
{
    public partial struct InfinityValue
    {
        /// <summary>
        /// long 타입의 숫자를 사용하여 InfinityValue 인스턴스를 생성합니다.
        /// </summary>
        /// <param name="number">초기화할 숫자 값입니다.</param>
        public InfinityValue(long number)
        {
            _unit0 = _unit1 = _unit2 = _unit3 = _unit4 = _unit5 = _unit6 = _unit7 = default;
            _unitCount = 0;

            if (number == 0) return;

            long rem = number;
            int index = 0;
            while(rem > 0)
            {
                long part = rem % 1000;
                if (part > 0)
                {
                    AddUnit(index, part);
                }
                rem /= 1000;
                index++;
            }
        }
        
        /// <summary>
        /// float 타입의 숫자를 사용하여 InfinityValue 인스턴스를 생성합니다. 소수점 이하는 버려집니다.
        /// </summary>
        /// <param name="number">초기화할 숫자 값입니다.</param>
        public InfinityValue(float number)
        {
            this = new InfinityValue((long)number);
        }

        /// <summary>
        /// 문자열 표현(예: "1.23M", "500K")을 파싱하여 InfinityValue 인스턴스를 생성합니다.
        /// </summary>
        /// <param name="input">파싱할 문자열입니다.</param>
        public InfinityValue(string input)
        {
            _unit0 = _unit1 = _unit2 = _unit3 = _unit4 = _unit5 = _unit6 = _unit7 = default;
            _unitCount = 0;
            
            ValidateUnitNames();

            if (string.IsNullOrEmpty(input) || input == "0")
            {
                return;
            }

            int currentPos = 0;
            while (currentPos < input.Length)
            {
                int numEnd = currentPos;
                while (numEnd < input.Length && char.IsDigit(input[numEnd]))
                {
                    numEnd++;
                }

                if (numEnd == currentPos)
                {
                    currentPos++;
                    continue;
                }
                
                long value = long.Parse(input.Substring(currentPos, numEnd - currentPos));

                currentPos = numEnd;
                int unitEnd = currentPos;
                while (unitEnd < input.Length && char.IsLetter(input[unitEnd]))
                {
                    unitEnd++;
                }

                if (unitEnd > currentPos)
                {
                    string unitName = input.Substring(currentPos, unitEnd - currentPos);
                    int unitIndex = _unitNames.IndexOf(unitName);
                    if (unitIndex != -1)
                    {
                        AddUnit(unitIndex, value);
                    }
                }
                
                currentPos = unitEnd;
            }
            
            Normalize();
        }

        // 새 단위-값 쌍을 내부 저장소의 첫 번째 비어있는 슬롯에 추가합니다.
        // 슬롯이 8개 모두 차 있으면 추가되지 않습니다.
        private void AddUnit(int index, long value)
        {
            if (_unitCount >= 8)
            {
                return;
            }
            
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

        // 내부 단위들을 정규화합니다.
        // 1. 단위 인덱스를 기준으로 오름차순 정렬합니다.
        // 2. 같은 인덱스를 가진 단위를 병합합니다.
        // 3. 1000이 넘는 값을 다음 단위로 올립니다.
        private void Normalize()
        {
            for (int i = 0; i < _unitCount - 1; i++)
            {
                for (int j = 0; j < _unitCount - i - 1; j++)
                {
                    if (GetUnit(j).Item1 > GetUnit(j + 1).Item1)
                    {
                        var temp = GetUnit(j);
                        SetUnit(j, GetUnit(j + 1));
                        SetUnit(j + 1, temp);
                    }
                }
            }

            for (int i = 0; i < _unitCount - 1; i++)
            {
                if (GetUnit(i).Item1 == GetUnit(i + 1).Item1)
                {
                    (int index, long value) unitA = GetUnit(i);
                    (int index, long value) unitB = GetUnit(i + 1);
                    SetUnit(i, (unitA.Item1, unitA.Item2 + unitB.Item2));
                    
                    for (int j = i + 1; j < _unitCount - 1; j++)
                    {
                        SetUnit(j, GetUnit(j + 1));
                    }
                    _unitCount--;
                    i--;
                }
            }
            
            for (int i = 0; i < _unitCount; i++)
            {
                (int index, long value) = GetUnit(i);
                if (value >= 1000)
                {
                    long overflow = value / 1000;
                    value %= 1000;
                    SetUnit(i, (index, value));
                    AddOrUpdateUnit(index + 1, overflow);
                }
            }
        }

        // 정규화 과정에서 특정 인덱스의 값을 추가하거나 업데이트하는 헬퍼 메서드입니다.
        private void AddOrUpdateUnit(int index, long value)
        {
            for (int i = 0; i < _unitCount; i++)
            {
                if (GetUnit(i).Item1 == index)
                {
                    (int idx, long val) = GetUnit(i);
                    SetUnit(i, (idx, val + value));
                    return;
                }
            }
            AddUnit(index, value);
            Normalize();
        }
        
        // 특정 슬롯 인덱스의 단위-값 쌍을 가져옵니다.
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
        
        // 특정 슬롯 인덱스에 단위-값 쌍을 설정합니다.
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

        /// <summary>
        /// int에서 InfinityValue로의 암시적 변환을 정의합니다.
        /// </summary>
        public static implicit operator InfinityValue(int value) => new InfinityValue(value);
        /// <summary>
        /// long에서 InfinityValue로의 암시적 변환을 정의합니다.
        /// </summary>
        public static implicit operator InfinityValue(long value) => new InfinityValue(value);
        /// <summary>
        /// float에서 InfinityValue로의 암시적 변환을 정의합니다.
        /// </summary>
        public static implicit operator InfinityValue(float value) => new InfinityValue(value);
        /// <summary>
        /// string에서 InfinityValue로의 암시적 변환을 정의합니다.
        /// </summary>
        public static implicit operator InfinityValue(string value) => new InfinityValue(value);
        /// <summary>
        /// BigInteger에서 InfinityValue로의 암시적 변환을 정의합니다.
        /// </summary>
        public static implicit operator InfinityValue(BigInteger value)
        {
            var result = new InfinityValue();
            
            int index = 0;
            while (value > 0)
            {
                var rem = value % 1000;
                if (rem > 0)
                {
                    result.AddUnit(index, (long)rem);
                }
                value /= 1000;
                index++;
            }
            result.Normalize();
            return result;
        }
    }
}