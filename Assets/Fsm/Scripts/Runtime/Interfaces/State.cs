namespace GoldSprite.Fsm {
    public abstract class State : IState {
        public IFsm Fsm { get; }
        public State(IFsm fsm) => Fsm = fsm;
        public int Priority { get; set; }
        public virtual bool CanTranSelf { get; protected set; }
        private bool OnEnterEnd;

        public abstract bool Enter();
        public bool Exit() { if (!OnEnterEnd) return false; return Exit0(); }
        public abstract bool Exit0();
        public virtual void OnEnter() { OnEnter0(); OnEnterEnd = true; }
        public virtual void OnEnter0() { }
        public virtual void OnExit() { OnEnterEnd = false; OnExit0(); }
        public virtual void OnExit0() { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }

        public override string ToString()
        {
            return $"[{GetType().Name}]";
        }
    }
}
