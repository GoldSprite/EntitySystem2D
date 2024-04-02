using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLogs
{
    [Test]
    public void TestLog()
    {
        LogTool.NLog(msg:"你好");
        Debug.Log("已通过.");
        LogTool.NLog("kk", "你好");
        LogTool.NLog("MyInputs", "输入器");
    }
}
