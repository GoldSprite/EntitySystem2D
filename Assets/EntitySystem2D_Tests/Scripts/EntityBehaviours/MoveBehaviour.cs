
using System;
using System.Data;
using UnityEngine;

namespace GoldSprite.UnityPlugins.EntitySystem2D.Tests {
    public class MoveBehaviour : EntityBehaviourState {
        public Rigidbody2D rb;
        public float MoveSpeed => ent.props.GetProp<float>("MoveSpeed");
        public Vector2 MoveDir => ent.inputs.GetValue<Vector2>(ent.inputs.InputActions.GamePlay.Move);


        public override bool Enter()
        {
            return MoveDir != Vector2.zero;
        }
        public override bool Exit()
        {
            return MoveDir == Vector2.zero;
        }


        public override void InitState()
        {
            rb = ent.props.GetProp<Rigidbody2D>("Rb");
            ent.inputs.AddActionListener(ent.inputs.InputActions.GamePlay.Move, (Action<Vector2>)((dir) => {
                if (dir.x != 0) {
                    ent.fsm.UpdateNextState();
                }
            }));
        }

        public override void OnEnter()
        {
            //转向
            var face = rb.transform.localScale;
            face.x = MoveDir.x > 0 ? 1 : -1;
            rb.transform.localScale = face;
        }

        public override void Run()
        {
            ent.fsm.FDebug("执行移动.");
            var velx = MoveDir.x * MoveSpeed;
            var vel = rb.velocity;
            vel.x = velx;
            rb.velocity = vel;
        }
    }
}