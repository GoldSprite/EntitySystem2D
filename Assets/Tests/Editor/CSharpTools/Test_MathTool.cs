using GoldSprite.GUtils;
using NUnit.Framework;
using UnityEngine;

namespace GoldSprite.UnityPlugins.EntitySystem2D.Tests {
    public class Test_MathTool {
        [Test]
        public void TestMathTool()
        {
            Debug.Log($"���16λGuid: {MathTool.NewCustomGuid()}");
            Debug.Log($"���12λGuid: {MathTool.NewCustomGuid(12)}");
        }
    }
}
