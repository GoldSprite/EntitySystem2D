using GoldSprite.UnityPlugins.MyInputSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GoldSprite.UnityPlugins.EntitySystem2D.Tests {

    public class TestEntityConstructor : EntitySystem {
    }


    public class EntitySystem : MonoBehaviour {
        public string Name = "AA";
        [Header("Constructor")]
        [Tooltip("实体构造器")]
        public EntityBehaviourConstructor bevs;
        [Header("Components")]
        [Tooltip("属性管理者")]
        public PropertyManager props;
        [Tooltip("状态器")]
        public FinateStateMachine fsm;
        [Tooltip("输入提供者")]
        public InputProvider inputs;

        private void Awake()
        {
            props = new PropertyManager();
            fsm = new FinateStateMachine();
            inputs = new InputProvider();
            InitEntityBehaviours();
        }

        private void InitEntityBehaviours()
        {
            bevs = new EntityBehaviourConstructor(this);

            //Idle
            bevs.AddBehaviour(new IdleBehaviour());
            //bevs.AddBehaviour(new MoveBehaviour());

        }
    }


    [Serializable]
    public class EntityBehaviourConstructor {
        private EntitySystem ent;


        public EntityBehaviourConstructor(EntitySystem ent) => this.ent = ent;


        public void AddBehaviour(EntityBehaviour bev, int priority = 1)
        {
            if(ent.fsm.currentState == null) priority = 0;
            bev.Init(ent.props, ent.fsm, ent.inputs, priority);
        }
    }


    [Serializable]
    public class FinateStateMachine {
        public Dictionary<Type, IState> states = new();
        public IState currentState;
        public IState defaultState;

        public void InitState(IState state)
        {
            defaultState = currentState = state;
        }

        public void AddState(IState state)
        {
            states.Add(state.GetType(), state);
        }

        public void UpdateNextState()
        {
            var target = currentState;
            //遍历条件, 进入优先级最高的那个状态
            foreach (var(type, state) in states) {
                if (!EnterState(state, target)) continue;
                target = state;
            }
            if (target.Exit()) target = defaultState;  //计算完优先级后再判断返回到默认状态
            var change = currentState != target;
            if (change) OnEnterState(target);
        }

        protected bool EnterState(IState state) { return EnterState(state, currentState); }
        protected bool EnterState(IState state, IState cState)
        {
            if (state == cState || cState.Priority > state.Priority) return false;
            return state.Enter();
        }

        protected void OnEnterState(IState targetState)
        {
            currentState.OnExit();
            Debug.Log($"{"Fsm"}退出状态{currentState}.");
            currentState = targetState;
            currentState.OnEnter();
            Debug.Log($"{"Fsm"}进入状态{currentState}.");
        }
    }


    [Serializable]
    public class PropertyManager {
        public Dictionary<string, object> pairs;


        public T GetProp<T>(string key)
        {
            if (!pairs.ContainsKey(key)) return default(T);
            return (T)pairs[key];
        }
    }


    [Serializable]
    public class InputProvider : MyInputManager {

        protected override Dictionary<InputActionMap, bool> SetInputActionMaps()
        {
            var maps = new Dictionary<InputActionMap, bool>() {
                { InputActions.GamePlay, true },
                { InputActions.UIPlay, true }
            };
            return maps;
        }
    }

}
