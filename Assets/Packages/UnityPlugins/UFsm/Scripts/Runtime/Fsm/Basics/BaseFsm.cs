

using GoldSprite.UnityPlugins.MyAnimator;
using System;
using UnityEngine;

namespace GoldSprite.UFsm {
    public class BaseFsm : Fsm {
        public new IBaseProps Props { get; protected set; }
        public MyAnimator AnimCtrls { get; protected set; }
        protected BaseFsmCommandManager Cmd { get; private set; }

        public void InitFsm(IBaseProps props)
        {
            Props = props;
            Cmd = new BaseFsmCommandManager();
            InitStates();
            InitCommands();
        }

        protected virtual void InitStates()
        {
            InitState(new IdleState(this, Props));
            AddState(new MoveState(this, Props));
        }

        protected virtual void InitCommands()
        {
            Cmd.RegisterCommand<Vector2>(BaseFsmCommand.Move, (dir) => {
                Props.Direction = dir;
            });
        }

        public void Command(Enum cmd, object p)
        {
            Cmd.Execute(cmd, p);
        }

        public override string ToString()
        {
            return $"[{GetType().Name}-{Props.Name}]";
        }
    }
}
