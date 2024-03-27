
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
            return MoveDir.x != 0;
        }
        public override bool Exit()
        {
            return MoveDir.x == 0;
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
        }

        public override void Run()
        {
            ent.animCtrls.PlayAnim(AnimName);  //防止bug, 所以持续调用播放(一个出现概率极低的移动但idle动画)

            Move();

            TurnFace();
        }

        public void TurnFace()
        {   //转向
            var face = rb.transform.localScale;
            face.x = MoveDir.x > 0 ? 1 : -1;
            rb.transform.localScale = face;
        }

        public void Move()
        {
            ent.fsm.FDebug("执行移动.");
            var vel = rb.velocity;
            var velxNormalized = MoveDir.x > 0 ? 1 : -1;
            var velx = velxNormalized * MoveSpeed;
            vel.x = velx;
            rb.velocity = vel;
        }
    }
}