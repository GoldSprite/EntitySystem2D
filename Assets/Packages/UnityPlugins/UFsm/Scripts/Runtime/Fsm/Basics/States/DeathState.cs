
using UnityEngine;

namespace GoldSprite.UFsm {
    public class DeathState : BaseState {
        public override bool CanTranSelf { get; protected set; } = false;
        public DeathState(BaseFsm fsm) : base(fsm)
        {
        }

        public override bool Enter()
        {
            if (Props.Health <= 0) Props.DeathKey = true;
            return Props.DeathKey;
        }
        public override bool Exit()
        {
            return !Props.DeathKey;
        }

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
        }
    }
}
