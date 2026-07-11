using Achieve.InfinityValue;
using UnityEditor;
using UnityEngine;

namespace Achieve.InfinityValue.Editor
{
    /// <summary>
    /// <see cref="SerializableInfinityValue"/> 필드를 "5.30B"와 같은 컴팩트 표기로
    /// 인스펙터에서 직접 읽고 편집할 수 있게 해주는 PropertyDrawer입니다.
    /// </summary>
    [CustomPropertyDrawer(typeof(SerializableInfinityValue))]
    public sealed class SerializableInfinityValueDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var exactProp = property.FindPropertyRelative("_exact");

            EditorGUI.BeginProperty(position, label, property);

            InfinityValue.TryParseExact(exactProp.stringValue, InfinityValueUnitNames.Default, out var current);
            string display = current.ToString();

            EditorGUI.BeginChangeCheck();
            string edited = EditorGUI.DelayedTextField(position, label, display);
            if (EditorGUI.EndChangeCheck())
            {
                if (InfinityValue.TryParse(edited, InfinityValueUnitNames.Default, out var parsed))
                    exactProp.stringValue = parsed.ToExactString();
                else
                    Debug.LogWarning($"Infinity Value: could not parse \"{edited}\".");
            }

            EditorGUI.EndProperty();
        }
    }
}
