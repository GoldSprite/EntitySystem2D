
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
            Fsm.Props.BodyCollider.enabled = false;
            Fsm.Props.BodyCollider.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }

        public override void OnExit()
        {
            Fsm.Props.BodyCollider.enabled = true;
            Fsm.Props.BodyCollider.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }

        public override void FixedUpdate()
        {
        }
    }
}
