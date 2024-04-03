using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LogTool {
    public static bool IsInit;
    public static SObject Options;
    public static void Init()
    {
        try {
            Options = Resources.Load<SObject>("LogToolData");
            if (Options == null) throw new Exception();
            IsInit = true;
            Debug.Log("�Ѽ���Ĭ������: ");
            Options.Reload = () => { Init(); };
        }
        catch (Exception) {
            //IsInit = true;
            Debug.Log("�Ҳ����κ�����, ���ֶ�����.");
        }
    }
    public static void NLog(object msg = default) => NLog("", msg);
    public static void NLog(string tag, object msg)
    {
        //
        if (!IsInit) Init();

        tag = string.IsNullOrEmpty(tag) ? "default" : tag;

        if (msg == default || !DisPlayLog(tag))
            return;

        var log = ""
            + "[" + tag + "] "
            + msg
            ;
        Debug.Log(log);
    }
    public static bool DisPlayLog(string tag)
    {
        if (Options == null) return true;
        var exist = Options.logdatas.TryGetValue(tag, out bool enable) && enable;
        if (!exist && Options.showInterceptMsg) NLog($"[tag:{tag}] �ѱ�����.");
        return exist;
    }
}
