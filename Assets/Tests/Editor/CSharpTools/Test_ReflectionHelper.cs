using GoldSprite.GUtils;
using NUnit.Framework;
using System;
using UnityEngine;

namespace GoldSprite.GUtils.Tests {
    public class Test_ReflectionHelper {
        [Test]
        public void 基本查找()
        {
            var obj = new MyObj();
            Debug.Log($"有路径泛型 查找Vector2成员MyObj.BodyPos: {ReflectionHelper.GetField<Vector2>(obj, "BodyPos")}");
            Debug.Log($"无路径泛型 查找string成员MyObj: {ReflectionHelper.GetField<string>(obj)}");
            Debug.Log($"有路径object型 查找成员MyObj.BodyPos: {ReflectionHelper.GetField(obj, "BodyPos")}");

            obj.obj = new MyObj.MyObjOb();
            var type = ReflectionHelper.GetFieldInfo(obj, "obj.Name").FieldType;
            Debug.Log($"有路径 查找成员反射信息MyObj.obj.Name: {type}");
            Assert.AreEqual(typeof(string), type);

            Debug.Log($"有路径 查找成员反射类型MyObj.obj.Name: {ReflectionHelper.GetFieldType(obj, "obj.Name").Name}");


        }


        [Test]
        public void 异常查找()
        {
            var obj = new MyObj();
            TestDelegate dele = () => { var type = ReflectionHelper.GetFieldInfo(obj, "obj.Name"); };
            var ex = Assert.Throws<Exception>(dele);
            Assert.AreEqual("找不到该路径成员信息.", ex.Message);
        }


        [Test]
        public void 继承成员查找()
        {
            var obj = new MyObj();
            var field = ReflectionHelper.GetField<string>(obj, "obj2.BaseObj.BaseName");
            Assert.AreEqual("Zhang San Parent2", field);
        }


        public class MyObj
        {
            public string Name = "Zhang San";
            public Vector2 Pos = new Vector2(33, 0);
            public MyObjOb obj;
            public MyObjOb obj2 = new MyObjOb();

            public class MyObjOb : MyObjobBase {
                public string Name = "Zhang San's Son";
            }

            public class MyObjobBase
            {
                public BaseObj2 BaseObj = new BaseObj2();
            }
            public class BaseObj2 {
                public string BaseName = "Zhang San Parent2";
            }
        }
    }
}
