#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using Debug = UnityEngine.Debug;
using System.Diagnostics;
using System;
using GoldSprite.UnityPlugins.GUtils;

namespace GoldSprite.UnityPlugins.SerializationData {
    [ExecuteAlways]
    public class TestObjectSerialization : MonoBehaviour {
        [SerializedObject2]
        public MyObject2 MyObj;
        [SerializedObject2]
        public MyObject2 MyObj2;
        [SerializedObject2]
        public MyObject2 MyObj3;
        [SerializedObject2]
        public MyObject2 MyObj4;


        public void OnEnable()
        {
            //Init();
        }

        public void Init()
        {
            UnityEngine.Debug.Log(new StackTrace().GetFrame(1).GetMethod().Name);

            //MyObj.Value = new Vector3();
            //MyObj2.Value = Quaternion.identity;
            MyObj3.Value = "youDadi";
            //MyObj4.Value = Color.yellow;
        }
    }


    [Serializable]
    public class MyObject2 {
        public object Value;
    }


#if UNITY_EDITOR
    public class SerializedObject2Attribute : PropertyAttribute { }
    [CustomPropertyDrawer(typeof(MyObject2))]
    public class MyObjectDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var target = property.serializedObject.targetObject;
            var val = (MyObject2)fieldInfo.GetValue(target);
            //Debug.Log("Draw object: "+val.GetHashCode());

            CustomDrawerHelper.DrawGeneric(position, ref val.Value, property.displayName);
        }
    }
#endif


}