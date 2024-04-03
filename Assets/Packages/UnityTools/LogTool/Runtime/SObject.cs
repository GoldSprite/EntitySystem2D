using GoldSprite.UnityTools.MyDict;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SObject : SerializedScriptableObject
{
    public Action Reload;
    [Tooltip("是否显示被拦截日志")] public bool showInterceptMsg = true;
    [NonSerialized, OdinSerialize]
    public Dictionary<string, bool> logdatas;


    [ContextMenu("重加载")]
    public void InitLogData()
    {
        Reload?.Invoke();
    }
}
