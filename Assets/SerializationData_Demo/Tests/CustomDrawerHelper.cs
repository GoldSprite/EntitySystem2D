using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace GoldSprite.UnityPlugins.SerializationData {
    public static class CustomDrawerHelper {

        public static void DrawGeneric(Rect position, ref object instance, string label="")
        {
            var icon1 = EditorGUIUtility.IconContent("console.warnicon");
            if (instance == null) {
                EditorGUI.TextField(position, icon1, "*null object");
                return;
            }
            Type objectType = instance.GetType();
            var style = GUI.skin.textField;
            //style.margin = new RectOffset(5, 5, 5, 5);
            style.alignment = TextAnchor.MiddleLeft;


            if (instance is UnityEngine.Object uObj) {
                instance = EditorGUI.ObjectField(position, label, uObj, uObj.GetType(), true);
                return;
            }

            //手动处理类型
            if (instance is string str) {
                instance = EditorGUI.TextField(position, label, str);
                return;
            }

            Type editorGUIType = typeof(EditorGUI);
            MethodInfo[] methods = editorGUIType.GetMethods(BindingFlags.Public | BindingFlags.Static);
            foreach (MethodInfo method in methods) {
                // 确保方法有3个参数
                if (method.GetParameters().Length == 3) {
                    // 确保第一个参数类型为Rect
                    ParameterInfo[] parameters = method.GetParameters();
                    if (parameters[0].ParameterType == typeof(Rect)) {
                        // 确保第二个参数类型为string
                        if (parameters[1].ParameterType == typeof(string)) {
                            // 确保第三个参数类型为指定的判定类型或其子类
                            Type judgeType = parameters[2].ParameterType;
                            if (judgeType == objectType || objectType.IsAssignableFrom(judgeType)) {
                                // 如果条件都满足，则调用这个方法
                                object[] methodParams = new object[] { position, label, instance };
                                instance = method.Invoke(null, methodParams);
                                return;
                            }
                        }
                    }
                }
            }
            EditorGUI.TextField(position, icon1,  "找不到任何符合该类型的绘制方式.");
        }
    }
}