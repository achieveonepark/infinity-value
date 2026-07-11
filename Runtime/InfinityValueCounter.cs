using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Achieve.InfinityValue
{
    /// <summary>
    /// InfinityValue를 부드럽게 증가/감소시키는 비동기 카운터입니다.
    /// 애니메이션이 진행되는 도중 여러 곳에서 동시에 <see cref="AddAsync"/>/<see cref="AnimateToAsync"/>를
    /// 호출해도 새 호출은 기존에 진행 중인 애니메이션에 합류할 뿐 새 애니메이션을 따로 시작하지 않으며,
    /// 하나의 흐름으로 그 시점 기준 가장 높은 목표값까지 이어서 올라갑니다.
    /// 별도의 Update 루프 없이 호출부에서는 <c>await counter.AddAsync(reward);</c> 한 줄로 사용할 수 있습니다.
    /// 메인 스레드에서 호출해야 합니다.
    /// </summary>
    public sealed class InfinityValueCounter : IDisposable
    {
        private readonly float _speed;
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        private InfinityValue _current;
        private InfinityValue _target;
        private Task _runningLoop;

        /// <summary>현재 화면에 표시되어야 하는(애니메이션 진행 중인) 값입니다.</summary>
        public InfinityValue Current => _current;

        /// <summary>애니메이션이 최종적으로 수렴할 목표값입니다.</summary>
        public InfinityValue Target => _target;

        /// <summary>애니메이션 진행 중 Current가 갱신될 때마다 새 값과 함께 호출됩니다.</summary>
        public event Action<InfinityValue> ValueChanged;

        /// <param name="initial">초기값입니다.</param>
        /// <param name="speed">초당 남은 거리를 따라잡는 비율입니다. 클수록 목표값에 빠르게 수렴합니다.</param>
        public InfinityValueCounter(InfinityValue initial, float speed = 6f)
        {
            _current = _target = initial;
            _speed = Mathf.Max(0.01f, speed);
        }

        /// <summary>
        /// 목표값에 delta를 더하고, 그 합계까지 부드럽게 올라가는 애니메이션이 끝날 때까지 기다립니다.
        /// 이미 애니메이션이 진행 중이면 새로 시작하지 않고 진행 중인 애니메이션에 합류합니다.
        /// </summary>
        public Task AddAsync(InfinityValue delta) => AnimateToAsync(_target + delta);

        /// <summary>
        /// 목표값을 newTarget으로 갱신하고(현재 목표보다 낮으면 무시), 애니메이션이 그 시점 기준
        /// 최종 목표값에 도달할 때까지 기다립니다.
        /// </summary>
        public Task AnimateToAsync(InfinityValue newTarget)
        {
            if (newTarget > _target)
                _target = newTarget;

            if (_runningLoop == null || _runningLoop.IsCompleted)
                _runningLoop = RunLoopAsync();

            return _runningLoop;
        }

        /// <summary>진행 중인 애니메이션을 건너뛰고 즉시 목표값으로 맞춥니다.</summary>
        public void SnapToTarget()
        {
            if (_current == _target) return;
            _current = _target;
            ValueChanged?.Invoke(_current);
        }

        /// <summary>진행 중인 애니메이션을 취소합니다. 컴포넌트가 파괴될 때 호출하세요.</summary>
        public void Dispose() => _cts.Cancel();

        private async Task RunLoopAsync()
        {
            while (_current != _target)
            {
                _cts.Token.ThrowIfCancellationRequested();

                double t = 1.0 - Math.Exp(-_speed * Time.deltaTime);
                var next = Lerp(_current, _target, t);

                // 값이 커서 한 프레임 증분이 정수 절삭으로 0이 되는 경우에도 진행이 멈추지 않도록 보장합니다.
                if (_target > _current && next <= _current) next = _current + InfinityValue.One;
                else if (_target < _current && next >= _current) next = _current - InfinityValue.One;

                _current = next;
                ValueChanged?.Invoke(_current);

                if (_current == _target) break;
                await Task.Yield();
            }
        }

        private static InfinityValue Lerp(InfinityValue from, InfinityValue to, double t)
        {
            if (t <= 0) return from;
            if (t >= 1) return to;
            return to >= from
                ? from + (to - from) * t
                : from - (from - to) * t;
        }
    }
}
