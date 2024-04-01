using GoldSprite.GUtils;
using NUnit.Framework;
using UnityEngine;

namespace GoldSprite.UnityPlugins.EntitySystem2D.Tests {
    public class Test_MathTool {
        [Test]
        public void TestMathTool()
        {
            Debug.Log($"随机16位Guid: {MathTool.NewCustomGuid()}");
            Debug.Log($"随机12位Guid: {MathTool.NewCustomGuid(12)}");
        }
    }
}
