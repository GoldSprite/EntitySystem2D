using System.Collections.Generic;
using System;
using GoldSprite.UnityPlugins.GUtils;
using GoldSprite.UnityPlugins.MyInputSystem;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace GoldSprite.UnityPlugins.EntitySystem2D.Tests {
    [Serializable]
    public class PropertyManager {
        [ShowProperty]
        [SerializeField]
        public string draw;
        [HideInInspector]
        public bool foldout;
        public Dictionary<string, object> pairs = new();


        public T GetProp<T>(string key)
        {
            if (!pairs.ContainsKey(key)) return default(T);
            return (T)pairs[key];
        }

        public void AddProp(string key, object val)
        {
            if (!pairs.ContainsKey(key)) pairs.Add(key, val);
        }

        public void SetProp(string key, object val)
        {
            if (pairs.ContainsKey(key)) pairs[key] = val;
        }

        public bool Exists(string key)
        {
            return pairs.ContainsKey(key);
        }

    }


    [AttributeUsage(AttributeTargets.Field, Inherited = true)]
    public class ShowPropertyAttribute : PropertyAttribute { }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ShowPropertyAttribute))]
    public class MyInputManagerDrawer : PropertyDrawer {
        private float height;
        List<KeyValuePair<string, object>> updateList = new();

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            height = 0;

            var serializedObject = property.serializedObject;
            var target = serializedObject.targetObject;
            var props = ReflectionHelper.GetField<PropertyManager>(target);
            if (props == null) {
                return;
            }

            var pairs = ReflectionHelper.GetValue<PropertyManager, Dictionary<string, object>>(props, "pairs");

            //UProps
            if (pairs != null && pairs.Count > 0) {
                position.y += 5f;
                position.height = EditorGUIUtility.singleLineHeight;
                updateList.Clear();

                foreach (var kvp in pairs) {
                    var k = kvp.Key;
                    var v = kvp.Value;
                    object fixValue = null;
                    //EditorGUI.PrefixLabel(position, new GUIContent(k));
                    //NextLine(ref position);
                    //EditorGUI.indentLevel++;
                    EditorGUI.BeginChangeCheck();
                    {
                        if (v is Transform trans) {
                            fixValue = (Transform)EditorGUI.ObjectField(position, k, trans, typeof(Transform), true);
                            NextLine(ref position);
                        } else
                        if (v is Rigidbody2D rb) {
                            fixValue = (Rigidbody2D)EditorGUI.ObjectField(position, k, rb, typeof(Rigidbody2D), true);
                            NextLine(ref position);
                        } else
                        if (v is Vector3 vec3) {
                            fixValue = EditorGUI.Vector3Field(position, k, vec3);
                            NextLine(ref position);
                        } else
                        if (v is string str) {
                            fixValue = EditorGUI.TextField(position, k, str);
                            NextLine(ref position);
                        } else
                        if (v is float fl) {
                            fixValue = EditorGUI.FloatField(position, k, fl);
                            NextLine(ref position);
                        }else
                        if (v is int inte) {
                            fixValue = EditorGUI.IntField(position, k, inte);
                            NextLine(ref position);
                        }
                    }
                    //EditorGUI.indentLevel--;
                    if (EditorGUI.EndChangeCheck()) {
                        updateList.Add(new KeyValuePair<string, object>(k, fixValue));
                    }
                }
                NextLine(ref position);

                if (updateList.Count > 0) {
                    foreach (var pair in updateList) pairs[pair.Key] = pair.Value;
                    //Debug.Log("改变值");
                    EditorUtility.SetDirty(target);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                }
            }
        }

        private void NextLine(ref Rect position)
        {
            var lineMargin = 2f;
            position.y += EditorGUIUtility.singleLineHeight + lineMargin;
            height += EditorGUIUtility.singleLineHeight;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return height;
        }
    }
#endif

}