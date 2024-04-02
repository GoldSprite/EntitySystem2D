using GoldSprite.UFsm;
using System;
using UnityEngine;

namespace GoldSprite.EntitySystem2D {
    [CreateAssetMenu]
    public class IdleBehaviourMeta : BehaviourMeta {
        public override Type StateType=> typeof(IdleState);
        public BasePropsMeta props;
        public override IBaseProps Props=> props;
    }
}