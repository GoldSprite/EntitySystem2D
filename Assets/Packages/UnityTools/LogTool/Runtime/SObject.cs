using GoldSprite.UnityTools.MyDict;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SObject : ScriptableObject
{
    public Action Reload;
    [Tooltip("是否显示被拦截日志")] public bool showInterceptMsg = true;
    public MyDict<string, List<string>> logdatas;


    [ContextMenu("重加载")]
    public void InitLogData()
    {
        Reload?.Invoke();
    }
}
