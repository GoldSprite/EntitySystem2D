using GoldSprite.UFsm;
using System;
using UnityEngine;

namespace GoldSprite.EntitySystem2D {
    [CreateAssetMenu]
    public class AttackBehaviourMeta : EntityBehaviourMeta {
        public override Type StateType => typeof(AttackState);
    }
}