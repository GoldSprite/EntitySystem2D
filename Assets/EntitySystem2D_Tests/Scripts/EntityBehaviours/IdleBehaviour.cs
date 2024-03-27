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
        public Vector2 MoveDir;


        //这个enter其实可以省略(一般作为defaultState在其他状态OnExit之后都会自动变为idle)
        public override bool Enter()
        {
            return MoveDir == Vector2.zero;
        }
        public override bool Exit()
        {
            return MoveDir != Vector2.zero;
        }


        public override void InitState()
        {
            rb = ent.props.GetProp<Rigidbody2D>("Rb");
            ent.inputs.RegisterActionListener(ent.inputs.InputActions.GamePlay.Move, (Action<Vector2>)IdleKey);
        }

        public void IdleKey(Vector2 dir)
        {
            MoveDir = dir;
            //key up
            if(dir.x == 0) {
                ent.fsm.UpdateNextState();
            }
        }

        public void Idle()
        {
            ent.fsm.FDebug("执行停下.");
            var vel = rb.velocity;
            vel.x = 0;
            rb.velocity = vel;
        }

        public override void OnEnter()
        {
            Idle();
        }
    }


}
