using System;
using UnityEngine;

namespace Achieve.InfinityValue
{
    /// <summary>
    /// MonoBehaviour/ScriptableObject 필드에서 InfinityValue를 인스펙터에 노출하기 위한 래퍼입니다.
    /// InfinityValue는 8개의 (int, long) 튜플로 저장되어 유니티 직렬화 시스템이 직접 다룰 수 없으므로,
    /// 이 래퍼는 정밀도 손실 없는 문자열 하나로 값을 직렬화하고 커스텀 PropertyDrawer로 편집을 지원합니다.
    /// 단위 이름은 직렬화 대상이 아니며 항상 <see cref="InfinityValueUnitNames.Default"/>를 사용합니다.
    /// </summary>
    [Serializable]
    public struct SerializableInfinityValue : ISerializationCallbackReceiver, IEquatable<SerializableInfinityValue>
    {
        [SerializeField] private string _exact;

        private InfinityValue _value;

        /// <summary>래핑된 InfinityValue입니다.</summary>
        public InfinityValue Value
        {
            get => _value;
            set => _value = value.WithUnitNames(InfinityValueUnitNames.Default);
        }

        public SerializableInfinityValue(InfinityValue value)
        {
            _exact = null;
            _value = value.WithUnitNames(InfinityValueUnitNames.Default);
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            _exact = _value.ToExactString();
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            _value = InfinityValue.TryParseExact(_exact, InfinityValueUnitNames.Default, out var parsed)
                ? parsed
                : InfinityValue.Zero.WithUnitNames(InfinityValueUnitNames.Default);
        }

        public override string ToString() => _value.ToString();

        public bool Equals(SerializableInfinityValue other) => _value.Equals(other._value);
        public override bool Equals(object obj) => obj is SerializableInfinityValue other && Equals(other);
        public override int GetHashCode() => _value.GetHashCode();

        /// <summary>래핑된 InfinityValue로 암시적 변환합니다.</summary>
        public static implicit operator InfinityValue(SerializableInfinityValue field) => field.Value;
        /// <summary>InfinityValue를 래퍼로 암시적 변환합니다.</summary>
        public static implicit operator SerializableInfinityValue(InfinityValue value) => new SerializableInfinityValue(value);
    }
}
