using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldSprite.UnityTools.MyDict {

    [JsonObject]
    [Serializable]
    public class MyDict<K, V> : IEnumerable, IEnumerable<(K, V)> {
        public List<MyKVPair<K, V>> pairs = new List<MyKVPair<K, V>>();

        public bool ContainsKey(K key)
        {
            return pairs.Any(p => p.Key.Equals(key));
        }
        public bool TryGetValue(K key, out V value)
        {
            value = default(V);
            if (!ContainsKey(key)) return false;
            value = this[key];
            return true;
        }
        public bool ContainsValue(V val)
        {
            return pairs.Any(p => p.Value.Equals(val));
        }

        public void Add(K key, V val)
        {
            if (ContainsKey(key)) return;
            pairs.Add(new MyKVPair<K, V>() { Key = key, Value = val });
        }

        private MyKVPair<K, V> GetPair(K key)
        {
            return pairs.FirstOrDefault(p => p.Key.Equals(key));
        }

        public V Get(K key)
        {
            var pair = pairs.FirstOrDefault(p => p.Key.Equals(key));
            if (pair != null)
                return pair.Value;
            return default(V);
        }

        public int Count() { return pairs.Count; }

        private void SetPair(K key, V val)
        {
            var pair = pairs.FirstOrDefault(p => p.Key.Equals(key));
            if (pair != null)
                pair.Value = val;
        }

        public void Set(K key, V val)
        {
            SetPair(key, val);
        }

        public V this[K k] {
            get {
                if (!ContainsKey(k)) Add(k, default(V));
                return GetPair(k).Value;
            }
            set {
                if (!ContainsKey(k)) Add(k, value);
                SetPair(k, value);
            }
        }


        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("{\n");
            for (int i = 0; i < pairs.Count; i++)
                stringBuilder.Append("  ").Append(pairs[i]).Append((i == pairs.Count - 1 ? "" : ", \n"));
            stringBuilder.Append("\n}");
            return stringBuilder.ToString();
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return pairs.GetEnumerator();
        }

        IEnumerator<(K, V)> IEnumerable<(K, V)>.GetEnumerator()
        {
            foreach (var kvp in pairs) {
                yield return (kvp.Key, kvp.Value);
            }
        }

        public void Clear()
        {
            pairs.Clear();
        }


        [JsonIgnore]
        public ICollection<K> Keys => new KeyCollection(this);
        [JsonIgnore]
        public ICollection<V> Values => new ValueCollection(this);


        abstract class KVCollection<T> : ICollection<T> {
            protected MyDict<K, V> dict;
            public KVCollection(MyDict<K, V> dict) { this.dict = dict; }

            public int Count => dict.Count();

            public bool IsReadOnly => true;

            public void Add(T item)
            {
                throw new NotSupportedException("KVCollection is read-only.");
            }

            public void Clear()
            {
                throw new NotSupportedException("KVCollection is read-only.");
            }

            public bool Remove(T item)
            {
                throw new NotSupportedException("KVCollection is read-only.");
            }

            IEnumerator IEnumerable.GetEnumerator() { yield return this.GetEnumerator(); }

            public abstract bool Contains(T k);

            public abstract void CopyTo(T[] array, int arrayIndex);

            public abstract IEnumerator<T> GetEnumerator();
        }
        class KeyCollection : KVCollection<K> {
            public KeyCollection(MyDict<K, V> dict) : base(dict) { }

            public override bool Contains(K t) { return dict.ContainsKey(t); }

            public override void CopyTo(K[] array, int arrayIndex) { foreach (var (k, v) in dict) array[arrayIndex++] = k; }

            public override IEnumerator<K> GetEnumerator()
            {
                foreach (var (k, v) in dict)
                    yield return k;
            }
        }
        class ValueCollection : KVCollection<V> {
            public ValueCollection(MyDict<K, V> dict) : base(dict) { }

            public override bool Contains(V t) { return dict.ContainsValue(t); }

            public override void CopyTo(V[] array, int arrayIndex) { foreach (var (k, v) in dict) array[arrayIndex++] = v; }

            public override IEnumerator<V> GetEnumerator() { foreach (var (k, v) in dict) yield return v; }
        }
    }
}
