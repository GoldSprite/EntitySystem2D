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

            props.AddProp("Name", "���");

            var rb = GetComponent<Rigidbody2D>();
            if (rb == null) throw new Exception($"�Ҳ���[Rigidbody2D]���");
            props.AddProp("Rb", rb);

            var anims = GetComponent<Animator>();
            if (anims == null) throw new Exception($"�Ҳ���[Animator]���");
            props.AddProp("Anims", anims);

            props.AddProp("MoveSpeed", moveSpeed);
        }

        #region TestAddProps

        [ContextMenu("AddRb")]
        public void AddRb()
        {
            var rb = GetComponent<Rigidbody2D>();
            if (rb == null) throw new Exception($"�Ҳ���Rigidbody2D���");
            if (!props.Exists("Rb")) props.AddProp("Rb", rb);
            else props.SetProp("Rb", rb);
        }

        #endregion
    }


    public class EntitySystem : MonoBehaviour {
        public string Name = "AA";
        [Header("Constructor")]
        [Tooltip("ʵ�幹����")]
        public EntityBehaviourConstructor bevs;
        [Header("Components")]
        [Tooltip("���Թ�����")]
        public PropertyManager props;
        [Tooltip("״̬��")]
        public FinateStateMachine fsm;
        [Tooltip("�����ṩ��")]
        public InputProvider inputs;
        [Tooltip("����������")]
        public AnimManager animCtrls;

        private void Awake()
        {
            InitPropertyManager();
            if (fsm == null) fsm = new FinateStateMachine();
            InitInputProvider();
            InitAnimatorManager();  //����Ҫ��ǰ��
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
