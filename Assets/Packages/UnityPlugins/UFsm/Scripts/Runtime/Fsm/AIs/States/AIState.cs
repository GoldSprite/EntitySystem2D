using System;

namespace GoldSprite.UFsm {
    [Serializable]
    public class AIState : BaseState {
        public new AIFsm Fsm { get; }
        public new IAIProps Props => Fsm.Props;

        public AIState(AIFsm fsm) : base(fsm) { Fsm = fsm; }
    }
}
