using GoldSprite.UnityPlugins.MyInputSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GoldSprite.UnityPlugins.EntitySystem2D.Tests {

    public class TestEntityConstructor : EntitySystem {


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
        public float moveSpeed = 5.5f;
        public float jumpForce = 6f;
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
        [Tooltip("物理管理器")]
        public PhysicsManager physics;


        private void Awake()
        {
            //初始化属性器
            props.AddProp("Name", "佩茨");
            var rb = GetComponent<Rigidbody2D>();
            if (rb == null) throw new Exception($"找不到[Rigidbody2D]组件");
            props.AddProp("Rb", rb);
            var anims = GetComponent<Animator>();
            if (anims == null) throw new Exception($"找不到[Animator]组件");
            props.AddProp("Anims", anims);
            props.AddProp("MoveSpeed", moveSpeed);
            props.AddProp("JumpForce", jumpForce);

            //初始化输入器
            inputs.Awake();

            //初始化动画器
            var anims2 = props.GetProp<Animator>("Anims");
            animCtrls.SetAnims(anims2);

            //初始化物理器
            physics.Init();

            bevs.Init(this);
            //初始化行为状态列表
            bevs.AddBehaviour(new IdleBehaviour() { AnimName = "Idle" });
            bevs.AddBehaviour(new MoveBehaviour() { AnimName = "Run" }, 0);
            bevs.AddBehaviour(new JumpBehaviour() { AnimName="JumpBlend", AnimNames = new string[] { "JumpStart", "JumpUpper", "JumpTurnFall", "JumpFall" } });
        }


        private void Update()
        {
            fsm.Update();
            animCtrls.Update();
        }


        private void OnTriggerEnter2D(Collider2D collision) => physics.OnTriggerEnter2D?.Invoke(collision);
        private void OnTriggerExit2D(Collider2D collision) => physics.OnTriggerExit2D?.Invoke(collision);
    }


}
