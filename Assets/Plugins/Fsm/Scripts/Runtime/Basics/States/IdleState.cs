using UnityEngine;

namespace GoldSprite.Fsm {
    public class IdleState : BaseState {
        public override bool CanTranSelf { get; protected set; } = false;
        public IdleState(BaseFsm fsm, BaseProps props) : base(fsm, props) {
        }

        public override bool Enter()
        {
            return true;
        }
        public override bool Exit()
        {
            return true;
        }
    }
}
