using UnityEngine;

namespace GoldSprite.UFsm {
    public class IdleState : BaseState {
        public override bool CanTranSelf { get; protected set; } = false;
        public IdleState(BaseFsm fsm) : base(fsm) {
        }

        public override bool Enter() => false/*Props.Velocity == Vector2.zero*/;
        public override bool Exit() => /*Props.Direction.x != 0*/true;

        public override void Update()
        {
            //¶¯»­
            Fsm.AnimCtrls.Play(AnimName);
        }
    }
}
