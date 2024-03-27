using GoldSprite.UnityPlugins.MyInputSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GoldSprite.UnityPlugins.EntitySystem2D.Tests {

    public class TestEntityConstructor : EntitySystem {
        public float moveSpeed = 5.5f;

        public override void InitPropertyManager()
        {
            //if (props == null) props = new PropertyManager();
            base.InitPropertyManager();

            props.AddProp("Name", "佩茨");

            var rb = GetComponent<Rigidbody2D>();
            if (rb == null) throw new Exception($"找不到[Rigidbody2D]组件");
            props.AddProp("Rb", rb);

            var anims = GetComponent<Animator>();
            if (anims == null) throw new Exception($"找不到[Animator]组件");
            props.AddProp("Anims", anims);

            props.AddProp("MoveSpeed", moveSpeed);
        }

        #region TestAddProps

        [ContextMenu("AddRb")]
        public void AddRb()
        {
            var rb = GetComponent<Rigidbody2D>();
            if (rb == null) throw new Exception($"找不到Rigidbody2D组件");
            if (!props.Exists("Rb")) props.AddProp("Rb", rb);
            else props.SetProp("Rb", rb);
        }

        #endregion
    }


    public class EntitySystem : MonoBehaviour {
        public string Name = "AA";
        [Header("Constructor")]
        [Tooltip("实体构造器")]
        public EntityBehaviourConstructor bevs;
        [Header("Components")]
        [Tooltip("属性管理者")]
        public PropertyManager props;
        [Tooltip("状态器")]
        public FinateStateMachine fsm;
        [Tooltip("输入提供者")]
        public InputProvider inputs;
        [Tooltip("动画控制器")]
        public AnimManager animCtrls;

        private void Awake()
        {
            InitPropertyManager();
            if (fsm == null) fsm = new FinateStateMachine();
            InitInputProvider();
            InitAnimatorManager();  //这里要在前面
            InitEntityBehaviours();
        }

        private void InitAnimatorManager()
        {
            if (animCtrls == null) animCtrls = new AnimManager();
            var anims = props.GetProp<Animator>("Anims");
            animCtrls.SetAnims(anims);
        }

        private void InitInputProvider()
        {
            if (inputs == null) inputs = new InputProvider();
            inputs.Awake();
        }

        public virtual void InitPropertyManager()
        {
            if (props == null) props = new PropertyManager();
        }

        private void InitEntityBehaviours()
        {
            if (bevs == null) bevs = new EntityBehaviourConstructor(this);
            bevs.AddBehaviour(new IdleBehaviour() { AnimName = "Idle" });
            bevs.AddBehaviour(new MoveBehaviour() { AnimName = "Run" }, 0);

        }


        private void Update()
        {
            fsm.Update();
            animCtrls.Update();
        }
    }


}
