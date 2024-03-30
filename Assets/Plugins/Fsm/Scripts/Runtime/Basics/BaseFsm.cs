

using System;
using UnityEngine;

namespace GoldSprite.Fsm {
    public class BaseFsm : Fsm {
        public new BaseProps Props { get; }
        protected BaseFsmCommandManager Cmd { get; }

        public BaseFsm(BaseProps props)
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
                //归一化
                if (dir.magnitude > 1) dir = dir.normalized;
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
