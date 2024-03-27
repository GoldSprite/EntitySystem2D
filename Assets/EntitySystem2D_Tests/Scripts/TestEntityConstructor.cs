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
            if (rb == null) throw new Exception($"�Ҳ���Rigidbody2D���");
            if (!props.Exists("Rb")) props.AddProp("Rb", rb);
            else props.SetProp("Rb", rb);
        }

        #endregion
    }


    public class EntitySystem : MonoBehaviour {
        public float moveSpeed = 5.5f;
        public float jumpForce = 6f;
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
        [Tooltip("���������")]
        public PhysicsManager physics;


        private void Awake()
        {
            //��ʼ��������
            props.AddProp("Name", "���");
            var rb = GetComponent<Rigidbody2D>();
            if (rb == null) throw new Exception($"�Ҳ���[Rigidbody2D]���");
            props.AddProp("Rb", rb);
            var anims = GetComponent<Animator>();
            if (anims == null) throw new Exception($"�Ҳ���[Animator]���");
            props.AddProp("Anims", anims);
            props.AddProp("MoveSpeed", moveSpeed);
            props.AddProp("JumpForce", jumpForce);

            //��ʼ��������
            inputs.Awake();

            //��ʼ��������
            var anims2 = props.GetProp<Animator>("Anims");
            animCtrls.SetAnims(anims2);

            //��ʼ��������
            physics.Init();

            bevs.Init(this);
            //��ʼ����Ϊ״̬�б�
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
