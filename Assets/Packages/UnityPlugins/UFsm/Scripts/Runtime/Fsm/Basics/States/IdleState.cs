using UnityEngine;

namespace GoldSprite.UFsm {
    public class IdleState : BaseState {
        public override bool CanTranSelf { get; protected set; } = false;
        public IdleState(BaseFsm fsm, IBaseProps props) : base(fsm, props) {
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
