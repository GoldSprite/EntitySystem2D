using GoldSprite.UFsm;
using System;
using UnityEngine;

namespace GoldSprite.EntitySystem2D {
    [CreateAssetMenu]
    public class JumpBehaviourMeta : EntityBehaviourMeta {
        public override Type StateType => typeof(JumpState);
    }
}