
using UnityEngine;

namespace GoldSprite.UFsm {
    public class MoveState : BaseState {
        public override bool CanTranSelf { get; protected set; } = false;
        public MoveState(BaseFsm fsm) : base(fsm) {
        }

        public override bool Enter() => Props.Direction.x != 0 && Props.IsGround;
        public override bool Exit() => (Props.Direction.x == 0 && Props.Velocity == Vector2.zero) || !Props.IsGround;

        public override void OnEnter()
        {
        }

        public override void OnExit()
        {
            Fsm.AnimCtrls.SetSpeed(1);
            ////退出时置零速度保证其他状态不会有滑步.
            //var dir = Props.Direction;
            //dir.x = 0;
            //Props.Direction = dir;
        }

        public override void FixedUpdate()
        {
            Move();

            //动画
            Fsm.AnimCtrls.SetSpeed(Props.Direction.x);
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
