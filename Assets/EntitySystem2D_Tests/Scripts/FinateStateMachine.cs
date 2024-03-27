using System.Collections.Generic;
using System;
using UnityEngine;

namespace GoldSprite.UnityPlugins.EntitySystem2D.Tests {

    [Serializable]
    public class FinateStateMachine {
        public Dictionary<Type, IState> states = new();
        public IState currentState;
        public IState defaultState;

        public void InitState(IState state)
        {
            defaultState = currentState = state;
            states.Add(state.GetType(), state);
        }

        public void AddState(IState state)
        {
            states.Add(state.GetType(), state);
        }

        public void UpdateNextState()
        {
            var target = currentState;
            //遍历条件, 进入优先级最高的那个状态
            foreach (var (type, state) in states) {
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
}