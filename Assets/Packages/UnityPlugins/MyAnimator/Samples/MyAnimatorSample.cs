using GoldSprite.UnityTools.MyDict;
using GoldSprite.UnityPlugins.MyAnimator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoldSprite.UnityPlugins.MyAnimator.Samples {
    public class MyAnimatorSample : MonoBehaviour {
        public MyAnimator animCtrl;
        public MyDict<AnimClipType, AnimationClip> clips;


        private void Start()
        {
            animCtrl.SetAnimClips(clips);
            animCtrl.OnAnimEndEvent += (clipType) => {
                if (AnimClipType.Idle.Equals(clipType))
                    Debug.Log("AnimClipType.Idle动画结束");
            };
            animCtrl.OnAnimTranEvent += (lastAnim, nowAnim) => {
                Debug.Log($"动画转换: {lastAnim}=>{nowAnim}");
            };
        }

        [ContextMenu("Exec")]
        public void Exec()
        {
            if (!Application.isPlaying) return;
            animCtrl.Play(AnimClipType.Idle, 0, 0);
        }


        public enum AnimClipType {
            Idle, Run, Hurt
        }
    }
}
