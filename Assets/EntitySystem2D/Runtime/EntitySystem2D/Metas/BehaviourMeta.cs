using GoldSprite.UFsm;
using GoldSprite.UnityTools.MyDict;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoldSprite.EntitySystem2D {
    public abstract class BehaviourMeta : ScriptableObject, IBehaviourMeta {
        public int priority;
        public int Priority => priority;
        public AnimationClip animClip;
        public AnimationClip AnimClip => animClip;
        public virtual Type StateType { get; }
        public virtual IBaseProps Props { get; }
    }
}