using System;
using UnityEngine;

namespace GoldSprite.UnityPlugins.EntitySystem2D.Tests {
    public class AttackBehaviour : EntityBehaviourState {
        public bool JumpKey => ent.inputs.GetValue<bool>(ent.inputs.InputActions.GamePlay.Jump);
        public bool IsGround => ent.physics.IsGround;
        public Rigidbody2D rb;
        public int JumpPhase { get => (int)ent.animCtrls.anims.GetFloat("JumpPhase"); set=> ent.animCtrls.anims.SetFloat("JumpPhase", value); }
        public string[] AnimNames;
        public string CurrentAnimName => AnimNames[JumpPhase];


        public override bool Enter()
        {
            return JumpKey && IsGround;
        }
        public override bool Exit()
        {
            return JumpPhase > 2 && IsGround;
        }

        public override void InitState()
        {
            rb = ent.props.GetProp<Rigidbody2D>("Rb");

            ent.inputs.AddActionListener(ent.inputs.InputActions.GamePlay.Attack, (Action<bool>)((down) => {
                if (down) ent.fsm.UpdateNextState();
            }));
        }


        public override void OnEnter()
        {
            ent.animCtrls.PlayAnim(AnimName);
        }

        public override void OnExit()
        {
            JumpPhase = 0;
        }

        public override void Run()
        {
            var velY = rb.velocity.y;
            var jumpForce = ent.props.GetProp<float>("JumpForce");
            switch (JumpPhase) {
                case 0:
                    if (ent.animCtrls.IsCurrentAnimEnd(CurrentAnimName)) {
                        TakeJumpForce(jumpForce);
                        JumpPhase++;
                    }
                    break;
                case 1:
                    if (velY < jumpForce*1/2f) {
                        JumpPhase++;
                        ent.animCtrls.anims.Play(AnimName, 0, 0);
                    }
                    break;
                case 2:
                    Debug.Log($"{ent.animCtrls.CAnimName},  {ent.animCtrls.CAnimNormalizedTime}");
                    if (ent.animCtrls.IsCurrentAnimEnd(CurrentAnimName))
                        JumpPhase++;
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