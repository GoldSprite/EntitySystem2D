
using System;
using System.Text;

namespace GoldSprite.UnityTools.MyDict {

    [Serializable]
    public class MyKVPair<K, V> {
        public K Key = default;
        public V Value = default;

        public MyKVPair() { }
        public MyKVPair(K key, V value)
        {
            Key = key;
            Value = value;
        }


        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append('[');
            if (Key != null) {
                stringBuilder.Append(Key.ToString());
            }

            stringBuilder.Append(", ");
            if (Value != null) {
                stringBuilder.Append(Value.ToString());
            }

            stringBuilder.Append(']');
            return stringBuilder.ToString();
        }
    }


}