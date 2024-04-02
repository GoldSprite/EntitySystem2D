using NUnit.Framework;
using UnityEngine;
using GoldSprite.UnityTools.MyDict;
using System;
using Newtonsoft.Json;

namespace GoldSprite.UnityTools.MyDict.Tests {
    public class TestKVPair序列化Json22 {
        [Test]
        public void 多重嵌套MyKVPair序列化反序列化()
        {
            var a = new MyDict<string, MyObj>() {
                pairs = new() { new MyKVPair<string, MyObj>() { Key = "kisjfdi", Value = new MyObj() } }
            };
            var json = JsonConvert.SerializeObject(a, Formatting.Indented);
            Debug.Log("序列化成功: " + json);
            var deA = JsonConvert.DeserializeObject<MyDict<string, MyObj>>(json);
            Debug.Log("反序列化成功: " + deA);

        }

        [Test]
        public void MyKVPair与Dictionary互转序列化反序列化()
        {

        }

        [Serializable]
        public class MyObj {
            public string Name = "张三";
            public int Age = 36;
        }
    }

}
