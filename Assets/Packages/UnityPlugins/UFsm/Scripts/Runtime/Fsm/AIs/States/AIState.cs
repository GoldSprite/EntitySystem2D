using System;

namespace GoldSprite.UFsm {
    [Serializable]
    public class AIState : BaseState {
        public new AIFsm Fsm { get; }
        public new IAIProps Props => Fsm.Props;

        public AIState(AIFsm fsm) : base(fsm) { Fsm = fsm; }

        public override string ToString()
        {
            return $"[{Props.Name}-{GetType().Name}]";
        }

        public override bool Enter() => false;
        public override bool Exit() => true;
    }
}
