using GoldSprite.GUtils;
using NUnit.Framework;
using UnityEngine;

namespace GoldSprite.GUtils.Tests {
    public class Test_ReflectionHelper {
        [Test]
        public void Test()
        {
            var obj = new MyObj();
            Debug.Log($"��·������ ����Vector2��ԱMyObj.Pos: {ReflectionHelper.GetField<Vector2>(obj, "Pos")}");
            Debug.Log($"��·������ ����string��ԱMyObj: {ReflectionHelper.GetField<string>(obj)}");
            Debug.Log($"��·��object�� ���ҳ�ԱMyObj.Pos: {ReflectionHelper.GetField(obj, "Pos")}");

            Debug.Log($"��·�� ���ҳ�Ա������ϢMyObj.obj.Name: {ReflectionHelper.GetFieldInfo(obj, "obj.Name").GetType()}");
            Debug.Log($"��·�� ���ҳ�Ա��������MyObj.obj.Name: {ReflectionHelper.GetFieldType(obj, "obj.Name").Name}");
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
