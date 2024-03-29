

using System;

namespace GoldSprite.Fsm {
    public class BaseFsm : Fsm {
        protected new BaseProps Props { get; }

        public BaseFsm(BaseProps props)
        {
            Props = props;

            InitStates();
        }

        protected virtual void InitStates()
        {
            InitState(new IdleState(this, Props));
            AddState(new MoveState(this, Props));
        }

        public override string ToString()
        {
            return $"[{GetType().Name}-{Props.Name}]";
        }
    }
}
