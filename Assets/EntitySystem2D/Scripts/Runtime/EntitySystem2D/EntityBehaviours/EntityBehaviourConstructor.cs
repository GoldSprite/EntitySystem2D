using System;
using System.Collections.Generic;
using UnityEngine;

namespace GoldSprite.UnityPlugins.EntitySystem2D {
    [Serializable]
    public class EntityBehaviourConstructor : IEntityProvider {
        public EntitySystem ent;
        [Tooltip("属性管理者")]
        public PropertyProvider props;
        [Tooltip("状态器")]
        public FinateStateMachine fsm;
        [Tooltip("输入提供者")]
        public InputProvider inputs;
        [Tooltip("动画控制器")]
        public AnimsProvider animCtrls;
        [Tooltip("物理管理器")]
        public PhysicsManager physics;
        [Tooltip("事件管理器")]
        public EventManager events;

        public void AddBehaviour(EntityBehaviour bev, int priority = 1)
        {
            //if (ent.fsm.currentState == null) priority = 0;
            bev.Init(this, priority);
        }

        public bool Init()
        {
            var msgs = new List<string>();
            if (props == null) msgs.Add("未提供属性管理者.");
            else if (!props.Init()) { msgs.Add("属性管理者初始化失败."); props = null; }

            if (fsm == null) msgs.Add("未提供状态器.");
            else if (!fsm.Init()) { msgs.Add("状态器初始化失败."); fsm = null; }

            if (inputs == null) msgs.Add("未提供输入提供者.");
            else if (!inputs.Init()) { msgs.Add("输入提供者初始化失败."); inputs = null; }

            if (animCtrls == null) msgs.Add("未提供动画控制器.");
            else if (!animCtrls.Init()) { msgs.Add("动画控制器初始化失败."); animCtrls = null; }

            if (physics == null) msgs.Add("未提供物理管理器.");
            else if (!physics.Init()) { msgs.Add("物理管理器初始化失败."); physics = null; }

            if (events == null) msgs.Add("未提供事件管理器.");
            else if (!events.Init()) { msgs.Add("事件管理器初始化失败."); events = null; }

            return IEntityProvider.PrintInitLog(this, msgs);
        }
    }


    //public class BasicEntityBehaviourConstructor : EntityBehaviourConstructor {
    //    public Rigidbody2D rb;

    //    public BasicEntityBehaviourConstructor(EntitySystem ent) : base(ent) { }
    //}
}