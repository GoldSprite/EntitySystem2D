using UnityEngine;

namespace GoldSprite.UFsm {
    public class IdleState : BaseState {
        public override bool CanTranSelf { get; protected set; } = false;
        public IdleState(BaseFsm fsm) : base(fsm) {
        }

        public override bool Enter() => true;
        public override bool Exit() => true;

        public override void Update()
        {
            //¶¯»­
            Fsm.AnimCtrls.Play(AnimName);
        }
    }
}
