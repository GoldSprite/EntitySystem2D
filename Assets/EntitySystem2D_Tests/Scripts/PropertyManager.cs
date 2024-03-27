using System.Collections.Generic;
using System;

namespace GoldSprite.UnityPlugins.EntitySystem2D.Tests {
    [Serializable]
    public class PropertyManager {
        public Dictionary<string, object> pairs = new();


        public T GetProp<T>(string key)
        {
            if (!pairs.ContainsKey(key)) return default(T);
            return (T)pairs[key];
        }

        public void AddProp(string key, object val)
        {
            if (!pairs.ContainsKey(key)) pairs.Add(key, val);
        }

        public void SetProp(string key, object val)
        {
            if (pairs.ContainsKey(key)) pairs[key] = val;
        }


    }
}