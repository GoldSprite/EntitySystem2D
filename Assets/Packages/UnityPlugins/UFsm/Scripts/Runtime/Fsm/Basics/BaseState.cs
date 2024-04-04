using System;

namespace GoldSprite.UFsm {
    [Serializable]
    public class BaseState : State {
        public string AnimName { get; set; } = "";
        public new BaseFsm Fsm { get; }
        public IEntityProps Props => Fsm.Props;

        public BaseState(BaseFsm fsm) : base(fsm) { Fsm = fsm; }

        public override string ToString()
        {
            return $"[{Props.Name}-{GetType().Name}]";
        }

        public override bool Enter() => false;
        public override bool Exit() => true;

        public virtual void OnDrawGizmos() { }
    }
}
