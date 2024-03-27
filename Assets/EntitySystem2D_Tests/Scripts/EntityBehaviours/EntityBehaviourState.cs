using System;
using UnityEngine;

namespace GoldSprite.UnityPlugins.EntitySystem2D.Tests {

    public abstract class EntityBehaviourState : EntityBehaviour, IState {
        public EntitySystem ent;
        public string AnimName { get; set; } = "";
        public int Priority { get; set; }
        public bool StateSwitch { get; protected set; }


        public abstract bool Enter();
        public abstract bool Exit();
        public virtual void OnEnter() { }
        public virtual void OnExit() { }
        public virtual void Run() { }


        public override void Init(EntitySystem ent, int priority)
        {
            this.ent = ent;

            InitState();  //先初始化数据, 再加入状态机
            if (ent.fsm.currentState == null) ent.fsm.InitState(this);
            else ent.fsm.AddState(this, priority);
        }

        public abstract void InitState();

        public override string ToString()
        {
            return GetType().Name;
        }
    }
}