using GoldSprite.GUtils;
using NUnit.Framework;
using UnityEngine;

namespace GoldSprite.UnityPlugins.EntitySystem2D.Tests {
    public class Test_ReflectionHelper {
        [Test]
        public void Test()
        {
            var obj = new MyObj();
            Debug.Log($"查找MyObj成员Name: {ReflectionHelper.GetField<string>(obj, "Name")}");
            Debug.Log($"查找MyObj的首位Vector2类型成员: {ReflectionHelper.GetField<Vector2>(obj)}");
        }

        public class MyObj
        {
            public string Name = "Zhang San";
            public Vector2 Pos = new Vector2(33, 0);
        }
    }
}
