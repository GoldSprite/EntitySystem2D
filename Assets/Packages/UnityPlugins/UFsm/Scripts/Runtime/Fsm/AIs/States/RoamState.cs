using GoldSprite.GUtils;
using GoldSprite.UnityPlugins.PhysicsManager;
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
        public Vector2 RoamVelMinMax = new(0.2f, 1f);
        public float reverseProbability = 0.6f;
        public float LeftDirProbability = 0.5f;
        private float Ticker, TickerInterval = 2;
        [ShowInInspector]
        public float TickerNormalizee => (Ticker - seconds) / TickerInterval;
        private float seconds;

        [ShowInInspector]
        public Vector2 Direction { get => Fsm.ctrlFsm.Props.Direction; set => Fsm.ctrlFsm.Props.Direction = value; }
        public float CenterDirX => CenterDistanceX > 0 ? 1 : -1;
        public float CenterDistanceX => Fsm.Props.RoamArea.center.x - Fsm.Props.BodyCollider.bounds.center.x;

        public Collider2D ground;
        private Vector3 headPoint;

        public RoamState(AIFsm fsm) : base(fsm)
        {
            CanTranSelf = false;
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
            Ticker = Time.time + TickerInterval;

            if (IsOutOfRoamArea())
                CenterMove();
            else RandomMove();
        }

        public override void Update()
        {
            //边界时朝中心移动
            if (IsOutOfRoamArea() && !IsSameSign(Direction.x, CenterDirX)) {
                LogTool.NLog("RoamStateTest", "碰撞越界.");
                if (IsRan(reverseProbability)) CenterMove();
                else {
                    LogTool.NLog("RoamStateTest", "随机到退出.");
                    ExitRoam = true;
                }
                //碰撞地面体时
            } else
            if (IsCollisionGround()) {
                LogTool.NLog("RoamStateTest", "头部碰撞地面体.");
                if (IsRan(reverseProbability)) {
                    float GroundDistanceX = ground.bounds.center.x - headPoint.x;
                    float GroundDirX = GroundDistanceX > 0 ? 1 : -1;
                    ReverseMove(GroundDirX);
                } else {
                    LogTool.NLog("RoamStateTest", "随机到退出.");
                    ExitRoam = true;
                }
                ground = null;
            } else
                //内部时
                RandomMoveTask();

        }

        private bool IsCollisionGround()
        {
            var bounds = Fsm.Props.BodyCollider.bounds;
            headPoint = bounds.center;
            headPoint.y = bounds.max.y;
            var radius = bounds.size.x;
            ground = Physics2D.OverlapCircle(headPoint, radius, PhysicsManager.GroundMask);
            return ground != null;
        }

        private bool IsSameSign(float a, float b)
        {
            return Math.Sign(a) == Math.Sign(b);
        }

        private void RandomMoveTask()
        {
            seconds = Time.time;
            if (TickerNormalizee <= 0) {
                LogTool.NLog("RoamStateTest", $"执行随机移动任务: ");
                if (IsRan(reverseProbability)) RandomMove();
                else {
                    LogTool.NLog("RoamStateTest", "随机到退出.");
                    ExitRoam = true;
                }
                Ticker = seconds + TickerInterval;
                //LogTool.NLog("RoamStateTest", $"下一次秒数: {Ticker}");
            }
        }

        private bool IsRan(float probability)
        {
            return MathTool.rand.NextDouble() < probability;
        }

        private void CenterMove()
        {
            var dirx = CenterDirX;
            var dir = Direction;
            var vel = IsSameSign(dirx, dir.x) ? Math.Abs(dir.x) : Random.Range(RoamVelMinMax.x, RoamVelMinMax.y);  //同向则延续速度
            LogTool.NLog("RoamStateTest", $"朝中心移动: dirx: {dirx}, vel: {vel}");
            dir.x = dirx * vel;
            Direction = dir;
        }

        private void RandomMove()
        {
            var dirx = Ran(LeftDirProbability);
            var dir = Direction;
            var vel = IsSameSign(dirx, dir.x) ? Math.Abs(dir.x) : Random.Range(RoamVelMinMax.x, RoamVelMinMax.y);  //同向则延续速度
            LogTool.NLog("RoamStateTest", $"随机移动: dirx: {dirx}, vel: {vel}");
            dir.x = dirx * vel;
            Direction = dir;
        }

        private void ReverseMove(float groundDirX)
        {
            var dir = Direction;
            dir.x = -groundDirX * Math.Abs(dir.x);
            LogTool.NLog("RoamStateTest", $"反向移动: ");
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
