using GoldSprite.UFsm;
using GoldSprite.UnityPlugins.MyInputSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GoldSprite.EntitySystem2D {
    public class EntityInputs : MyUInputManager {
        [Header("Êä³öµ½×´Ì¬Æ÷")]
        [ManualRequire]
        public EntityUFsm22 fsm;


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
            AddActionListener(InputActions.TestPlay.Move, (Action<Vector2>)Move);
            AddActionListener(InputActions.TestPlay.MoveBoost, (Action<bool>)MoveBoost);
            AddActionListener(InputActions.TestPlay.Attack, (Action<bool>)Attack);
            AddActionListener(InputActions.TestPlay.SpecialAttack, (Action<bool>)SpecialAttack);
            AddActionListener(InputActions.TestPlay.Hurt, (Action<bool>)Hurt);
        }

        private void MoveBoost(bool down)
        {
            //if(down) props.
        }

        public void Move(Vector2 dir)
        {
            fsm.Command(BaseFsmCommand.Move, dir);
        }

        public void Attack(bool down)
        {
        }

        public void SpecialAttack(bool down)
        {
        }

        public void Hurt(bool down)
        {
        }
    }
}
