using GoldSprite.UnityTools.MyDict;
using Newtonsoft.Json;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace GoldSprite.UnityTools.MyDict.Samples {
    public class 演示序列化2 : MonoBehaviour {
        [Header("运行来测试")]
        public MyDict<Country, MyDict<School, MyObj>> 嵌套Dict对象;
        public MyDict<MyObj, MyDict<MyObj, MyObj>> 测试2;
        public MyDict<MyObj, Vector3> 测试3;
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
            嵌套Dict对象 = JsonConvert.DeserializeObject<MyDict<Country, MyDict<School, MyObj>>>(json字符串);
        }


        [Serializable]
        public class MyObj {
            public string Name = "王五";
            public Job job;

            public enum Job {
                上班狗, 战斗员
            }
        }

        public enum Country {
            中国, 外国, 宇宙国
        }
        public enum School {
            北京大学, 黄山中学
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(演示序列化2))]
    public class 演示序列化2Drawer : Editor {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var obj = (演示序列化2)target;
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