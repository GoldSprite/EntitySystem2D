using GoldSprite.UFsm;
using System;
using UnityEngine;

namespace GoldSprite.EntitySystem2D {
    public interface IBehaviourMeta
    {
        public int Priority { get; }
        public Type StateType { get; }
        public AnimationClip AnimClip { get; }
        public IBaseProps Props { get; }
    }
}