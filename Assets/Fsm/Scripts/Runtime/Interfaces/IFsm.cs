namespace GoldSprite.Fsm {
    public interface IFsm {
        public IState CState { get; }
        public IState DefaultState { get; }

        public bool UpdateNextState();
        public T GetState<T>() where T : IState;
    }
}
