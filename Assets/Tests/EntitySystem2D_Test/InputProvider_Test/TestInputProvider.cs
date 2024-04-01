using GoldSprite.UnityPlugins.EntitySystem2D;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TestInputProvider {

    [Test]
    public static void TestInputProviderInit()
    {
        var inputs = new InputProvider();
        inputs.Awake();
        foreach (var (k, v) in inputs.keyValues) Debug.Log("模拟键: "+k);
    }
}
