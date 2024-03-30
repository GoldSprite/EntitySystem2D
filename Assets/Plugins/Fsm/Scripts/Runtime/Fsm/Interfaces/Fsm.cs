using System;
using System.Collections.Generic;
using UnityEngine.Networking.Types;

namespace GoldSprite.Fsm {
    public class Fsm : IFsm {
        protected Dictionary<Type, IState> states = new();
        public IState CState { get; protected set; }
        public IState DefaultState { get; protected set; }
        public int LastPriority { get; private set; }
        public IProps Props { get; }


        protected void InitState(IState state)
        {
            DefaultState = CState = state;
            AddState(state, 0);
        }

        protected void AddState(IState state, int priority = 1)
        {
            states.Add(state.GetType(), state);
            LastPriority += priority;
            state.Priority = LastPriority;
        }

        public void Start()
        {
            CState.OnEnter();
        }


        public bool UpdateNextState()
        {
            var targetState = CState;
            foreach (var state in states.Values) {
                if (EnterState(state, targetState)) targetState = state;
            }
            var change = targetState != CState;
            if (!change && CState.Exit()) { targetState = DefaultState; change = true; }
            if (change) OnEnterState(targetState);
            return change;
        }

        protected bool EnterState(IState target, IState current)
        {
            if ((target == CState && !target.CanTranSelf) || current.Priority > target.Priority) return false;
            return target.Enter();
        }

        protected void OnEnterState(IState state)
        {
            CState.OnExit();
            CState = state;
            CState.OnEnter();
        }

        public T GetState<T>() where T : IState
        {
            if (states.TryGetValue(typeof(T), out IState state)) return (T)state;
            return default;
        }

        public void Update()
        {
            UpdateNextState();
            CState.Update();
        }

        public void FixedUpdate()
        {
            CState.FixedUpdate();
        }
    }
}
