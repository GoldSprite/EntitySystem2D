namespace GoldSprite.Fsm {
    public abstract class BaseState : State {
        protected BaseProps Props { get; }

        public BaseState(BaseFsm fsm, BaseProps props) : base(fsm) { this.Props = props; }
    }
}
