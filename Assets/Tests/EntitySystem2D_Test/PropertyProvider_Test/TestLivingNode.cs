using GoldSprite.UnityPlugins.EntitySystem2D;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLivingNode {

    [Test]
    public void TestAttack()
    {
        var props = new PropertyProvider();
        props.AddProp("Name", "Attacker");
        var attacker = new LivingNode();
        attacker.Init(props);

        var props2 = new PropertyProvider();
        props2.AddProp("Name", "Victim");
        var victim = new LivingNode();
        victim.Init(props2);


        attacker.Attack(victim);
        attacker.Attack(victim);
        attacker.Attack(victim);
        attacker.Attack(victim);
        attacker.Attack(victim);
        attacker.Attack(victim);
    }
}
