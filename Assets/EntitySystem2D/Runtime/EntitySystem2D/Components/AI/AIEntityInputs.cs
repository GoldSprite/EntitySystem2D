using GoldSprite.UFsm;
using GoldSprite.UnityPlugins.MyInputSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GoldSprite.EntitySystem2D {
    public class AIEntityInputs : AIFsm {
        [Header("Êä³öµ½×´Ì¬Æ÷")]
        [SerializeField]
        private List<BehaviourMeta> behaviourMetaList;
        public AIUProps props;


        public void Start()
        {
            InitMetas(props, behaviourMetaList);
            props.RoamState = GetState<RoamState>();
        }

        public void InitMetas(AIUProps props, List<BehaviourMeta> behaviourMetaList)
        {
            Props = props;

            Cmd = new AIFsmCommandManager();
            InitCommands();

            for (int i = 0; i < behaviourMetaList.Count; i++) {
                var behaviourMeta = behaviourMetaList[i];
                var stateType = behaviourMeta.StateType;
                var obj = Activator.CreateInstance(stateType, new object[] { this });
                BaseState state = obj as BaseState;
                //state.AnimName = behaviourMeta.AnimClip.name;
                if (i == 0) InitState(state);
                else AddStateFix(state, behaviourMeta.Priority);
            }
        }


        private void MoveBoost(bool down)
        {
            ctrlFsm.Command(BaseFsmCommand.MoveBoost, down);
        }

        public void Move(Vector2 dir)
        {
            ctrlFsm.Command(BaseFsmCommand.Move, dir);
        }

        public void Attack(bool down)
        {
            ctrlFsm.Command(BaseFsmCommand.Attack, down);
        }

        public void SpecialAttack(bool down)
        {
        }

        public void Hurt(bool down)
        {
            if (!down) return;
            var attacker = ((IAttacker)ctrlFsm.Props);
            ctrlFsm.Command(BaseFsmCommand.Hurt, attacker);
        }
        public void Death(bool down)
        {
            if (down)
                ctrlFsm.Command(BaseFsmCommand.Death, !ctrlFsm.Props.DeathKey);
        }
        public void Jump(bool down)
        {
            ctrlFsm.Command(BaseFsmCommand.Jump, down);
        }
    }
}
