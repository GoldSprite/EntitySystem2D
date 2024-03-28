#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using Debug = UnityEngine.Debug;
using System.Diagnostics;
using System;
using GoldSprite.UnityPlugins.GUtils;
using System.Runtime.Serialization;
using UnityEditor.Build.Content;
using Assets.SerializationData_Demo.Tests;

namespace GoldSprite.UnityPlugins.SerializationData {
    [ExecuteAlways]
    public class TestObjectSerialization : MonoBehaviour {
        public MyObject2 MyObj;
        public MyObject2 MyObj2;
        public MyObject2 MyObj3;
        public MyObject2 MyObj4;

        public MyObject4 OutMyObj;


        public void OnEnable()
        {
            //Init();
        }

        [ContextMenu("Init")]
        public void Init()
        {
            MyObj.Value = 666f;
            MyObj2.Value = transform;
            MyObj3.Value = "youDadi";
            MyObj4.Value = Color.yellow;

            try {
                UnityEngine.Debug.Log(new StackTrace().GetFrame(1).GetMethod().Name);
            }
            catch (Exception) {
                UnityEngine.Debug.Log(new StackTrace().GetFrame(0).GetMethod().Name);
            }
        }



    }


#if UNITY_EDITOR
    //public class SerializedObject2Attribute : PropertyAttribute { }
    [CustomPropertyDrawer(typeof(MyObject2))]
    public class MyObjectDrawer : PropertyDrawer {
        float height;
        static float viewSingleHeight = EditorGUIUtility.singleLineHeight;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            height = 0;
            position.height = viewSingleHeight;
            var target = property.serializedObject.targetObject;
            var val = (MyObject2)fieldInfo.GetValue(target);
            //Debug.Log("Draw object: "+val.GetHashCode());

            //EditorGUI.PropertyField(position, property.FindPropertyRelative("SampleVal"), label);
            //position.y += viewSingleHeight;
            //height += viewSingleHeight;

            EditorGUI.BeginChangeCheck();
            CustomDrawerHelper.DrawGeneric(position, ref val.Value, property.displayName);
            if (EditorGUI.EndChangeCheck()) {
                Debug.Log("数据已更新.");
                EditorUtility.SetDirty(target);
                AssetDatabase.Refresh();
            }
            position.y += viewSingleHeight;
            height += viewSingleHeight;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return height;
        }
    }
#endif


}