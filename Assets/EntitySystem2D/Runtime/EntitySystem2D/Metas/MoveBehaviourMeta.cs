using GoldSprite.UFsm;
using System;
using UnityEngine;

namespace GoldSprite.EntitySystem2D {
    [CreateAssetMenu]
    public class MoveBehaviourMeta : BehaviourMeta {
        public override Type StateType => typeof(MoveState);
        public BasePropsMeta props;
        public override IBaseProps Props => props;
    }
}