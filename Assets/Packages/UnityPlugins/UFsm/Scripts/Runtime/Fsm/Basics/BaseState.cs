using System;

namespace GoldSprite.UFsm {
    [Serializable]
    public class BaseState : State {
        public Enum AnimEnum { get; set; }
        public new BaseFsm Fsm { get; }
        public IBaseProps Props => Fsm.Props;

        public BaseState(BaseFsm fsm) : base(fsm) { Fsm = fsm; }

        public override string ToString()
        {
            return $"[{Props.Name}-{GetType().Name}]";
        }

        public override bool Enter() => false;
        public override bool Exit() => true;
    }
}
