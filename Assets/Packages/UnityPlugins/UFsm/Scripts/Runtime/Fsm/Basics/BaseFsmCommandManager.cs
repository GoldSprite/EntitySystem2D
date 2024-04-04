using System;
using System.Collections.Generic;

namespace GoldSprite.UFsm {
    public class BaseFsmCommandManager {
        protected Dictionary<Enum, Delegate> commands = new();

        public void RegisterCommand(Enum cmd, Delegate action)
        {
            commands[cmd] = action;
        }

        public void AddCommandListener(Enum cmd, Delegate action)
        {
            //可能会抛异常
            commands[cmd] = Delegate.Combine(commands[cmd], action);
        }

        public void Execute(Enum cmd, params object[] p)
        {
            if (!commands.TryGetValue(cmd, out Delegate dele)) return;
            dele?.DynamicInvoke(p);
        }
    }

    public enum BaseFsmCommand {
        Idle, Move, Attack, Hurt, Death,
        Jump,
        MoveBoost,
        Turn
    }
}