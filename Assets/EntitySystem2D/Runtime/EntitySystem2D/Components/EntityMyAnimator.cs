using GoldSprite.UnityPlugins.MyAnimator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GoldSprite.EntitySystem2D {
    public class EntityMyAnimator : MyAnimator{
        public void InitMetas(List<BehaviourMeta> behaviourMetaList)
        {
            var dict = new Dictionary<Enum, AnimationClip>();
            foreach(var meta in behaviourMetaList) {
                dict[meta.BId] = meta.AnimClip;
            }
            SetAnimClips(dict);
        }
    }
}
