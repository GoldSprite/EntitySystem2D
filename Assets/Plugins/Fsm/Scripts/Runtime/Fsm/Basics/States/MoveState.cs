
using UnityEngine;

namespace GoldSprite.Fsm {
    public class MoveState : BaseState {
        public override bool CanTranSelf { get; protected set; } = false;
        public MoveState(BaseFsm fsm, BaseProps props) : base(fsm, props) {
        }

        public override bool Enter()
        {
            return Props.Direction.x != 0 && Props.IsGround;
        }
        public override bool Exit()
        {
            return Props.Velocity == Vector2.zero || !Props.IsGround;
        }

        public override void OnEnter()
        {
        }

        public override void OnExit()
        {
        }

        public override void FixedUpdate()
        {
            var vel = Props.Velocity;
            vel.x = Props.Direction.x * Props.Speed;
            Props.Velocity = vel;
        }
    }
}
