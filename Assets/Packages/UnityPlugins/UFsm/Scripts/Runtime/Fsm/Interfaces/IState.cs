namespace GoldSprite.UFsm {
    public interface IState {
        public int Priority { get; set; }
        public bool CanTranSelf { get; }

        public bool Enter();
        public bool Exit();
        public void OnEnter();
        public void OnExit();
        public void Update();
        public void FixedUpdate();
        public void UpdateCondition();
    }
}
