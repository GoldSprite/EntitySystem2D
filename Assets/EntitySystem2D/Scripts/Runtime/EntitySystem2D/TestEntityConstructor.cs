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
            if (rb == null) throw new Exception($"找不到Rigidbody2D组件");
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
        [Tooltip("实体构造器")]
        public EntityBehaviourConstructor bevs;
        [Header("Components")]
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


        private void Awake()
        {
            //初始化属性器
            props.AddProp("Name", "佩茨");
            props.AddProp("TempVal", 2f);
            var rb = GetComponent<Rigidbody2D>();
            if (rb == null) throw new Exception($"找不到[Rigidbody2D]组件");
            props.AddProp("Rb", rb);
            var anims = GetComponent<Animator>();
            if (anims == null) throw new Exception($"找不到[Animator]组件");
            props.AddProp("Anims", anims);
            props.AddProp("MoveSpeed", moveSpeed);
            props.AddProp("JumpForce", jumpForce);
            props.AddProp("AttackPower", attackPower);
            props.AddProp("MoveAction", (Action<Vector2, float>)((dir, moveBoost) => {
                float moveSpeed = props.GetProp<float>("MoveSpeed");
                Vector2 moveDir = inputs.GetValue<Vector2>(inputs.InputActions.GamePlay.Move);

                //fsm.FDebug("执行移动.");
                var vel = rb.velocity;
                var velxNormalized = moveDir.x == 0 ? 0 : (moveDir.x > 0 ? 1 : -1);
                var velx = velxNormalized * moveSpeed * moveBoost;
                vel.x = Mathf.Lerp(vel.x, velx, 3 / 60f);
                rb.velocity = vel;

                props.GetProp<Action<int>>("TurnAction")?.Invoke(velxNormalized);
            }));
            props.AddProp("TurnAction", (Action<int>)((face) => {
                if (face == 0) return;
                //转向
                var localScale = rb.transform.localScale;
                localScale.x = face;
                rb.transform.localScale = localScale;
            }));
            props.AddProp("LastFace", transform.localScale.x > 0 ? 1 : -1);
            //Props.AddProp("RunTurnEvent", (Action<int>)((face) => { }));
            props.Init();

            //初始化输入器
            inputs.Awake();
            //inputs.AddActionListener(inputs.InputActions.GamePlay.Move, (Action<Vector2>)((MoveDir) => {
            //    if (MoveDir.x == 0) return;
            //    int dirxNormalized = MoveDir.x > 0 ? 1 : -1;
            //    if (dirxNormalized != Props.GetProp<int>("LastFace")) {
            //        Debug.Log("转向事件触发.");
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

            //初始化动画器
            var anims2 = props.GetProp<Animator>("Anims");
            animCtrls.anims = anims2;
            animCtrls.Init();

            //初始化物理器
            physics.Awake();
            physics.Init();

            //初始化事件器
            events.Init();

            bevs.ent = this;
            bevs.props = props;
            bevs.fsm = fsm;
            bevs.inputs = inputs;
            bevs.animCtrls = animCtrls;
            bevs.physics = physics;
            bevs.events = events;
            bevs.Init();
            //初始化行为状态列表
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
