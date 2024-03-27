using System;
using UnityEngine;

namespace GoldSprite.UnityPlugins.EntitySystem2D.Tests {
    [Serializable]
    public class AnimManager {
        public bool debugLog;
        //引用
        public Animator anims;
        public string CAnim = "", LastAnim = "";
        [Header("动画事件")]
        public Action<string, string> AnimTranAction;  //last, next

        //public Action<string> AnimEndAction;


        public void SetAnims(Animator anims) => this.anims = anims;


        public void PlayAnim(string animName)
        {
            anims.Play(animName);
        }

        public bool IsAnimName(string animName)
        {
            return anims.GetCurrentAnimatorStateInfo(0).IsName(animName);
        }


        public void Update()
        {
            try {
                CAnim = anims.GetCurrentAnimatorClipInfo(0)[0].clip.name;
                if (LastAnim != CAnim) {
                    ADebug($"动画转换事件{LastAnim} -> {CAnim}");
                    AnimTranAction?.Invoke(LastAnim, CAnim);
                    LastAnim = CAnim;
                }
            }
            catch (Exception) { }
        }


        public void ADebug(string msg)
        {
            if (debugLog)
                Debug.Log(msg);
        }
    }
}