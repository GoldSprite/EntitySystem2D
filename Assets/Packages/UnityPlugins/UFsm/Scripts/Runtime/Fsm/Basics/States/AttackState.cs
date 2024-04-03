
using UnityEngine;

namespace GoldSprite.UFsm {
    public class AttackState : BaseState {
        public override bool CanTranSelf { get; protected set; } = true;
        public AttackState(BaseFsm fsm) : base(fsm)
        {
        }

        public override bool Enter()
        {
            return Props.AttackKey;
        }
        public override bool Exit() => Fsm.AnimCtrls.IsCurrentAnimEnd(AnimName);

        public override void OnEnter()
        {
            Props.AttackKey = false;
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
