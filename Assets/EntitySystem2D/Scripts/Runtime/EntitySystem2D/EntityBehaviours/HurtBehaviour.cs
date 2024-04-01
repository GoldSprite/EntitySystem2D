using System;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

namespace GoldSprite.UnityPlugins.EntitySystem2D {
    public class HurtBehaviour : EntityBehaviourState {
        public Dictionary<Type, EntityBehaviourNode> hurtBehaviourNodes = new();

        public override bool Enter()
        {
            return hurtBehaviourNodes[typeof(HurtInputsBeNode)].Enter();
        }
        public override bool Exit()
        {
            return hurtBehaviourNodes[typeof(HurtAnimCtrlsBeNode)].Exit();
        }

        public override void InitState()
        {
            hurtBehaviourNodes.Add(typeof(HurtInputsBeNode), new HurtInputsBeNode(this, ent.inputs));
            //hurtBehaviourNodes.Add(typeof(HurtPropsBeNode), new HurtPropsBeNode(this, ent.props));
            hurtBehaviourNodes.Add(typeof(HurtAnimCtrlsBeNode), new HurtAnimCtrlsBeNode(this, ent.animCtrls) { AnimName = this.AnimName });
        }


        public override void OnEnter0()
        {
            Debug.Log("进入受伤状态");
            hurtBehaviourNodes[typeof(HurtInputsBeNode)].OnEnter();
            //hurtBehaviourNodes[typeof(HurtPropsBeNode)].OnEnter();
            hurtBehaviourNodes[typeof(HurtAnimCtrlsBeNode)].OnEnter();
        }

        public override void Run()
        {
        }
    }


    public abstract class EntityBehaviourNode : IState {
        public int Priority { get; set; }
        public bool OnEnterEnd { get; protected set; }
        public EntityBehaviourState Behaviour;
        public IEntityProvider provider { get; }
        public T Provider<T>() { return (T)(object)provider; }

        public EntityBehaviourNode(EntityBehaviourState behaviour, IEntityProvider provider)
        {
            this.Behaviour = behaviour;
            if(provider == null || !provider.Init()) {
                Debug.LogWarning($"[{behaviour.GetType().Name}-Init]: 传入提供器失败, 此模块相关功能自动关闭.");
                return;
            } else
                Debug.Log($"[{behaviour.GetType().Name}-Init]: 传入提供器[{provider.GetType().Name}]成功, 模块开启.");
        }

        public bool Enter()
        {
            if (provider == null) return false;
            return Enter0();
        }
        public virtual bool Enter0() { return false; }

        public bool Exit()
        {
            if (provider == null) return OnEnterEnd;
            return Exit0();
        }
        public virtual bool Exit0() { return false; }

        public void OnEnter()
        {
            if (provider != null) OnEnter0();
            OnEnterEnd = true;
        }
        public virtual void OnEnter0() { }

        public void OnExit()
        {
            OnEnterEnd = false;
            if (provider != null) OnExit0();
        }
        public virtual void OnExit0() { }

        public void Run()
        {
            if (provider == null) return;
            Run0();
        }
        public virtual void Run0() { }
    }


    public class HurtPropsBeNode : EntityBehaviourNode {
        public HurtPropsBeNode(EntityBehaviourState behaviour, PropertyProvider provider) : base(behaviour, provider) { }

        public override void OnEnter0()
        {

        }
    }

    public class HurtInputsBeNode : EntityBehaviourNode {
        public HurtInputsBeNode(EntityBehaviourState behaviour, InputProvider provider) : base(behaviour, provider) { }
        public override bool Enter0()
        {
            return Provider<InputProvider>().GetValue<bool>(InputProvider.VirtualKey.HurtKey);
        }
        public override void OnEnter0()
        {
            if (Provider<InputProvider>().GetValue<bool>(InputProvider.VirtualKey.HurtKey))
                Provider<InputProvider>().RaiseAction(InputProvider.VirtualKey.HurtKey, false);
        }
    }

    public class HurtAnimCtrlsBeNode : EntityBehaviourNode {
        public string AnimName;
        public HurtAnimCtrlsBeNode(EntityBehaviourState behaviour, AnimsProvider provider) : base(behaviour, provider) { }

        public override bool Exit0()
        {
            return Provider<AnimsProvider>().IsCurrentAnimEnd(AnimName);
        }
        public override void OnEnter0()
        {
            Provider<AnimsProvider>().PlayAnim(AnimName);
        }
    }
}