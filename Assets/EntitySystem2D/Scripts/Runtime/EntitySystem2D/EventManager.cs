
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

namespace GoldSprite.UnityPlugins.EntitySystem2D {
    /// <summary>
    /// 事件管理器: <para/>
    /// 用于注册, 以及订阅监听已注册事件, 和事件派发
    /// </summary>
    [Serializable]
    public class EventManager : IEntityProvider {
        Dictionary<Enum, Delegate> events = new();

        public virtual void RegisterEvent(Enum key, Delegate dele)
        {
            if (events.ContainsKey(key)) return;

            events[key] = dele;
        }

        public virtual void AddEventListener<T>(Enum key, T dele) where T : Delegate
        {
            if (!events.ContainsKey(key)) {
                RegisterEvent(key, dele);
                return;
            }

            events[key] = Delegate.Combine(events[key], dele);
        }

        public virtual void RemoveEventListener<T>(Enum key, T dele) where T : Delegate
        {
            if (!events.ContainsKey(key)) return;

            events[key] = Delegate.Remove(events[key], dele);
        }


        public virtual object RaiseEvent(Enum key, params object[] objs)
        {
            if (!events.ContainsKey(key)) return null;

            return events[key]?.DynamicInvoke(objs);
        }

        public bool Init()
        {
            var msgs = new List<string>();
            //if (events.Count == 0) msgs.Add("事件表为空, 将不会触发任何事件.");
            return IEntityProvider.PrintInitLog(this, msgs);
        }
    }
}