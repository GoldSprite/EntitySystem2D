
using UnityEngine;

namespace GoldSprite.UFsm {
    public class FallState : BaseState {

        public override bool CanTranSelf { get; protected set; } = false;
        public FallState(BaseFsm fsm) : base(fsm)
        {
        }

        public override bool Enter()
        {
            return Props.Velocity.y<0 && !Props.IsGround;
        }
        public override bool Exit() => Props.IsGround;

        public override void OnEnter()
        {
            //¶¯»­
            Fsm.AnimCtrls.Play(AnimName, 0, 0);
        }

        public override void OnExit()
        {
        }

        public override void FixedUpdate()
        {
            ((IJumper)Props).MoveState.Move(Props.JumpingMoveDrag);
        }
    }
}
