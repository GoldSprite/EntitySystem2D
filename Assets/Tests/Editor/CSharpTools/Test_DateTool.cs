using GoldSprite.GUtils;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoldSprite.GUtils.Tests {
    public class Test_DateTool {
        [Test]
        public void TestDateTool()
        {
            Debug.Log($"��ǰϵͳ������: {DateTool.CurrentTimeMillis()}");
            Debug.Log($"��ǰ��ʽ��ʱ��: {DateTool.CurrentFormatTime()}");
            Debug.Log($"ָ����������ʽ��ʱ��: {DateTool.FormatTimeByMillis(1000000)}");
        }
    }
}
