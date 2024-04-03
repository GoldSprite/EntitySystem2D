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
    [Tooltip("�Ƿ���ʾ��������־")] public bool showInterceptMsg = true;
    [NonSerialized, OdinSerialize]
    public Dictionary<string, bool> logdatas;


    [ContextMenu("�ؼ���")]
    public void InitLogData()
    {
        Reload?.Invoke();
    }
}
