using GoldSprite.GUtils;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GoldSprite.UFsm {
    [Serializable]
    public class RoamState : AIState {
        public bool EnterRoam;
        public bool ExitRoam;
        public Vector2 RoamVelMinMax = new Vector2(0.2f, 1f);
        public float reverseProbability = 0.6f;
        public float LeftDirProbability = 0.5f;
        public float Ticker, TickerInterval = 3;
        [ShowInInspector]
        public float TickerNormalize => (Ticker - seconds) / TickerInterval;
        public float seconds;

        public bool IsOutOrCollision { get; private set; }

        [ShowInInspector]
        public Vector2 Direction { get => Fsm.ctrlFsm.Props.Direction; set => Fsm.ctrlFsm.Props.Direction = value; }
        public float CenterDirX => CenterDistanceX > 0 ? 1 : -1;
        public float CenterDistanceX => Fsm.Props.RoamArea.center.x - Fsm.Props.BodyCollider.bounds.center.x;


        public override bool CanTranSelf { get; protected set; } = false;

        public RoamState(AIFsm fsm) : base(fsm)
        {
        }

        public override bool Enter() => EnterRoam;
        public override bool Exit() => ExitRoam;

        public override void OnExit()
        {
            ExitRoam = false;
            var dir = Direction;
            dir.x = 0;
            Direction = dir;
        }

        public override void OnEnter()
        {
            EnterRoam = false;
            Ticker = seconds + TickerInterval;

            if (IsOutOfRoamArea())
                CenterMove();
            else RandomMove();
        }

        public override void Update()
        {
            //边界时
            if (IsOutOfRoamArea() && Math.Sign(Direction.x) != Math.Sign(CenterDirX)) {
                Debug.Log("碰撞越界.");
                if (IsRan(reverseProbability)) CenterMove();
                else {
                    Debug.Log("随机到退出.");
                    ExitRoam = true;
                }
            //内部时
            } else RandomMoveTask();
        }

        private void RandomMoveTask()
        {
            seconds = Time.time;
            if (Ticker < seconds) {
                Debug.Log($"执行随机移动任务: ");
                if(IsRan(reverseProbability)) RandomMove();
                else {
                    Debug.Log("随机到退出.");
                    ExitRoam = true;
                }
                Ticker = seconds + TickerInterval;
                //Debug.Log($"下一次秒数: {Ticker}");
            }
        }

        private bool IsRan(float probability)
        {
            return MathTool.rand.NextDouble() < probability;
        }

        private void CenterMove()
        {
            var dirx = CenterDirX;
            var vel = Random.Range(RoamVelMinMax.x, RoamVelMinMax.y);
            Debug.Log($"朝中心移动: dirx: {dirx}, vel: {vel}");
            var dir = Direction;
            dir.x = dirx * vel;
            Direction = dir;
        }

        private void RandomMove()
        {
            var dirx = Ran(LeftDirProbability);
            var vel = Random.Range(RoamVelMinMax.x, RoamVelMinMax.y);
            Debug.Log($"随机移动: dirx: {dirx}, vel: {vel}");
            var dir = Direction;
            dir.x = dirx * vel;
            Direction = dir;
        }

        private int Ran(float leftDirProbability)
        {
            return MathTool.rand.NextDouble() < leftDirProbability ? -1 : 1;
        }

        public bool IsOutOfRoamArea()
        {
            var collBounds = Fsm.Props.BodyCollider.bounds;
            var rect = Fsm.Props.RoamArea;
            return collBounds.min.x < rect.min.x || collBounds.max.x > rect.max.x || collBounds.min.y < rect.min.y || collBounds.max.y > rect.max.y;
        }
    }
}
