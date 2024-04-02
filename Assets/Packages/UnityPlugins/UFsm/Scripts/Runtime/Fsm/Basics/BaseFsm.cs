

using GoldSprite.UnityPlugins.MyAnimator;
using System;
using UnityEngine;

namespace GoldSprite.UFsm {
    public class BaseFsm : Fsm {
        public new IBaseProps Props { get; protected set; }
        public MyAnimator AnimCtrls { get; protected set; }
        protected BaseFsmCommandManager Cmd { get; private set; }

        public void InitFsm(IBaseProps props, MyAnimator animCtrls)
        {
            Props = props;
            AnimCtrls = animCtrls;
            Cmd = new BaseFsmCommandManager();
            InitStates();
            InitCommands();
        }

        protected virtual void InitStates()
        {
            InitState(new IdleState(this));
            AddState(new MoveState(this));
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
