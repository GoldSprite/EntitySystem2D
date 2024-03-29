namespace GoldSprite.Fsm {
    public interface IState {
        public bool Enter();
        public bool Exit();
        public void OnEnter();
        public void OnExit();
        public void Update();
        public void FixedUpdate();
    }
}
