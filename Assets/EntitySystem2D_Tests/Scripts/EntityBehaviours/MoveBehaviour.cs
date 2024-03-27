
using System;
using System.Data;
using UnityEngine;

namespace GoldSprite.UnityPlugins.EntitySystem2D.Tests {
    public class MoveBehaviour : EntityBehaviourState {
        public Rigidbody2D rb;
        public float MoveSpeed => ent.props.GetProp<float>("MoveSpeed");
        public Vector2 MoveDir;


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
            ent.inputs.RegisterActionListener(ent.inputs.InputActions.GamePlay.Move, (Action<Vector2>)MoveKey);
        }

        public void MoveKey(Vector2 dir)
        {
            MoveDir = dir;
            //key down
            if (dir.x != 0) {
                ent.fsm.UpdateNextState();
            }
        }

        public void Move()
        {
            ent.fsm.FDebug("执行移动.");
            var velx = MoveDir.x * MoveSpeed;
            var vel = rb.velocity;
            vel.x = velx;
            rb.velocity = vel;

            //转向
            var face = rb.transform.localScale;
            face.x = vel.x > 0 ? 1 : -1;
            rb.transform.localScale = face;
        }

        public override void OnEnter()
        {
            Move();
        }
    }
}