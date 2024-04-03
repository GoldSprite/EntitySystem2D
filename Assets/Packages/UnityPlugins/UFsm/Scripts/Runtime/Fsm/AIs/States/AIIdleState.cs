using Sirenix.OdinInspector;
using UnityEngine;

namespace GoldSprite.UFsm {
    public class AIIdleState : AIState {
        private float Ticker, TickerInterval = 3;
        private float TickerNormalize => (Ticker - seconds) / TickerInterval;
        private float seconds;

        public AIIdleState(AIFsm fsm) : base(fsm)
        {
            CanTranSelf = false;
        }

        public override bool Enter() => false;
        public override bool Exit() => false;

        public override void OnEnter()
        {
            Ticker = Time.time + TickerInterval;
        }
        public override void OnExit()
        {
            Ticker = 0;
            LogTool.NLog("RoamStateTest", $"�˳�ʱ���ü�ʱ��: ");
        }

        public override void Update()
        {
            RandomRoamTask();
        }

        private void RandomRoamTask()
        {
            seconds = Time.time;
            if (TickerNormalize <= 0) {
                LogTool.NLog("RoamStateTest", $"ִ���������: ");
                Fsm.Props.RoamState.EnterRoam = true;
            }
        }
    }
}
