using GoldSprite.GUtils;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoldSprite.UnityPlugins.EntitySystem2D.Tests {
    public class Test_DateTool {
        [Test]
        public void TestDateTool()
        {
            Debug.Log($"当前系统毫秒数: {DateTool.CurrentTimeMillis()}");
            Debug.Log($"当前格式化时间: {DateTool.CurrentFormatTime()}");
            Debug.Log($"指定毫秒数格式化时间: {DateTool.FormatTimeByMillis(1000000)}");
        }
    }
}
