
using System;
using System.Collections;
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
        public int lastFace { get => ent.props.GetProp<int>("LastFace"); set => ent.props.SetProp("LastFace", value); }
        public bool turnEvent;


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
            lastFace = rb.transform.localScale.x > 0 ? 1 : -1;
            ent.inputs.AddActionListener(ent.inputs.InputActions.GamePlay.Move, (Action<Vector2>)((dir) => {
            }));
            ////注册转向事件
            //var runTurnEvent = ent.props.GetProp<Action<int>>("RunTurnEvent");
            //runTurnEvent += (nextFace) => {
            //    if (IsGround) turn = nextFace;
            //};
            //ent.props.SetProp("RunTurnEvent", runTurnEvent);
        }

        Coroutine turnTask;
        private IEnumerator TurnTask(int face)
        {
            ent.fsm.FDebug("移动Turn任务1: 播放转向动画...");
            ent.animCtrls.PlayAnim(TurnAnimName);
            //ent.animCtrls.anims.CrossFade(AnimName, 0.14f, 0, 0.913f /*moveFrameCache*/);
            while (!ent.animCtrls.IsCurrentAnimEnd(TurnAnimName))
                yield return new WaitForFixedUpdate();
            ent.fsm.FDebug("移动Turn任务1: 播放转向动画结束.");

            ent.fsm.FDebug("移动Turn任务2: 播放移动动画");
            ent.animCtrls.anims.Play(AnimName, 0, 0 /*moveFrameCache*/);
            lastFace = face;
            turnEvent = false;
            ent.props.GetProp<Action<int>>("TurnAction")?.Invoke(face);
        }

        public override void OnEnter()
        {
            ent.animCtrls.anims.CrossFade(AnimName, 0.14f, 0, 0.8f /*moveFrameCache*/);
        }

        public override void OnExit()
        {
            if (turnTask != null) ent.StopCoroutine(turnTask);
            turnPhase = 0;
            turn = 0;
            CanExit = false;
            moveFrameCache = ent.animCtrls.CAnimNormalizedTime;
        }

        float lastmoveDir;
        public override void Run()
        {
            //if (MoveDir.x != 0) {
            //    if (turnTask != null) ent.StopCoroutine(turnTask);
            //    var nextFace = (int)MoveDir.x;
            //    if (!turnEvent && lastFace != nextFace) {
            //        turnEvent = true;
            //        turnTask = ent.StartCoroutine(TurnTask(nextFace));
            //    } else {
            //        turnEvent = false;
            //        if (!ent.animCtrls.CAnimTranslationing && !ent.animCtrls.IsAnimName(AnimName)) ent.animCtrls.anims.CrossFade(AnimName, 0.14f, 0, 0.913f /*moveFrameCache*/);
            //    }
            //}

            //Debug.Log($"TurnEvent:{turnEvent}, 同向: {(int)MoveDir.x == rb.transform.localScale.x}");
            //if (!turnEvent && (int)MoveDir.x == rb.transform.localScale.x) {
                ent.props.GetProp<Action<Vector2, float>>("MoveAction")?.Invoke(MoveDir, 1);
            //}

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