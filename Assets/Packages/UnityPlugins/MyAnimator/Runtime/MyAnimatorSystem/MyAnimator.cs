using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Sirenix;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;

namespace GoldSprite.UnityPlugins.MyAnimator {
    public class MyAnimator: SerializedMonoBehaviour {
        [ManualRequire]
        public Animator anims;
        [OdinSerialize]
        private readonly Dictionary<string, AnimationClip> animClips = new();

        //引用
        [Header("动画事件")]
        public Action<string> OnAnimEndEvent = (p) => { };
        public Action<string, string> OnAnimTranEvent = (p, p2) => { };  //last, next
        [Header("实时")]
        public string CAnimName = "";
        public string LastAnimName = "";
        public string LastAnimEndName = "";
        public AnimationClip CAnimClip => anims.GetCurrentAnimatorClipInfo(0)[0].clip;
        public AnimatorStateInfo CAnimState => anims.GetCurrentAnimatorStateInfo(0);
        public float CAnimNormalizedTime;
        public bool CAnimClipLooping;
        public bool CAnimStateLoop;
        public bool CAnimTranslationing;


        public void SetAnimClips(Dictionary<string, AnimationClip> clips)
        {
            animClips.Clear();
            foreach (var (k, v) in clips) {
                animClips.Add(k, v);
            }
        }

        private void Update()
        {
            try {
                CAnimName = CAnimClip.name;
                CAnimNormalizedTime = CAnimState.normalizedTime;
                CAnimClipLooping = CAnimClip.isLooping;
                CAnimStateLoop = CAnimState.loop;
                CAnimTranslationing = anims.IsInTransition(0);

                if (LastAnimName != CAnimName) {
                    LastAnimEndName = "";
                    LogTool.NLog("MyAnimator", $"动画转换事件{LastAnimName} -> {CAnimName}");
                    OnAnimTranEvent?.Invoke(LastAnimName, CAnimName);
                    LastAnimName = CAnimName;
                }

                if (LastAnimEndName != CAnimName && IsCurrentAnimEnd(CAnimName)) {
                    //Play(CAnimName, 0, CAnimState.normalizedTime - 1);
                    LogTool.NLog("MyAnimator", $"动画播放结束事件: {CAnimName}");
                    OnAnimEndEvent?.Invoke(CAnimName);
                    LastAnimEndName = CAnimName;
                }
            }
            catch (Exception) { }
        }


        public string GetClipKey(AnimationClip clip)
        {
            return animClips.FirstOrDefault(p => p.Value == clip).Key;
        }

        public void Play(string animName) => anims.Play(animName);
        public void Play(string animName, int layer = 0) => anims.Play(animName, layer);
        public void Play(string animName, int layer = 0, float normalizedTime = 0) => anims.Play(animName, layer, normalizedTime);

        public bool IsAnimName(string animName)
        {
            return anims.GetCurrentAnimatorStateInfo(0).IsName(animName);
        }

        public bool IsCurrentAnimEnd(string animName)
        {
            return CAnimName == animName/* && !CAnimClipLooping*/ && CAnimState.normalizedTime >= 1f;
        }

        public void CrossFade(string animName, float normalizedTransitionDuration)
        {
            if (anims == null) return;
            anims.CrossFade(animName, normalizedTransitionDuration);
        }
    }





    public class ShowMyAnimatorAttribute : PropertyAttribute { }

}
