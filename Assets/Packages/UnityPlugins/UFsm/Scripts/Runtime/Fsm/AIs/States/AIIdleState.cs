using UnityEngine;

namespace GoldSprite.UFsm {
    public class AIIdleState : AIState {
        public override bool CanTranSelf { get; protected set; } = false;
        public AIIdleState(AIFsm fsm) : base(fsm)
        {
        }

        public override bool Enter() => false;
        public override bool Exit() => true;

        public override void Update()
        {
        }
    }
}
