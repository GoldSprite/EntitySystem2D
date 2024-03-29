using UnityEngine;

namespace GoldSprite.Fsm {
    public class MoveState : BaseState {
        public override bool CanTranSelf { get; protected set; } = false;
        public MoveState(BaseFsm fsm, BaseProps props) : base(fsm, props) {
        }

        public override bool Enter()
        {
            return Props.Velocity != Vector2.zero && Props.IsGround;
        }
        public override bool Exit0()
        {
            return Props.Direction == Vector2.zero || !Props.IsGround;
        }
    }
}
