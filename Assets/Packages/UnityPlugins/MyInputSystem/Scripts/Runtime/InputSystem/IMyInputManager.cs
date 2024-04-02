using System;
using UnityEngine.InputSystem;

namespace GoldSprite.UnityPlugins.MyInputSystem {
    public interface IMyInputManager {
        public InputActions InputActions { get; }
        public T GetValue<T>(InputAction keyAction);
        public void AddActionListener<T>(InputAction keyAction, Action<T> act, bool log = false);
    }
}