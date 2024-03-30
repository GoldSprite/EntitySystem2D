using System;
using System.Collections.Generic;

namespace GoldSprite.Fsm {
    public class BaseFsmCommandManager {
        protected Dictionary<Enum, Delegate> commands = new();
        protected Dictionary<Enum, Delegate> listeners = new();

        public void RegisterCommand<T>(Enum cmd, Action<T> action)
        {
            commands[cmd] = action;
            listeners[cmd] = (Action<T>)((p) => { });
        }

        public void AddCommandListener<T>(Enum cmd, Action<T> action)
        {
            try {
                listeners[cmd] = Delegate.Combine(listeners[cmd], action);
            }
            catch (Exception) { }
        }

        public void Execute<T>(Enum cmd, T p)
        {
            if (!commands.TryGetValue(cmd, out Delegate dele)) return;
            dele?.DynamicInvoke(p);
            listeners[cmd]?.DynamicInvoke(p);
        }
    }

    public enum BaseFsmCommand {
        Idle, Move
    }
}