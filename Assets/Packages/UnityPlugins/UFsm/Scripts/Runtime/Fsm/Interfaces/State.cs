namespace GoldSprite.UFsm {
    public abstract class State : IState {
        public IFsm Fsm { get; }
        public State(IFsm fsm) => Fsm = fsm;
        public int Priority { get; set; }
        public virtual bool CanTranSelf { get; protected set; }

        public abstract bool Enter();
        public abstract bool Exit();
        public virtual void OnEnter() { }
        public virtual void OnExit() { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }
        public virtual void UpdateCondition() { }

        public override string ToString()
        {
            return $"[{GetType().Name}]";
        }
    }
}
