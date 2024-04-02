using GoldSprite.UFsm;
using System;
using UnityEngine;

namespace GoldSprite.EntitySystem2D {
    [CreateAssetMenu]
    public class IdleBehaviourMeta : EntityBehaviourMeta {
        public override Type StateType=> typeof(IdleState);
        public BasePropsMeta props;
        public override IBaseProps Props=> props;
    }
}