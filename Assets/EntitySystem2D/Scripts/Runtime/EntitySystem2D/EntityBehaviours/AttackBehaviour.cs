using System;
using UnityEngine;

namespace GoldSprite.UnityPlugins.EntitySystem2D {
    public class AttackBehaviour : EntityBehaviourState {
        public bool AttackKey => ent.inputs.GetValue<bool>(ent.inputs.InputActions.GamePlay.Attack);
        public Vector2 MoveDir => ent.inputs.GetValue<Vector2>(ent.inputs.InputActions.GamePlay.Move);
        public int AttackPhase { get => (int)ent.animCtrls.anims.GetFloat("AttackPhase"); set => ent.animCtrls.anims.SetFloat("AttackPhase", value); }
        public string[] AnimNames;
        public int AnimsPhase = 3;
        public string CurrentAnimName => AnimNames[AttackPhase];
        float attackMoveDrag = 0.75f;  //攻击时移动阻力


        public override bool Enter()
        {
            return AttackKey;
        }
        public override bool Exit()
        {
            return ent.animCtrls.IsCurrentAnimEnd(CurrentAnimName);
        }

        public override void InitState()
        {

            ent.inputs.AddActionListener(ent.inputs.InputActions.GamePlay.Attack, (Action<bool>)((down) => {
                if (down) {
                    if (ent.fsm.currentState != this)
                        ent.fsm.UpdateNextState();
                    else {
                        AttackPhase = (AttackPhase + 1) % AnimsPhase;
                        ent.animCtrls.anims.Play(AnimName, 0, 0);
                    }
                }
            }));
        }


        public override void OnEnter0()
        {
            ent.animCtrls.PlayAnim(AnimName);
        }

        public override void OnExit0()
        {
            AttackPhase = 0;
        }

        public override void Run()
        {
            ent.props.GetProp<Action<Vector2, float>>("MoveAction")?.Invoke(MoveDir, 1 - attackMoveDrag);
            //switch (AttackPhase) {
            //    case 0: 
            //        if (ent.provider.IsCurrentAnimEnd(CurrentAnimName)) {
            //            AttackPhase++;
            //            ent.provider.anims.Play(AnimName, 0, 0);
            //        }
            //        break;
            //    case 1:
            //        if (ent.provider.IsCurrentAnimEnd(CurrentAnimName)) {
            //            AttackPhase++;
            //            ent.provider.anims.Play(AnimName, 0, 0);
            //        }
            //        break;
            //    case 2:
            //        break;
            //}
        }
    }
}