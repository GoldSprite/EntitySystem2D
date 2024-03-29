using UnityEngine;

namespace GoldSprite.Fsm {
    public class IdleState : BaseState {
        public IdleState(BaseFsm fsm, BaseProps props) : base(fsm, props) { }

        public override bool Enter()
        {
            return Props.MoveDir == Vector2.zero;
        }
        public override bool Exit0()
        {
            return Props.MoveDir != Vector2.zero;
        }
    }
}
