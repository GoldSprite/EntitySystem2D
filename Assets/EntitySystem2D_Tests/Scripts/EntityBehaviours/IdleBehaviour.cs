using GoldSprite.UnityPlugins.MyInputSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GoldSprite.UnityPlugins.EntitySystem2D.Tests {
    public class IdleBehaviour : EntityBehaviour, IState {
        public string Name => "IdleState";
        public EntityBehaviourConstructor ent;
        public int Priority { get; private set; }
        public bool StateSwitch { get; private set; }
        public Rigidbody2D rb;


        //这个enter其实可以省略(一般作为defaultState在其他状态OnExit之后都会自动变为idle)
        public bool Enter()
        {
            var moveDir = ent.inputs.GetValue<Vector2>(MoveKey);
            return moveDir == Vector2.zero;
        }
        public bool Exit()
        {
            var moveDir = ent.inputs.GetValue<Vector2>(MoveKey);
            return moveDir != Vector2.zero;
        }
        public void OnEnter() { }
        public void OnExit() { }
        public void Run() { }


        public override void Init(EntityBehaviourConstructor ent, int priority)
        {
            this.ent = ent;
            rb = ent.props.GetProp<Rigidbody2D>("Rb");

            Priority = priority;
            if(ent.fsm.currentState==null) ent.fsm.InitState(this);
            else ent.fsm.AddState(this);

            ent.inputs.RegisterActionListener(ent.inputs.InputActions.GamePlay.Move, (Action<Vector2>)MoveKey);
        }



        public void MoveKey(Vector2 vector)
        {
            //key up
            if(vector == Vector2.zero) {
                ent.fsm.UpdateNextState();
            }
        }

        public void Idle()
        {
            rb.velocity = Vector2.zero;
        }
    }


    public abstract class EntityBehaviour {
        public abstract void Init(EntityBehaviourConstructor ent, int priority);

        public enum Type
        {
            Idle, Move
        }
    }
}
