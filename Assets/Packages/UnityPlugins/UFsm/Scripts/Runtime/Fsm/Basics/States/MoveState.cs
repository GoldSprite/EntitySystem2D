
using UnityEngine;

namespace GoldSprite.UFsm {
    public class MoveState : BaseState {
        public override bool CanTranSelf { get; protected set; } = false;
        public MoveState(BaseFsm fsm) : base(fsm) {
        }

        public override bool Enter() => Props.Direction.x != 0 && Props.IsGround;
        public override bool Exit() => Props.Velocity == Vector2.zero || !Props.IsGround;

        public override void OnEnter()
        {
        }

        public override void OnExit()
        {
        }

        public override void FixedUpdate()
        {
            Move();

            //动画
            Fsm.AnimCtrls.Play(AnimName);
        }

        public void Move(float moveDrag = 0)
        {
            //属性
            var vel = Props.Velocity;
            var boost = (Props.MoveBoostKey ? Props.SpeedBoost : 1);
            var drag = (1 - moveDrag);
            vel.x = Props.Direction.x * Props.Speed * boost * drag;
            Props.Velocity = vel;

            //转向
            if (Props.Direction.x != 0) Props.Face = Props.Direction.x > 0 ? 1 : -1;
        }
    }
}
