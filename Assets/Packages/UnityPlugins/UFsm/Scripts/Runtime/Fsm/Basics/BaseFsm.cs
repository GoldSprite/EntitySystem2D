

using GoldSprite.UnityPlugins.MyAnimator;
using System;
using UnityEngine;

namespace GoldSprite.UFsm {
    public class BaseFsm : Fsm {
        public new IEntityProps Props { get; protected set; }
        public MyAnimator AnimCtrls { get; protected set; }
        protected BaseFsmCommandManager Cmd { get; set; }

        public void InitFsm(IEntityProps props, MyAnimator animCtrls)
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
            Cmd.RegisterCommand<bool>(BaseFsmCommand.Attack, (down) => {
                Props.AttackKey = down;
            });
            Cmd.RegisterCommand<bool>(BaseFsmCommand.Hurt, (down) => {
                Props.HurtKey = down;
            });
            Cmd.RegisterCommand<bool>(BaseFsmCommand.Death, (down) => {
                Props.DeathKey = down;
            });
            Cmd.RegisterCommand<bool>(BaseFsmCommand.Jump, (down) => {
                Props.JumpKey = down;
            });
            Cmd.RegisterCommand<bool>(BaseFsmCommand.MoveBoost, (down) => {
                if (Props.MoveBoostKeyType == IEntityProps.KeySwitchType.Key)
                    Props.MoveBoostKey = down;
                else
                if (Props.MoveBoostKeyType == IEntityProps.KeySwitchType.KeyDown && down)
                    Props.MoveBoostKey = !Props.MoveBoostKey;
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
