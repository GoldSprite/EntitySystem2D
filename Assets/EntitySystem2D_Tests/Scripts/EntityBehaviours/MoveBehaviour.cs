
using System;
using System.Data;
using UnityEngine;

namespace GoldSprite.UnityPlugins.EntitySystem2D.Tests {
    public class MoveBehaviour : EntityBehaviourState {
        public Rigidbody2D rb;
        public float MoveSpeed => ent.props.GetProp<float>("MoveSpeed");
        public Vector2 MoveDir => ent.inputs.GetValue<Vector2>(ent.inputs.InputActions.GamePlay.Move);
        public float moveFrameCache;


        public override bool Enter()
        {
            return MoveDir.x != 0 && !ent.animCtrls.CAnimTranslationing;
        }
        public override bool Exit()
        {
            return MoveDir.x == 0 && !ent.animCtrls.CAnimTranslationing;
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
            ent.animCtrls.anims.CrossFade(AnimName, 0.14f, 0, 0.913f);
        }

        public override void OnExit()
        {
            moveFrameCache = ent.animCtrls.CAnimNormalizedTime;
        }

        public override void Run()
        {
            //if (!ent.animCtrls.IsAnimName(AnimName)) {
            //    //防止bug, 所以持续调用播放(一个出现概率极低的移动但idle动画)///改为判定动画是否成功切换
            //    ent.animCtrls.anims.CrossFade(AnimName, 0.3f, 0, moveFrameCache);
            //}

            ent.props.GetProp<Action<Vector2, float>>("MoveAction")?.Invoke(MoveDir, 1);
        }
    }
}