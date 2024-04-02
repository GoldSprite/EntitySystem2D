using GoldSprite.UFsm;
using GoldSprite.UnityTools.MyDict;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoldSprite.EntitySystem2D {
    public abstract class BehaviourMeta : SerializedScriptableObject, IBehaviourMeta {
        public virtual Enum BId {get;}
        public int priority;
        public int Priority => priority;
        public AnimationClip animClip;
        public virtual AnimationClip AnimClip => animClip;
        public virtual Type StateType { get; }
        public virtual IBaseProps Props { get; }
    }
    public abstract class EntityBehaviourMeta : BehaviourMeta {
        public EntityBId bid;
        public override Enum BId => bid;
    }


    public enum EntityBId
    {
        Idle, Move
    }
}