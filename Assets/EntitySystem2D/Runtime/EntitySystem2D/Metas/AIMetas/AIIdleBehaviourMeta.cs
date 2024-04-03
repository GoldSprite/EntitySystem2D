using GoldSprite.UFsm;
using System;
using UnityEngine;

namespace GoldSprite.EntitySystem2D {
    [CreateAssetMenu]
    public class AIIdleBehaviourMeta : EntityBehaviourMeta {
        public override Type StateType=> typeof(AIIdleState);
    }
}