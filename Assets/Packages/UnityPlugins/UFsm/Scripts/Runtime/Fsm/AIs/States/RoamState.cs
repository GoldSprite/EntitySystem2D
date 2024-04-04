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
        private float EnterTicker, EnterTickerInterval = 2;
        [ShowInInspector]
        public float EnterTickerNormalized => (EnterTicker - Time.time) / EnterTickerInterval;
        private float seconds;

        [ShowInInspector]
        public Vector2 Direction { get => Fsm.ctrlFsm.Props.Direction; set => Fsm.ctrlFsm.Props.Direction = value; }
        public float CenterDirX => CenterDistanceX > 0 ? 1 : -1;
        public float CenterDistanceX => Fsm.Props.LandArea.center.x - Fsm.ctrlFsm.Props.BodyCollider.bounds.center.x;

        public int GroundDirX => GroundDistanceX > 0 ? 1 : -1;
        public float GroundDistanceX => ground.bounds.center.x - headPoint.x;

        public Collider2D ground;
        private Vector3 headPoint;

        public RoamState(AIFsm fsm) : base(fsm)
        {
            CanTranSelf = false;
            EnterTicker = Time.time + EnterTickerInterval;
        }

        public override void UpdateCondition()
        {
            if (Fsm.IsDefaultState()) {
                LogTool.NLog("EnterRoamStateTest", $"������ڷ���, �Ͻ��������, ����ʱ--[{EnterTickerNormalized}]");
            } else {
                LogTool.NLog("EnterRoamStateTest", "�����й������æ, ��ʱ������.");
                EnterTicker = Time.time + EnterTickerInterval;
            }
            if(EnterTickerNormalized <= 0) {
                LogTool.NLog("EnterRoamStateTest", "��ʱ����, ������һ�����, ���.");
                EnterRoam = true;
            }
        }


        public override bool Enter() => EnterRoam;
        public override bool Exit() => ExitRoam;

        public override void OnExit()
        {
            LogTool.NLog("RoamStateTest", "�˳�����, �ٶ�����.");
            ExitRoam = false;
            var dir = Direction;
            dir.x = 0;
            Direction = dir;
        }

        public override void OnEnter()
        {
            EnterRoam = false;
            Ticker = Time.time + TickerInterval;

            if (Fsm.collCtrls.IsOutOfLandArea())
                CenterMove();
            else
            if (IsCollisionGround()) {
                ReverseMove(Random.Range(RoamVelMinMax.x, RoamVelMinMax.y));
            } else RandomMove();
        }

        public override void Update()
        {
            //�߽�ʱ�������ƶ�
            if (Fsm.collCtrls.IsOutOfLandArea() && !IsSameSign(Direction.x, CenterDirX)) {
                LogTool.NLog("RoamStateTest", "��ײԽ��.");
                if (IsRan(reverseProbability)) CenterMove();
                else {
                    LogTool.NLog("RoamStateTest", "������˳�.");
                    ExitRoam = true;
                }
            } else
            //��ײ������ʱ�����ƶ�
            if (IsCollisionGround() && IsSameSign(Direction.x, GroundDirX)) {
                LogTool.NLog("RoamStateTest", "ͷ����ײ������.");
                if (IsRan(reverseProbability)) {
                    ReverseMove(GroundDirX);
                } else {
                    LogTool.NLog("RoamStateTest", "������˳�.");
                    ExitRoam = true;
                }
                ground = null;
            } else
                //����ʱ����ƶ�
                RandomMoveTask();

        }

        private bool IsCollisionGround()
        {
            var bounds = Fsm.ctrlFsm.Props.BodyCollider.bounds;
            headPoint = bounds.center;
            headPoint.y = bounds.max.y;
            var radius = bounds.size.x / 2f * 1.1f;
            ground = Physics2D.OverlapCircle(headPoint, radius, GroundDetection.GroundMask);
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
                LogTool.NLog("RoamStateTest", $"ִ������ƶ�����: ");
                if (IsRan(reverseProbability)) RandomMove();
                else {
                    LogTool.NLog("RoamStateTest", "������˳�.");
                    ExitRoam = true;
                }
                Ticker = seconds + TickerInterval;
                //LogTool.NLog("RoamStateTest", $"��һ������: {Ticker}");
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
            var vel = IsSameSign(dirx, dir.x) ? Math.Abs(dir.x) : Random.Range(RoamVelMinMax.x, RoamVelMinMax.y);  //ͬ���������ٶ�
            LogTool.NLog("RoamStateTest", $"�������ƶ�: dirx: {dirx}, vel: {vel}");
            dir.x = dirx * vel;
            Direction = dir;
        }

        private void RandomMove()
        {
            var dirx = Ran(LeftDirProbability);
            var dir = Direction;
            var vel = IsSameSign(dirx, dir.x) ? Math.Abs(dir.x) : Random.Range(RoamVelMinMax.x, RoamVelMinMax.y);  //ͬ���������ٶ�
            LogTool.NLog("RoamStateTest", $"����ƶ�: dirx: {dirx}, vel: {vel}");
            dir.x = dirx * vel;
            Direction = dir;
        }

        private void ReverseMove(float? force = null)
        {
            var dir = Direction;
            dir.x = -GroundDirX * Math.Abs(force ?? dir.x);
            LogTool.NLog("RoamStateTest", $"�����ƶ�: ");
            Direction = dir;
        }

        private int Ran(float leftDirProbability)
        {
            return MathTool.rand.NextDouble() < leftDirProbability ? -1 : 1;
        }

    }
}
