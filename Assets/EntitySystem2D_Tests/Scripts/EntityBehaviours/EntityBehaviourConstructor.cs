using System;
using UnityEngine;

namespace GoldSprite.UnityPlugins.EntitySystem2D.Tests {
    public class EntityBehaviourConstructor {
        public EntitySystem ent;


        public EntityBehaviourConstructor(EntitySystem ent) => this.ent = ent;

        public void AddBehaviour(EntityBehaviour bev, int priority = 1)
        {
            //if (ent.fsm.currentState == null) priority = 0;
            bev.Init(ent, priority);
        }
    }


    //public class BasicEntityBehaviourConstructor : EntityBehaviourConstructor {
    //    public Rigidbody2D rb;

    //    public BasicEntityBehaviourConstructor(EntitySystem ent) : base(ent) { }
    //}
}