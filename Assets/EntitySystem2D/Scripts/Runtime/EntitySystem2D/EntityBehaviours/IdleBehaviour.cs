using GoldSprite.UnityPlugins.MyInputSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GoldSprite.UnityPlugins.EntitySystem2D {
    public class IdleBehaviour : EntityBehaviourState {
        public Rigidbody2D rb;
        public bool IsGround => ent.physics.IsGround;
        private float IdleFrameCache;

        public Vector2 MoveDir => ent.inputs.GetValue<Vector2>(ent.inputs.InputActions.GamePlay.Move);


        //这个enter其实可以省略(一般作为defaultState在其他状态OnExit之后都会自动变为idle)
        public override bool Enter()
        {
            return IsGround && MoveDir.x == 0/* && !ent.provider.CAnimTranslationing*/;
        }
        public override bool Exit()
        {
            return MoveDir.x != 0 /*&& !ent.provider.CAnimTranslationing*/;
        }


        public override void InitState()
        {
            rb = ent.props.GetProp<Rigidbody2D>("Rb");
        }

        public override void OnEnter0()
        {
            //ent.fsm.FDebug("执行停下.");
            //var vel = rb.velocity;
            //vel.x = 0;
            //rb.velocity = vel;

            ent.animCtrls?.CrossFade(AnimName, 0.12f);
        }

        public override void OnExit0()
        {
            IdleFrameCache = ent.animCtrls.CAnimNormalizedTime;
        }
    }


}
