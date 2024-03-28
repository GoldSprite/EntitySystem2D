
using System;
using System.Data;
using UnityEngine;

namespace GoldSprite.UnityPlugins.EntitySystem2D.Tests {
    public class MoveBehaviour : EntityBehaviourState {
        public Rigidbody2D rb;
        public bool IsGround => ent.physics.IsGround;
        public float MoveSpeed => ent.props.GetProp<float>("MoveSpeed");
        public Vector2 MoveDir => ent.inputs.GetValue<Vector2>(ent.inputs.InputActions.GamePlay.Move);
        public float moveFrameCache;
        public bool CanExit;
        public float keepTicker, keepDuration = 0.2f;
        public string TurnAnimName;
        public int turn = 0;
        public int turnPhase;


        public override bool Enter()
        {
            return IsGround && MoveDir.x != 0/* && !ent.animCtrls.CAnimTranslationing*/;
        }
        public override bool Exit()
        {
            return CanExit /*&& !ent.animCtrls.CAnimTranslationing*/;
        }


        public override void InitState()
        {
            rb = ent.props.GetProp<Rigidbody2D>("Rb");
            ent.inputs.AddActionListener(ent.inputs.InputActions.GamePlay.Move, (Action<Vector2>)((dir) => {
                if (dir.x != 0) {
                    ent.fsm.UpdateNextState();
                }
            }));
            //注册转向事件
            var runTurnEvent = ent.props.GetProp<Action<int>>("RunTurnEvent");
            runTurnEvent += (face) => {
                if (IsGround) turn = face;
            };
            ent.props.SetProp("RunTurnEvent", runTurnEvent);
        }

        public override void OnEnter()
        {
            ent.animCtrls.anims.CrossFade(AnimName, 0.14f, 0, 0.913f /*moveFrameCache*/);
        }

        public override void OnExit()
        {
            turnPhase = 0;
            turn = 0;
            CanExit = false;
            moveFrameCache = ent.animCtrls.CAnimNormalizedTime;
        }

        float lastmoveDir;
        public override void Run()
        {
            //if (!ent.animCtrls.IsAnimName(AnimName)) {
            //    //防止bug, 所以持续调用播放(一个出现概率极低的移动但idle动画)///改为判定动画是否成功切换
            //    ent.animCtrls.anims.CrossFade(AnimName, 0.3f, 0, moveFrameCache);
            //}
            if (turn != 0) {
                if (!ent.animCtrls.IsAnimName(TurnAnimName)) {
                    ent.animCtrls.PlayAnim(TurnAnimName);
                } else
                if (ent.animCtrls.IsCurrentAnimEnd(TurnAnimName)) {
                    ent.props.GetProp<Action<int>>("TurnAction")?.Invoke(turn);
                    //ent.animCtrls.anims.CrossFade(AnimName, 0.14f, 0, 0.913f /*moveFrameCache*/);
                    ent.animCtrls.anims.Play(AnimName, 0, 0.913f /*moveFrameCache*/);
                    turn = 0;
                }
            } else {
            }
            if (turn == 0 && ent.animCtrls.IsAnimName(AnimName)) {
                ent.props.GetProp<Action<Vector2, float>>("MoveAction")?.Invoke(MoveDir, 1);
            }

            //移动键粘连计时器
            if (MoveDir.x == 0) {
                if (Time.realtimeSinceStartup > keepTicker) {
                    Debug.Log("移动粘连结束.");
                    CanExit = true;
                }
            } else {
                if (MoveDir.x != lastmoveDir) {
                    Debug.Log("移动粘连刷新.");
                    lastmoveDir = MoveDir.x;
                }
                keepTicker = Time.realtimeSinceStartup + keepDuration;
            }
        }
    }
}