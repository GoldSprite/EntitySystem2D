using GoldSprite.UnityPlugins.MyInputSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GoldSprite.UnityPlugins.EntitySystem2D.Tests {

    public class TestEntityConstructor : EntitySystem {
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

        private void Awake()
        {
            InitPropertyManager();
            fsm = new FinateStateMachine();
            InitInputProvider();
            InitEntityBehaviours();
        }

        private void InitInputProvider()
        {
            inputs = new InputProvider();
            inputs.Awake();
        }

        private void InitPropertyManager()
        {
            props = new PropertyManager();
            var rb = GetComponent<Rigidbody2D>();
            if (rb == null) throw new Exception($"�Ҳ���Rigidbody2D���");
            props.AddProp("Rb", rb);
        }

        private void InitEntityBehaviours()
        {
            bevs = new EntityBehaviourConstructor(this);

            bevs.AddBehaviour(new IdleBehaviour());
            //bevs.AddBehaviour(new MoveBehaviour());

        }
    }


}
