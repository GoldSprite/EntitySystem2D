using GoldSprite.GUtils;
using NUnit.Framework;
using UnityEngine;

namespace GoldSprite.GUtils.Tests {
    public class Test_ReflectionHelper {
        [Test]
        public void Test()
        {
            var obj = new MyObj();
            Debug.Log($"有路径泛型 查找Vector2成员MyObj.Pos: {ReflectionHelper.GetField<Vector2>(obj, "Pos")}");
            Debug.Log($"无路径泛型 查找string成员MyObj: {ReflectionHelper.GetField<string>(obj)}");
            Debug.Log($"有路径object型 查找成员MyObj.Pos: {ReflectionHelper.GetField(obj, "Pos")}");

            Debug.Log($"有路径 查找成员反射信息MyObj.obj.Name: {ReflectionHelper.GetFieldInfo(obj, "obj.Name").GetType()}");
            Debug.Log($"有路径 查找成员反射类型MyObj.obj.Name: {ReflectionHelper.GetFieldType(obj, "obj.Name").Name}");
        }

        public class MyObj
        {
            public string Name = "Zhang San";
            public Vector2 Pos = new Vector2(33, 0);
            public MyObjOb obj;

            public class MyObjOb {
                public string Name = "Zhang San's Son";
            }
        }
    }
}
