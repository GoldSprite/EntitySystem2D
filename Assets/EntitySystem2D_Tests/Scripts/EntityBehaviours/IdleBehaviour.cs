using GoldSprite.UnityPlugins.MyInputSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GoldSprite.UnityPlugins.EntitySystem2D.Tests {
    public class IdleBehaviour : EntityBehaviourState {
        public Rigidbody2D rb;
        private float IdleFrameCache;

        public Vector2 MoveDir => ent.inputs.GetValue<Vector2>(ent.inputs.InputActions.GamePlay.Move);


        //这个enter其实可以省略(一般作为defaultState在其他状态OnExit之后都会自动变为idle)
        public override bool Enter()
        {
            return MoveDir.x == 0 && !ent.animCtrls.CAnimTranslationing;
        }
        public override bool Exit()
        {
            return MoveDir.x != 0 && !ent.animCtrls.CAnimTranslationing;
        }


        public override void InitState()
        {
            rb = ent.props.GetProp<Rigidbody2D>("Rb");
            ent.inputs.AddActionListener(ent.inputs.InputActions.GamePlay.Move, (Action<Vector2>)((dir) => {
                if (dir.x == 0) {
                    ent.fsm.UpdateNextState();
                }
            }));
        }

        public override void OnEnter()
        {
            ent.fsm.FDebug("执行停下.");
            var vel = rb.velocity;
            vel.x = 0;
            rb.velocity = vel;

            ent.animCtrls.anims.CrossFade(AnimName, 0.3f);
        }

        public override void OnExit()
        {
            IdleFrameCache = ent.animCtrls.CAnimNormalizedTime;
        }
    }


}
