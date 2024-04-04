using GoldSprite.UFsm;
using System;
using UnityEngine;

namespace GoldSprite.EntitySystem2D {
    [CreateAssetMenu]
    public class AutoAttackBehaviourMeta : EntityBehaviourMeta {
        public override Type StateType=> typeof(AutoAttackState);
    }
}