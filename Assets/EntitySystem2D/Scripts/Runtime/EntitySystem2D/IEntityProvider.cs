using System;
using System.Collections.Generic;
using UnityEngine;

namespace GoldSprite.UnityPlugins.EntitySystem2D {
    public interface IEntityProvider {
        public bool Init();

        protected static bool PrintInitLog(IEntityProvider instance, List<string> msgs)
        {
            var prefix = $"[{instance.GetType().Name}-Init]: ";
            if (msgs.Count > 0) foreach (var msg in msgs) Debug.LogWarning(prefix + msg);
            return msgs.Count == 0;
        }
    }
}