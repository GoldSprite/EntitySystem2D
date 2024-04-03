
using UnityEngine;

namespace GoldSprite.UFsm {
    public class JumpState : BaseState {

        public override bool CanTranSelf { get; protected set; } = false;
        public JumpState(BaseFsm fsm) : base(fsm)
        {
        }

        public override bool Enter()
        {
            return Props.JumpKey && Props.IsGround;
        }
        public override bool Exit() => Props.Velocity.y <= 0;

        public override void OnEnter()
        {
            Props.JumpKey = false;
            Jump();
            //¶¯»­
            Fsm.AnimCtrls.Play(AnimName, 0, 0);
        }

        public override void OnExit()
        {
        }

        public override void FixedUpdate()
        {
            if (((IJumper)Props).MoveState != null)
                ((IJumper)Props).MoveState.Move(Props.JumpingMoveDrag);
        }

        public void Jump()
        {
            //ÊôÐÔ
            var vel = Props.Velocity;
            vel.y = Props.JumpForce;
            Props.Velocity = vel;
        }
    }
}
