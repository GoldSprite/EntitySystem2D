
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
            //属性
            var vel = Props.Velocity;
            vel.x = Props.Direction.x * Props.Speed;
            Props.Velocity = vel;

            //转向
            if(Props.Direction.x != 0) Props.Face = Props.Direction.x > 0 ? 1 : -1;

            //动画
            Fsm.AnimCtrls.Play(AnimEnum);
        }
    }
}
