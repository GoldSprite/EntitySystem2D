using GoldSprite.GUtils;
using NUnit.Framework;
using System;
using UnityEngine;

namespace GoldSprite.GUtils.Tests {
    public class Test_ReflectionHelper {
        [Test]
        public void ��������()
        {
            var obj = new MyObj();
            Debug.Log($"��·������ ����Vector2��ԱMyObj.BodyPos: {ReflectionHelper.GetField<Vector2>(obj, "BodyPos")}");
            Debug.Log($"��·������ ����string��ԱMyObj: {ReflectionHelper.GetField<string>(obj)}");
            Debug.Log($"��·��object�� ���ҳ�ԱMyObj.BodyPos: {ReflectionHelper.GetField(obj, "BodyPos")}");

            obj.obj = new MyObj.MyObjOb();
            var type = ReflectionHelper.GetFieldInfo(obj, "obj.Name").FieldType;
            Debug.Log($"��·�� ���ҳ�Ա������ϢMyObj.obj.Name: {type}");
            Assert.AreEqual(typeof(string), type);

            Debug.Log($"��·�� ���ҳ�Ա��������MyObj.obj.Name: {ReflectionHelper.GetFieldType(obj, "obj.Name").Name}");


        }


        [Test]
        public void �쳣����()
        {
            var obj = new MyObj();
            TestDelegate dele = () => { var type = ReflectionHelper.GetFieldInfo(obj, "obj.Name"); };
            var ex = Assert.Throws<Exception>(dele);
            Assert.AreEqual("�Ҳ�����·����Ա��Ϣ.", ex.Message);
        }


        [Test]
        public void �̳г�Ա����()
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
