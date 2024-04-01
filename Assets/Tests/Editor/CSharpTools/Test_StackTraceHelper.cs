using GoldSprite.GUtils;
using NUnit.Framework;
using System;
using UnityEngine;

namespace GoldSprite.UnityPlugins.EntitySystem2D.Tests {
    public class Test_StackTraceHelper {
        [Test]
        public void Test()
        {
            new MyObj();
            new MyObj2();
        }

        public class MyObj
        {
            public MyObj()
            {
                InvokeMethodA();
            }

            public void InvokeMethodA()
            {

                Debug.Log($"获取调用者类名通过实例: {StackTraceHelper.GetStackAboveClassName(this)}");
            }
        }
        public class MyObj2
        {
            public MyObj2()
            {
                InvokeMethodA();
            }

            public void InvokeMethodA()
            {
                InvokeMethodB();
            }

            private void InvokeMethodB()
            {
                Debug.Log($"获取调用者类名通过类型: {StackTraceHelper.GetStackAboveClassName(typeof(MyObj2))}");
            }
        }
    }
}
