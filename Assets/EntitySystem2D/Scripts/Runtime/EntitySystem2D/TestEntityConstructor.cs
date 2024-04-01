using GoldSprite.UnityPlugins.MyInputSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GoldSprite.UnityPlugins.EntitySystem2D {

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
        public float attackPower = 5f;
        [Header("Constructor")]
        [HideInInspector]
        [Tooltip("ʵ�幹����")]
        public EntityBehaviourConstructor bevs;
        [Header("Components")]
        [Tooltip("���Թ�����")]
        public PropertyProvider props;
        [Tooltip("״̬��")]
        public FinateStateMachine fsm;
        [Tooltip("�����ṩ��")]
        public InputProvider inputs;
        [Tooltip("����������")]
        public AnimsProvider animCtrls;
        [Tooltip("���������")]
        public PhysicsManager physics;
        [Tooltip("�¼�������")]
        public EventManager events;


        private void Awake()
        {
            //��ʼ��������
            props.AddProp("Name", "���");
            props.AddProp("TempVal", 2f);
            var rb = GetComponent<Rigidbody2D>();
            if (rb == null) throw new Exception($"�Ҳ���[Rigidbody2D]���");
            props.AddProp("Rb", rb);
            var anims = GetComponent<Animator>();
            if (anims == null) throw new Exception($"�Ҳ���[Animator]���");
            props.AddProp("Anims", anims);
            props.AddProp("MoveSpeed", moveSpeed);
            props.AddProp("JumpForce", jumpForce);
            props.AddProp("AttackPower", attackPower);
            props.AddProp("MoveAction", (Action<Vector2, float>)((dir, moveBoost) => {
                float moveSpeed = props.GetProp<float>("MoveSpeed");
                Vector2 moveDir = inputs.GetValue<Vector2>(inputs.InputActions.GamePlay.Move);

                //fsm.FDebug("ִ���ƶ�.");
                var vel = rb.velocity;
                var velxNormalized = moveDir.x == 0 ? 0 : (moveDir.x > 0 ? 1 : -1);
                var velx = velxNormalized * moveSpeed * moveBoost;
                vel.x = Mathf.Lerp(vel.x, velx, 3 / 60f);
                rb.velocity = vel;

                props.GetProp<Action<int>>("TurnAction")?.Invoke(velxNormalized);
            }));
            props.AddProp("TurnAction", (Action<int>)((face) => {
                if (face == 0) return;
                //ת��
                var localScale = rb.transform.localScale;
                localScale.x = face;
                rb.transform.localScale = localScale;
            }));
            props.AddProp("LastFace", transform.localScale.x > 0 ? 1 : -1);
            //Props.AddProp("RunTurnEvent", (Action<int>)((face) => { }));
            props.Init();

            //��ʼ��������
            inputs.Awake();
            //inputs.AddActionListener(inputs.InputActions.GamePlay.Move, (Action<Vector2>)((MoveDir) => {
            //    if (MoveDir.x == 0) return;
            //    int dirxNormalized = MoveDir.x > 0 ? 1 : -1;
            //    if (dirxNormalized != Props.GetProp<int>("LastFace")) {
            //        Debug.Log("ת���¼�����.");
            //        Action<int> runTurnAction = Props.GetProp<Action<int>>("RunTurnEvent");
            //        runTurnAction?.Invoke(dirxNormalized);
            //        Props.SetProp("LastFace", dirxNormalized);
            //    }
            //}));
            inputs.AddActionListener(inputs.InputActions.GamePlay.Move, (Action<Vector2>)((dir) => { }));
            inputs.AddActionListener(inputs.InputActions.GamePlay.Attack, (Action<bool>)((down) => { }));
            inputs.AddActionListener(inputs.InputActions.GamePlay.Jump, (Action<bool>)((down) => { }));
            inputs.AddActionListener(InputProvider.VirtualKey.HurtKey, (Action<bool>)((down) => { }));
            inputs.Init();

            //��ʼ��������
            var anims2 = props.GetProp<Animator>("Anims");
            animCtrls.anims = anims2;
            animCtrls.Init();

            //��ʼ��������
            physics.Awake();
            physics.Init();

            //��ʼ���¼���
            events.Init();

            bevs.ent = this;
            bevs.props = props;
            bevs.fsm = fsm;
            bevs.inputs = inputs;
            bevs.animCtrls = animCtrls;
            bevs.physics = physics;
            bevs.events = events;
            bevs.Init();
            //��ʼ����Ϊ״̬�б�
            bevs.AddBehaviour(new IdleBehaviour() { AnimName = "Idle" });
            bevs.AddBehaviour(new MoveBehaviour() { AnimName = "Run", TurnAnimName = "RunTurn" });
            bevs.AddBehaviour(new JumpBehaviour() { AnimName = "JumpBlend", AnimsPhase = 5, AnimNames = new string[] { "JumpStart", "JumpUpper", "JumpTurnFall", "JumpFall", "Land" } });
            bevs.AddBehaviour(new AttackBehaviour() { AnimName = "AttackBlend", AnimsPhase = 3, AnimNames = new string[] { "Attack_1", "Attack_2", "Attack_3" } });
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
