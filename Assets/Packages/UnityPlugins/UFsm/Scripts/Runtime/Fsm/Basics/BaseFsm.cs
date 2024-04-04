

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
            InitCommands();
            InitStates();
        }

        protected virtual void InitStates()
        {
            InitState(new IdleState(this));
            AddState(new MoveState(this));
        }

        protected virtual void InitCommands()
        {
            Cmd.RegisterCommand(BaseFsmCommand.Move, (Action<Vector2>)((dir) => {
                Props.Direction = dir;
            }));
            Cmd.RegisterCommand(BaseFsmCommand.Attack, (Action<bool>)((down) => {
                Props.AttackKey = down;
            }));
            Cmd.RegisterCommand(BaseFsmCommand.Hurt, (Action<IAttacker>)((attacker) => {
                if (Props.DeathKey) return;
                Props.HurtKey = true;
            }));
            Cmd.RegisterCommand(BaseFsmCommand.Turn, (Action<float>)((face) => {
                var sign = Math.Sign(face);
                if (sign==0) return;
                var ls = transform.localScale;
                ls.x = sign;
                transform.localScale = ls;
            }));
            Cmd.RegisterCommand(BaseFsmCommand.Death, (Action<bool>)((down) => {
                Props.DeathKey = down;
            }));
            Cmd.RegisterCommand(BaseFsmCommand.Jump, (Action<bool>)((down) => {
                Props.JumpKey = down;
            }));
            Cmd.RegisterCommand(BaseFsmCommand.MoveBoost, (Action<bool>)((down) => {
                if (Props.MoveBoostKeyType == IEntityProps.KeySwitchType.Key)
                    Props.MoveBoostKey = down;
                else
                if (Props.MoveBoostKeyType == IEntityProps.KeySwitchType.KeyDown && down)
                    Props.MoveBoostKey = !Props.MoveBoostKey;
            }));
        }

        public void Command(Enum cmd, params object[] p)
        {
            Cmd.Execute(cmd, p);
        }

        public void AddCommandListener(Enum cmd, Delegate act)
        {
            Cmd.AddCommandListener(cmd, act);
        }

        public override string ToString()
        {
            return $"[{GetType().Name}-{Props.Name}]";
        }

    }
}
