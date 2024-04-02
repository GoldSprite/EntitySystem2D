using GoldSprite.UnityTools.MyDict;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace GoldSprite.UnityPlugins.MyAnimator {
    public class MyAnimator : MonoBehaviour {
        [ShowMyAnimator]
        public string draw;
        public Animator anims;
        private MyDict<Enum, AnimationClip> animClips = new();

        //引用
        [Header("动画事件")]
        public Action<Enum> OnAnimEndEvent = (p) => { };
        public Action<Enum, Enum> OnAnimTranEvent = (p, p2) => { };  //last, next
        [Header("实时")]
        public string CAnimName = "";
        public string LastAnimName = "";
        public string LastAnimEndName = "";
        public Enum CAnimEnum, LastAnimEnum, LastAnimEndEnum;
        public AnimationClip CAnimClip => anims.GetCurrentAnimatorClipInfo(0)[0].clip;
        public AnimatorStateInfo CAnimState => anims.GetCurrentAnimatorStateInfo(0);
        public float CAnimNormalizedTime;
        public bool CAnimClipLooping;
        public bool CAnimStateLoop;
        public bool CAnimTranslationing;


        public void SetAnimClips<T>(MyDict<T, AnimationClip> clips) where T : Enum
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
                CAnimEnum = GetClipKey(CAnimClip);
                CAnimNormalizedTime = CAnimState.normalizedTime;
                CAnimClipLooping = CAnimClip.isLooping;
                CAnimStateLoop = CAnimState.loop;
                CAnimTranslationing = anims.IsInTransition(0);

                if (LastAnimEndName != CAnimName && IsCurrentAnimEnd(CAnimName)) {
                    LastAnimEndName = CAnimName;
                    //PlayAnim(CAnimName, 0, CAnimState.normalizedTime - 1);
                    LogTool.NLog("MyAnimator", $"动画播放结束事件: {LastAnimName}");
                    //CurrentAnimEndAction?.Invoke(LastAnimName);
                    var clipKey = GetClipKey(CAnimClip);
                    OnAnimEndEvent?.Invoke(clipKey);
                }
                if (LastAnimName != CAnimName) {
                    LastAnimEndName = "";
                    LogTool.NLog("MyAnimator", $"动画转换事件{LastAnimName} -> {CAnimName}");
                    OnAnimTranEvent?.Invoke(LastAnimEnum, CAnimEnum);
                    LastAnimName = CAnimName;
                    LastAnimEnum = CAnimEnum;
                }
            }
            catch (Exception) { }
        }


        public Enum GetClipKey(AnimationClip clip)
        {
            return animClips.FirstOrDefault(p => p.Item2 == clip).Item1;
        }

        public void Play(Enum key, int layer = 0, float normalizedTime = 0)
        {
            anims.Play(animClips[key].name);
        }

        public void PlayAnim(string animName, int layer = 0, float normalizedTime = 0)
        {
            anims.Play(animName, layer, normalizedTime);
        }

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

        public T InsertComponentLater<T>()
        {
            throw new NotImplementedException();
        }
    }


    public class ShowMyAnimatorAttribute : PropertyAttribute { }

}
