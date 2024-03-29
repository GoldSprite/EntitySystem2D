using System;
using System.Collections.Generic;

namespace GoldSprite.Fsm {
    public class BaseFsm : IFsm {
        private Dictionary<Type, IState> states = new();
        public IState CState { get; }
        public IState DefaultState { get; }
        protected IProps Props { get; }

        public bool UpdateNextState()
        {
            foreach(var state in states.Values) {
                if (state.Enter()) {
                    state.OnEnter();
                    return true;
                }
            }
            return false;
        }

        public T GetState<T>() where T : IState
        {
            if(states.TryGetValue(typeof(T), out IState state)) return (T)state;
            return default(T);
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
