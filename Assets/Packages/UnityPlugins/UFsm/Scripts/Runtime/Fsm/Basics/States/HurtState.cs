
using System;
using UnityEngine;

namespace GoldSprite.UFsm {
    public class HurtState : BaseState {
        public override bool CanTranSelf { get; protected set; } = false;

        public IEntityProps Attacker;
        public float HurtDamage;


        public HurtState(BaseFsm fsm) : base(fsm)
        {
            fsm.AddCommandListener(BaseFsmCommand.Hurt, (Action<IAttacker>)((attacker) => {
                if (attacker is IEntityProps Attacker)this.Attacker = Attacker;
                HurtDamage = attacker.AttackPower * 1;
            }));
        }

        public override bool Enter()
        {
            var enter = !Props.DeathKey && Props.HurtKey;
            return enter;
        }
        public override bool Exit() => Fsm.AnimCtrls.IsCurrentAnimEnd(AnimName);

        public override void OnEnter()
        {
            if (DeathJudgment()) return;
            //动画
            Fsm.AnimCtrls.Play(AnimName, 0, 0);
            if (Attacker != null) {
                float vecx = Attacker.BodyCollider.bounds.center.x - Fsm.Props.BodyCollider.bounds.center.x;
                //Debug.Log($"攻击者是实体状态机, 他的方位: {vecx}");
                Fsm.Command(BaseFsmCommand.Turn, vecx);
            }
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
            Props.HurtKey = false;
        }

        public override void FixedUpdate()
        {
        }
    }
}
