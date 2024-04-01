using GoldSprite.UnityTools.MyDict;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace GoldSprite.UnityTools.MyDict {
    // 自定义绘制器
    [CustomPropertyDrawer(typeof(MyKVPair<,>))]
    public class MyKVPairDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // 计算每个元素的标题
            SerializedProperty keyProperty = property.FindPropertyRelative("Key");
            SerializedProperty valProperty = property.FindPropertyRelative("Value");

            EditorGUI.BeginChangeCheck();

            position.height = EditorGUIUtility.singleLineHeight;
            var keyWidth = position.width / 5f;
            var marginRight = position.width / 20f;
            var keyRect = new Rect(position.x, position.y, keyWidth, position.height);
            var valRect = new Rect(position.x + keyWidth + marginRight, position.y, position.width - keyWidth - marginRight, position.height);
            EditorGUI.PropertyField(keyRect, keyProperty, GUIContent.none, true);
            EditorGUI.PropertyField(valRect, valProperty, GUIContent.none, true);

            if (EditorGUI.EndChangeCheck()) {
                EditorUtility.SetDirty(property.serializedObject.targetObject);
                AssetDatabase.Refresh();
                AssetDatabase.SaveAssets();
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            SerializedProperty keyProperty = property.FindPropertyRelative("Key");
            float keyHeight = EditorGUI.GetPropertyHeight(keyProperty, true);
            SerializedProperty valProperty = property.FindPropertyRelative("Value");
            float valueHeight = EditorGUI.GetPropertyHeight(valProperty, true);

            // 返回字段 "Value" 高度加上默认间距
            return (valueHeight > keyHeight ? valueHeight : keyHeight) + EditorGUIUtility.standardVerticalSpacing + EditorGUIUtility.singleLineHeight * 0;
        }
    }

    // 自定义绘制器
    [CustomPropertyDrawer(typeof(MyDict<,>))]
    public class MyDictDrawer : PropertyDrawer {
        private float height;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var pairsProp = property.FindPropertyRelative("pairs");
            EditorGUI.PropertyField(position, pairsProp, new GUIContent(label.text + " - Pairs"));
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var pairsProp = property.FindPropertyRelative("pairs");
            return EditorGUI.GetPropertyHeight(pairsProp, label) + EditorGUIUtility.standardVerticalSpacing;
        }
    }
}
