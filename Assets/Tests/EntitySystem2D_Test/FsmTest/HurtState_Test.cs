using GoldSprite.UnityPlugins.EntitySystem2D;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class HurtState_Test {
    [Test]
    public static void Test()
    {
        var props = new PropertyProvider();
        {
            props.AddProp("Name", "TA");

            var living = new LivingNode();
            living.Init(props);
            props.AddProp("Living", living);
        }


        var fsm = new FinateStateMachine() { debugLog = true };
        var state = new IdleState();
        //state.inputs = 
        state.InitState();
    }
}
