using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoldSprite.UnityPlugins.MyInputSystem;
using UnityEngine.InputSystem;

namespace GoldSprite.UnityPlugins.EntitySystem2D {
    public class MyRoleInputs : MyInputManager {

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
            RegisterActionListener(InputActions.GamePlay.Move, (Action<Vector2>)Move);
            RegisterActionListener(InputActions.GamePlay.MoveBoost, (Action<bool>)MoveBoost);
            RegisterActionListener(InputActions.GamePlay.Attack, (Action<bool>)Attack);
            RegisterActionListener(InputActions.GamePlay.Jump, (Action<bool>)Jump);
            //RegisterActionListener(InputActions.GamePlay.SpecialAttack, (Action<bool>)SpecialAttack);
        }

        private void Jump(bool obj)
        {
        }

        private void Attack(bool obj)
        {
        }

        private void MoveBoost(bool obj)
        {
        }

        private void Move(Vector2 vector)
        {
        }
    }

}