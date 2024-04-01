using System;
using UnityEngine;

namespace GoldSprite.UnityPlugins.EntitySystem2D {
    /// <summary>
    /// 这个类注意细节: 混合树动画在播放时使用混合树动画名, 在判定获取时使用具体子动画名
    /// </summary>
    public class JumpBehaviour : EntityBehaviourState {
        public bool JumpKey => ent.inputs.GetValue<bool>(ent.inputs.InputActions.GamePlay.Jump);
        public Vector2 MoveDir => ent.inputs.GetValue<Vector2>(ent.inputs.InputActions.GamePlay.Move);
        public bool IsGround => ent.physics.IsGround;
        public Rigidbody2D rb;
        public int JumpPhase { get => (int)ent.animCtrls.anims.GetFloat("JumpPhase"); set => ent.animCtrls.anims.SetFloat("JumpPhase", value); }
        public int AnimsPhase = 5;
        public string[] AnimNames;
        float jumpDrag = 0.3f;  //跳跃时移动阻力
        public bool CanExit;
        public bool Cancel => ent.fsm.IsCanTurn();

        public string CurrentAnimName => AnimNames[JumpPhase];  //0, 1, 2, 3, 4分别为起跳 上升 转下落 下落, 落地


        public override bool Enter()
        {
            var result = false;
            //起跳
            if (IsGround && JumpKey) result = true;
            //空中坠落
            //ent.fsm.FDebug($"JumpEnter: con{!IsGround && rb.velocity.y < 0}, con1{!IsGround}, con2{rb.velocity.y < 0}");
            if (!IsGround) {
                var ray = Physics2D.RaycastAll(rb.transform.position, Vector2.down, 5, LayerMask.GetMask(new string[] {"Ground"}));
                if (ray.Length != 0 && ray[0].distance > 0.3f) {  //0.2f
                    JumpPhase = 2;
                    result = true;
                }
            }
            return result;
        }
        public override bool Exit()
        {
            return (JumpPhase >= 4 && IsGround && CanExit);
        }

        public override void InitState()
        {
            rb = ent.props.GetProp<Rigidbody2D>("Rb");
        }


        public override void OnEnter0()
        {
            CanExit = false;
            ent.animCtrls.PlayAnim(AnimName);
        }

        public override void OnExit0()
        {
            JumpPhase = 0;
        }

        public override void Run()
        {
            ent.props.GetProp<Action<Vector2, float>>("MoveAction")?.Invoke(MoveDir, 1 - jumpDrag);
            var velY = rb.velocity.y;
            var velX = rb.velocity.x;
            var jumpForce = ent.props.GetProp<float>("JumpForce");
            switch (JumpPhase) {
                case 0:
                    if (ent.animCtrls.CAnimName != CurrentAnimName) break;
                    if (ent.animCtrls.IsCurrentAnimEnd(CurrentAnimName)) {
                        TakeJumpForce(jumpForce);
                        JumpPhase = 1;
                        ent.animCtrls.anims.Play(AnimName, 0, 0);
                    }
                    break;
                case 1:
                    if (ent.animCtrls.CAnimName != CurrentAnimName) break;
                    if (velY < jumpForce * 1 / 2f) {
                        JumpPhase = 2;
                        ent.animCtrls.anims.Play(AnimName, 0, 0);
                    }
                    break;
                case 2:
                    if (ent.animCtrls.CAnimName != CurrentAnimName) break;
                    //Debug.Log($"{ent.provider.CAnimName},  {ent.provider.CAnimNormalizedTime}");
                    if (ent.animCtrls.IsCurrentAnimEnd(CurrentAnimName)) {
                        JumpPhase = 3;
                        ent.animCtrls.anims.Play(AnimName, 0, 0);
                    }
                    if (IsGround) {
                        JumpPhase = 4;
                        ent.animCtrls.anims.Play(AnimName, 0, 0);
                    }
                    break;
                case 3:
                    if (ent.animCtrls.CAnimName != CurrentAnimName) break;
                    if (IsGround) {
                        JumpPhase = 4;
                        ent.animCtrls.anims.Play(AnimName, 0, 0);
                    }
                    break;
                case 4:
                    if (ent.animCtrls.CAnimName != CurrentAnimName) break;
                    if ((ent.animCtrls.CAnimNormalizedTime > 0.24f && Cancel) || ent.animCtrls.IsCurrentAnimEnd(CurrentAnimName)) {
                        CanExit = true;
                    }
                    break;
            }
        }

        private void TakeJumpForce(float jumpForce)
        {
            var vel = rb.velocity;
            vel.y = jumpForce;
            rb.velocity = vel;
        }
    }
}