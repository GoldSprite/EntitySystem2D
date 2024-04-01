using GoldSprite.UnityTools.MyDict;
using Newtonsoft.Json;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace GoldSprite.UnityTools.MyDict.Samples {
    public class 演示序列化 : MonoBehaviour {
        [Header("运行来测试")]
        public MyDict<string, MyDict<string, MyObj>> 嵌套Dict对象;
        [HideInInspector]
        public string json字符串;


        [ContextMenu("序列化")]
        public void 序列化()
        {
            json字符串 = JsonConvert.SerializeObject(嵌套Dict对象, Formatting.Indented);
        }
        [ContextMenu("反序列化")]
        public void 反序列化()
        {
            嵌套Dict对象 = JsonConvert.DeserializeObject<MyDict<string, MyDict<string, MyObj>>>(json字符串);
        }


        [Serializable]
        public class MyObj {
            public string Name = "王五";
            public Job job;

            public enum Job {
                上班狗, 战斗员
            }
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(演示序列化))]
    public class 演示序列化Drawer : Editor {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var obj = (演示序列化)target;
            if (!Application.isPlaying) return;
            var line = obj.json字符串.Split("\n").Length - 1;
            obj.json字符串 = EditorGUILayout.TextArea(obj.json字符串, GUILayout.Height(EditorGUIUtility.singleLineHeight * line));
            if (GUILayout.Button("序列化")) {
                obj.序列化();
            }
            if (GUILayout.Button("反序列化")) {
                obj.反序列化();
            }
        }
    }
#endif
}
