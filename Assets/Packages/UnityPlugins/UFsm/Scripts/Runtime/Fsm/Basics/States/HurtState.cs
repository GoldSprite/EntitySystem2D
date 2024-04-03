
using UnityEngine;

namespace GoldSprite.UFsm {
    public class HurtState : BaseState {
        public override bool CanTranSelf { get; protected set; } = false;
        public HurtState(BaseFsm fsm) : base(fsm)
        {
        }

        public override bool Enter()
        {
            return Props.HurtKey;
        }
        public override bool Exit() => Fsm.AnimCtrls.IsCurrentAnimEnd(AnimName);

        public override void OnEnter()
        {
            Props.HurtKey = false;
            //¶¯»­
            Fsm.AnimCtrls.Play(AnimName, 0, 0);
        }

        public override void OnExit()
        {
        }

        public override void FixedUpdate()
        {
        }
    }
}
