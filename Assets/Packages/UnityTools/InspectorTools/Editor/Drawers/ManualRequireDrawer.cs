#if UNITY_EDITOR

using GoldSprite.GUtils;
using GoldSprite.UnityPlugins.MyAnimator;
using UnityEditor;
using UnityEngine;

namespace GoldSprite.UnityTools.InspectorTools {
    [CustomPropertyDrawer(typeof(ManualRequireAttribute))]
    public class ManualRequireDrawer : PropertyDrawer {
        float height;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            height = 0;
            position.height = InspectorHelper.singleLineMargin;

            EditorGUI.PropertyField(position, property, label);
            position.NextLine(ref height);

            var target = property.serializedObject.targetObject;
            var comp = target as Component;
            var requireType = ReflectionHelper.GetFieldType(target, property.propertyPath);
            if (typeof(Component).IsAssignableFrom(requireType)) {
                if ((property.objectReferenceValue = comp.GetComponent(requireType)) == null) {
                    EditorGUI.HelpBox(position, $"需要[{requireType.Name}]组件", MessageType.Warning);
                    position.NextLine(ref height);
                    if (GUI.Button(position, $"添加[{requireType.Name}]组件")) {
                        property.objectReferenceValue = comp.InsertComponentLater(requireType);
                    }
                    position.NextLine(ref height);
                }
            }

            //var requireType = ;
            //if(property.objectReferenceValue = obj.GetComponent()) {

            //}


            //if ((myanim.anims = myanim.GetComponent<Animator>()) == null) {
            //    EditorGUI.HelpBox(position, "需要Animator组件", MessageType.Warning);
            //    position.NextLine(ref height);
            //    if (GUI.Button(position, "添加Animator组件")) {
            //        myanim.anims = myanim.InsertComponentLater<Animator>();
            //    }
            //    position.NextLine(ref height);
            //}
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return height;
        }
    }
}
#endif