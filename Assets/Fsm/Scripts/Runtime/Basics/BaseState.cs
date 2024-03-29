namespace GoldSprite.Fsm {
    public abstract class BaseState : State {
        public new BaseFsm Fsm { get; }
        protected BaseProps Props { get; }

        public BaseState(BaseFsm fsm, BaseProps props) : base(fsm) { this.Props = props; }

        public override string ToString()
        {
            return $"[{GetType().Name}-{Props.Name}]";
        }
    }
}
