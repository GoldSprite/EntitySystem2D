
using System;
using UnityEngine;

namespace GoldSprite.UFsm {
    public class HurtState : BaseState {
        public override bool CanTranSelf { get; protected set; } = false;
        public float HurtDamage;

        public HurtState(BaseFsm fsm) : base(fsm)
        {
            fsm.AddCommandListener(BaseFsmCommand.Hurt, (Action<IAttacker>)((attacker) => {
                HurtDamage = attacker.AttackPower * 1;
            }));
        }

        public override bool Enter()
        {
            return Props.HurtKey;
        }
        public override bool Exit() => Fsm.AnimCtrls.IsCurrentAnimEnd(AnimName);

        public override void OnEnter()
        {
            Props.HurtKey = false;
            if (DeathJudgment()) return;
            //¶¯»­
            Fsm.AnimCtrls.Play(AnimName, 0, 0);
        }

        public bool DeathJudgment()
        {
            var health = Props.Health - HurtDamage;
            if(health > 0) {
                Props.Health = health;
                return false;
            } else {
                Props.Health = 0;
                Props.DeathKey = true;
                return true;
            }
        }

        public override void OnExit()
        {
        }

        public override void FixedUpdate()
        {
        }
    }
}
