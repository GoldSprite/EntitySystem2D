﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace GoldSprite.UnityPlugins.EntitySystem2D {
    [Serializable]
    public class AnimsProvider : IEntityProvider {
        public bool debugLog;
        //引用
        public Animator anims;
        [Header("动画事件")]
        public Action<string, string> AnimTranAction;  //last, next
        public Action<string> CurrentAnimEndAction;
        [Header("实时")]
        public string CAnimName = "", LastAnimName = "";
        public AnimationClip CAnimClip => anims.GetCurrentAnimatorClipInfo(0)[0].clip;
        public AnimatorStateInfo CAnimState => anims.GetCurrentAnimatorStateInfo(0);
        public float CAnimNormalizedTime;
        public bool CAnimClipLooping;
        public bool CAnimStateLoop;
        public bool CAnimTranslationing;

        public void SetAnims(Animator anims) => this.anims = anims;

        public bool Init()
        {
            var msgs = new List<string>();
            if (anims == null) msgs.Add($"[AnimCtrls]: 未设置动画控制组件anims, 将不会播放任何动画.");
            return IEntityProvider.PrintInitLog(this, msgs);
        }


        public void PlayAnim(string animName)
        {
            anims.Play(animName);
        }

        public bool IsAnimName(string animName)
        {
            return anims.GetCurrentAnimatorStateInfo(0).IsName(animName);
        }

        public bool IsCurrentAnimEnd(string animName)
        {
            return CAnimName == animName/* && !CAnimClipLooping*/ && CAnimState.normalizedTime >= 1f;
        }


        public void Update()
        {
            try {
                CAnimName = CAnimClip.name;
                CAnimNormalizedTime = CAnimState.normalizedTime;
                CAnimClipLooping = CAnimClip.isLooping;
                CAnimStateLoop = CAnimState.loop;
                CAnimTranslationing = anims.IsInTransition(0);

                if (LastAnimName != CAnimName) {
                    //if (IsCurrentAnimEnd(CAnimName)) {
                    ADebug($"动画播放结束事件: {LastAnimName}");
                    CurrentAnimEndAction?.Invoke(LastAnimName);
                    //}

                    ADebug($"动画转换事件{LastAnimName} -> {CAnimName}");
                    AnimTranAction?.Invoke(LastAnimName, CAnimName);
                    LastAnimName = CAnimName;
                }
            }
            catch (Exception) { }
        }


        public void ADebug(string msg)
        {
            if (debugLog)
                Debug.Log(msg);
        }

        public void CrossFade(string animName, float normalizedTransitionDuration)
        {
            if(anims == null) return;
            anims.CrossFade(animName, normalizedTransitionDuration);
        }
    }
}