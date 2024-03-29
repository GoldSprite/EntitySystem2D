using GoldSprite.UnityPlugins.MyInputSystem;
using System.Collections.Generic;
using System;
using UnityEngine.InputSystem;

namespace GoldSprite.UnityPlugins.EntitySystem2D {

    [Serializable]
    public class InputProvider : MyInputManager {

        protected override Dictionary<InputActionMap, bool> SetInputActionMaps()
        {
            var maps = new Dictionary<InputActionMap, bool>() {
                { InputActions.GamePlay, true },
                { InputActions.UIPlay, true }
            };
            return maps;
        }
    }
}