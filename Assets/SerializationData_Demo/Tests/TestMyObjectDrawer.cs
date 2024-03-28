#if UNITY_EDITOR
using UnityEditor;
#endif

using GoldSprite.UnityPlugins.GUtils;
using System;
using System.Reflection;
using UnityEngine;
using System.Diagnostics;

namespace GoldSprite.UnityPlugins.SerializationData {
    [ExecuteAlways]
    public class TestMyObjectDrawer : MonoBehaviour {

        //#if UNITY_EDITOR
        //    [InitializeOnLoadMethod]
        //    public static void InitializeOnLoadMethod()
        //    {
        //        Init();
        //    }
        //#endif

        public void OnEnable()
        {
            Init();
        }

        public static void Init()
        {
            TestMyObjectDrawer instance = FindObjectOfType<TestMyObjectDrawer>();
            if (instance == null) {
                //Debug.Log("不存在TestMyObjectSerialized, 已返回.");
                return;
            }
            UnityEngine.Debug.Log(new StackTrace().GetFrame(1).GetMethod().Name);

            instance.NewTrans();
        }


        [ContextMenu("New Trans")]
        public void NewTrans()
        {
            myObj3.MyObj2 = transform;
        }

        [SerializedObject]
        public MyObject myObj3;

    }

    [Serializable]
    public class MyObject {

        public object MyObj2;
    }


    public class SerializedObjectAttribute : PropertyAttribute { }


#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(SerializedObjectAttribute))]
    public class SerializedObjectDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //var myobj = property.FindPropertyRelative("MyObj");
            //EditorGUI.PropertyField(position, myobj);

            var target = property.serializedObject.targetObject;
            var instance = ReflectionHelper.GetField<MyObject>(target);
            var field2 = instance.MyObj2;

            try {
                CustomDrawerHelper.DrawGeneric(position, ref field2);
            }
            catch (Exception) {
            }
        }

    }


#endif

}