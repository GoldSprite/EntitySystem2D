using System.Linq;
using UnityEngine;

namespace GoldSprite.UnityTools {
    public static class InspectorHelper {

        public static T InsertComponentLater<T>(this Component mono) where T : Component
        {
            var gameObject = mono.gameObject;
            var above = mono;
            var comp = gameObject.AddComponent<T>();
            var comps = gameObject.GetComponents(typeof(Component)).ToList();
            var aboveIndex = comps.IndexOf(above);
            var compIndex = comps.IndexOf(comp);
            var step = compIndex - aboveIndex - 1;
            for (int i = 0; i < step; i++)
                UnityEditorInternal.ComponentUtility.MoveComponentUp(comp);
            return comp;
        }
    }
}
