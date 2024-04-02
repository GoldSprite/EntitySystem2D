#if UNITY_EDITOR
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace GoldSprite.UnityTools.InspectorTools {
    public static class InspectorHelper {
        public static float singleLineMargin = EditorGUIUtility.singleLineHeight;
        public static float singleLineSpacing = EditorGUIUtility.standardVerticalSpacing;


        public static Component InsertComponentLater(this Component mono, Type compType)
        {
            var gameObject = mono.gameObject;
            var above = mono;
            var comp = gameObject.AddComponent(compType);
            var comps = gameObject.GetComponents(typeof(Component)).ToList();
            var aboveIndex = comps.IndexOf(above);
            var compIndex = comps.IndexOf(comp);
            var step = compIndex - aboveIndex - 1;
            for (int i = 0; i < step; i++)
                UnityEditorInternal.ComponentUtility.MoveComponentUp(comp);
            return comp;
        }
        public static T InsertComponentLater<T>(this Component mono) where T : Component
        {
            return (T)mono.InsertComponentLater(typeof(T));
        }


        public static void NextLine(this ref Rect position, ref float height)
        {
            position.y += singleLineMargin;
            height += singleLineMargin;
        }
    }
}
#endif
