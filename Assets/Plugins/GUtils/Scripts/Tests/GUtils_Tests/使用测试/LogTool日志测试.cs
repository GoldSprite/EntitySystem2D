using GoldSprite.GUtils;
using NUnit.Framework;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.TestTools;

public class LogTool日志测试
{
    [Test]
    public void 测试打印所有类型日志()
    {
        var msg = "这是一条日志.";
        LogTool.NLogMsg(msg);
        LogTool.NLogInfo(msg);
        LogTool.NLogDebug(msg);

        LogAssert.Expect(LogType.Warning, new Regex(msg));
        LogTool.NLogWarn(msg);

        LogAssert.Expect(LogType.Error, new Regex(msg));
        LogTool.NLogErr(msg);
    }

    [Test]
    public void LogAssertExample()
    {
        //预期会出现一条常规日志消息
        LogAssert.Expect(LogType.Log, "Log message");
        //预期会出现一条日志消息，如果没有以下行，
        //测试将失败
        Debug.Log("Log message");
        //输出错误日志
        Debug.LogError("Error message");
        //如果不做错误日志的预期，测试将失败
        LogAssert.Expect(LogType.Error, "Error message");
    }
}
