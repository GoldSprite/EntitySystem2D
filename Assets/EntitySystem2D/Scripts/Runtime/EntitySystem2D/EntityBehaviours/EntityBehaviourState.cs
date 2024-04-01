using System;
using UnityEngine;

namespace GoldSprite.UnityPlugins.EntitySystem2D {

    public abstract class EntityBehaviourState : EntityBehaviour, IState {
        public EntityBehaviourConstructor ent;
        public string AnimName { get; set; } = "";
        public int Priority { get; set; }
        public bool OnEnterEnd { get; protected set; }
        public bool StateSwitch { get; protected set; }


        public abstract bool Enter();
        public abstract bool Exit();
        public void OnEnter()
        {
            OnEnter0();
            OnEnterEnd = true;
        }
        public virtual void OnEnter0() { }
        public void OnExit()
        {
            OnEnterEnd = false;
            OnExit0();
        }
        public virtual void OnExit0() { }
        public virtual void Run() { }


        public override void Init(EntityBehaviourConstructor ent, int priority)
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