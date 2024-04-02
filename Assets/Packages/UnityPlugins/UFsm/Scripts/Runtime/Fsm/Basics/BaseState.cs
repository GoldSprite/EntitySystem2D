namespace GoldSprite.UFsm {
    public abstract class BaseState : State {
        public new BaseFsm Fsm { get; }
        protected IBaseProps Props { get; }

        public BaseState(BaseFsm fsm, IBaseProps props) : base(fsm) { this.Props = props; }

        public override string ToString()
        {
            return $"[{Props.Name}-{GetType().Name}]";
        }
    }
}
