using System;

namespace Achieve.InfinityValue
{
    public partial struct InfinityValue
    {
        /// <summary>
        /// 현재 값의 상용로그(log10)를 근사적으로 반환합니다.
        /// 8개 단위 슬롯 범위를 넘어서는 값도 오버플로 없이 계산합니다.
        /// </summary>
        public double Log10()
        {
            if (_unitCount == 0) return double.NegativeInfinity;

            var highest = GetHighestUnit();
            double mantissa = highest.Item2 + GetValueForIndex(highest.Item1 - 1) / 1000.0;
            return Math.Log10(mantissa) + highest.Item1 * 3.0;
        }

        /// <summary>
        /// 현재 값을 지정된 지수만큼 거듭제곱합니다. 로그 공간에서 계산하므로
        /// double 변환을 거치지 않고도 매우 큰 결과를 근사할 수 있습니다.
        /// 업그레이드 비용 곡선(baseCost * growthRate^level) 계산에 유용합니다.
        /// </summary>
        public InfinityValue Pow(double exponent)
        {
            var unitNames = UnitNames;
            if (IsEmpty) return (exponent == 0 ? One : Zero).WithUnitNames(unitNames);
            if (exponent == 0) return One.WithUnitNames(unitNames);
            if (exponent == 1) return this;

            return FromLog10(Log10() * exponent, unitNames);
        }

        /// <summary>현재 값의 제곱근을 반환합니다. <c>Pow(0.5)</c>의 축약형입니다.</summary>
        public InfinityValue Sqrt() => Pow(0.5);

        /// <summary>
        /// 등비수열 비용 곡선(n번째 아이템 가격 = baseCost * growthRate^n)에서
        /// balance로 최대 몇 개를 구매할 수 있는지 계산합니다. "최대 구매" 버튼 구현에 사용합니다.
        /// </summary>
        /// <param name="balance">보유 재화입니다.</param>
        /// <param name="baseCost">0번째 아이템의 가격입니다. 0보다 커야 합니다.</param>
        /// <param name="growthRate">구매할 때마다 가격에 곱해지는 배율입니다. 1보다 커야 합니다.</param>
        /// <returns>balance로 구매 가능한 최대 아이템 개수입니다.</returns>
        public static long AffordableCount(InfinityValue balance, InfinityValue baseCost, double growthRate)
        {
            if (growthRate <= 1.0)
                throw new ArgumentOutOfRangeException(nameof(growthRate), "growthRate must be greater than 1.");
            if (baseCost.IsEmpty)
                throw new ArgumentOutOfRangeException(nameof(baseCost), "baseCost must be greater than 0.");
            if (balance < baseCost) return 0;

            double logRatio = balance.Log10() - baseCost.Log10();
            double logGrowth = Math.Log10(growthRate);
            double logStep = Math.Log10(growthRate - 1.0);

            // 구매 개수 k는 growthRate^k <= 1 + (balance/baseCost) * (growthRate - 1)를 만족하는 최대값입니다.
            // logRatio가 크면 10^logRatio를 직접 계산하지 않고 로그끼리 더해 오버플로를 피합니다.
            double logNumerator = logRatio > 6.0
                ? logRatio + logStep
                : Math.Log10(1.0 + Math.Pow(10, logRatio) * (growthRate - 1.0));

            double count = logNumerator / logGrowth;
            if (double.IsNaN(count) || count <= 0) return 0;
            if (double.IsPositiveInfinity(count) || count >= long.MaxValue) return long.MaxValue;
            return (long)count;
        }

        private static InfinityValue FromLog10(double log10Value, InfinityValueUnitNames unitNames)
        {
            if (double.IsNaN(log10Value) || log10Value < 0) return Zero.WithUnitNames(unitNames);
            if (log10Value == 0) return One.WithUnitNames(unitNames);
            if (double.IsPositiveInfinity(log10Value))
                throw new OverflowException("Pow result exceeds the representable range.");

            int unitIndex = (int)Math.Floor(log10Value / 3.0);
            double remainder = log10Value - unitIndex * 3.0;
            double scaled = Math.Pow(10, remainder);

            long topValue = Math.Min((long)scaled, 999);
            long lowerValue = (long)((scaled - topValue) * 1000.0);

            var result = default(InfinityValue).WithUnitNames(unitNames);
            if (topValue > 0) result.AddUnit(unitIndex, topValue);
            if (lowerValue > 0 && unitIndex > 0) result.AddUnit(unitIndex - 1, lowerValue);

            result.Normalize();
            return result;
        }
    }
}
