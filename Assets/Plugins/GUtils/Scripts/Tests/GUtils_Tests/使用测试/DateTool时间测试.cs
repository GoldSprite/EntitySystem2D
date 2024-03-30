using GoldSprite.GUtils;
using NUnit.Framework;
using UnityEngine;

public class DateTool时间测试
{
    [Test]
    public void 测试格式化时间()
    {
        Debug.Log("当前格式化时间: "+DateTool.CurrentFormatTime());
        Debug.Log("使用给定的毫秒数进行格式化: " + DateTool.FormatTimeByMillis(10000));
        Debug.Log("使用当前毫秒数进行格式化: " + DateTool.FormatTimeByMillis(DateTool.CurrentTimeMillis()));
    }
}
