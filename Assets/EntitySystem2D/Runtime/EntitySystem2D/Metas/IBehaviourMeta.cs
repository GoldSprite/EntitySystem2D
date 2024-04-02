using GoldSprite.UFsm;
using GoldSprite.UnityTools.MyDict;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GoldSprite.EntitySystem2D {
    public interface IBehaviourMeta {

        public Enum BId { get; }
        public int Priority { get; }
        public Type StateType { get; }
        public AnimationClip AnimClip { get; }
        public IBaseProps Props { get; }
    }
}