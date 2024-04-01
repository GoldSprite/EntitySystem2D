using GoldSprite.UnityPlugins.MyInputSystem;
using System.Collections.Generic;
using System;
using UnityEngine.InputSystem;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

namespace GoldSprite.UnityPlugins.EntitySystem2D {

    [Serializable]
    public class InputProvider : MyInputManager , IEntityProvider {
        public Dictionary<VirtualKey, Delegate> keyActions = new();
        public Dictionary<VirtualKey, object> keyValues = new();

        protected override Dictionary<InputActionMap, bool> SetInputActionMaps()
        {
            var maps = new Dictionary<InputActionMap, bool>() {
                { InputActions.GamePlay, true },
                { InputActions.UIPlay, true }
            };
            return maps;
        }

        protected override void InitActions()
        {
            //var keyTypes = Enum.GetValues(typeof(VirtualKey));
            //foreach (VirtualKey item in keyTypes)
            //{
            //    keyValues.Add(item, null);
            //}
        }

        public T GetValue<T>(VirtualKey key)
        {
            if (keyValues.TryGetValue(key, out object val)) return (T)val;
            return default(T);
        }

        public void RaiseKeyEvent(VirtualKey key, object val)
        {
            if (keyValues.ContainsKey(key)) {
                Debug.Log($"触发[{key}]虚拟键: {val}");
                keyValues[key] = val;
            }
        }

        public bool Init()
        {
            var msgs = new List<string>();
            //if (actions.Count == 0) msgs.Add("按键表为空, 将不会监听任何键.");
            //if (keyValues.Count == 0) msgs.Add("虚拟按键表为空, 将不会监听任何虚拟按键.");
            return IEntityProvider.PrintInitLog(this, msgs);
        }

        public void AddActionListener<T>(VirtualKey virtualKey, Action<T> action)
        {
            if (!keyActions.TryGetValue(virtualKey, out Delegate dele)) {
                keyActions[virtualKey] = (Action<T>)((p) => { if(debugLog) Debug.Log($"虚拟键[{virtualKey}]触发: {p}."); });
                keyValues.Add(virtualKey, default(T));
            } else {
                keyActions[virtualKey] = Delegate.Combine(dele, action);
            }
        }

        public void RaiseAction(VirtualKey virtualKey, object param) {
            if (keyActions.TryGetValue(virtualKey, out Delegate dele)) dele?.DynamicInvoke(param);
        }

        public enum VirtualKey
        {
            HurtKey
        }
    }
}