#if UNITY_EDITOR
using GoldSprite.UnityTools.InspectorTools;
using UnityEditor;
using UnityEngine;

namespace GoldSprite.UnityPlugins.MyAnimator {
    [CustomPropertyDrawer(typeof(ShowMyAnimatorAttribute))]
    public class MyAnimatorDrawer : PropertyDrawer {
        float height;
        private float singleLineHeight = EditorGUIUtility.singleLineHeight;
        private float singleLineMargin = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            height = 0;
            position.y += EditorGUIUtility.standardVerticalSpacing;
            position.height = singleLineHeight;
            var myanim = property.serializedObject.targetObject as MyAnimator;

            if ((myanim.anims = myanim.GetComponent<Animator>()) == null) {
                //var warnIcon = EditorGUIUtility.IconContent("console.warnicon.sml").image;
                //var content = new GUIContent("需要Animator组件", warnIcon);
                EditorGUI.HelpBox(position, "需要Animator组件", MessageType.Warning);
                position.NextLine(ref height);
                if (GUI.Button(position, "添加Animator组件")) {
                    myanim.anims = myanim.InsertComponentLater<Animator>();
                }
                position.NextLine(ref height);
            }

            //if(myanim.CAnimEnum != null) {
            //    EditorGUI.EnumPopup(position, myanim.CAnimEnum);
            //    NextLine(ref position);
            //}
            //if(myanim.LastAnimEnum != null) {
            //    EditorGUI.EnumPopup(position, myanim.LastAnimEnum);
            //    NextLine(ref position);
            //}
            //if(myanim.LastAnimEndEnum != null) {
            //    EditorGUI.EnumPopup(position, myanim.LastAnimEndEnum);
            //    NextLine(ref position);
            //}

            ////引用Animator
            //if ((myanim.anims = myanim.GetComponent<Animator>()) == null) {
            //    if (myanim.enabled) myanim.enabled = false;
            //    var warnIcon = EditorGUIUtility.IconContent("console.warnicon.sml").image;
            //    var content = new GUIContent("需要Animator组件", warnIcon);
            //    EditorGUILayout.HelpBox(content);
            //    if (GUILayout.Button("添加Animator组件")) {
            //        myanim.anims = myanim.InsertComponentLater<Animator>();
            //        if (!myanim.enabled) myanim.enabled = true;
            //    }
            //} else {
            //    //EditorGUILayout.ObjectField(new GUIContent("Animator"), myanim.anims, typeof(Animator), true);
            //}
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return height;
        }
    }
}
#endif