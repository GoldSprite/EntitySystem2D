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
        private FinateStateMachine fsm;
        private InputProvider inputs;
        public int Priority { get; private set; }
        private Rigidbody2D rb;
        public bool StateSwitch { get; private set; }


        //这个enter其实可以省略(一般作为defaultState在其他状态OnExit之后都会自动变为idle)
        public bool Enter()
        {
            var moveDir = inputs.GetValue<Vector2>(MoveKey);
            return moveDir == Vector2.zero;
        }
        public bool Exit()
        {
            var moveDir = inputs.GetValue<Vector2>(MoveKey);
            return moveDir != Vector2.zero;
        }
        public void OnEnter() { }
        public void OnExit() { }
        public void Run() { }


        public override void Init(PropertyManager props, FinateStateMachine fsm, InputProvider inputs, int priority)
        {
            this.Priority = priority;
            this.rb = props.GetProp<Rigidbody2D>("Rb");
            if(fsm.currentState==null) fsm.InitState(this);
            fsm.AddState(this);
            inputs.RegisterActionListener(inputs.GetInputActions().GamePlay.Move, (Action<Vector2>)MoveKey);
        }



        public void MoveKey(Vector2 vector)
        {
            //key up
            if(vector == Vector2.zero) {
                fsm.UpdateNextState();
            }
        }

        public void Idle()
        {
            rb.velocity = Vector2.zero;
        }
    }


    public abstract class EntityBehaviour {
        public abstract void Init(PropertyManager props, FinateStateMachine fsm, InputProvider inputs, int priority);

        public enum Type
        {
            Idle, Move
        }
    }
}
