using GoldSprite.UnityPlugins.PhysicsManager;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using GoldSprite.GUtils;
using GoldSprite.UnityPlugins.MyInputSystem;
using System.Collections.Generic;
using UnityEngine.Events;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GoldSprite.UFsm {
    public class EntityUProps : SerializedMonoBehaviour, IEntityProps {
        //Unity属性
        [ManualRequire]
        [Header("控制组件")]
        public Rigidbody2D rb;
        [Header("依赖物理")]
        public GroundDetection physics;
        [Header("摩擦材质")]
        [SerializeField] private PhysicsMaterial2D[] smoothOrRoughMaterial;
        public PhysicsMaterial2D[] SmoothOrRoughMaterial { get => smoothOrRoughMaterial; set => smoothOrRoughMaterial = value; }
        public PhysicsMaterial2D PhysicsMaterial { get => rb.sharedMaterial; set => rb.sharedMaterial = value; }
        [PropertySpace]
        //元属性
        [SerializeField] private new string name = "UNKNOWN";
        public string Name { get => name; set => name = value; }
        [SerializeField] private float health;
        public float Health { get => health; set => health = value; }
        [SerializeField] private float maxHealth = 20;
        public float MaxHealth { get => maxHealth; set => maxHealth = value; }
        [SerializeField] private float attackPower = 4;
        public float AttackPower { get => attackPower; set => attackPower = value; }
        private Vector2 direction;
        [ShowInInspector]
        public Vector2 Direction { get => (direction = direction.magnitude > 1 ? direction.normalized : direction); set => direction = value; }
        [SerializeField] private bool hurtTurn = true;
        public bool HurtTurn { get => hurtTurn; set => hurtTurn = value; }
        [ShowInInspector]
        public Vector2 Velocity { get => rb.velocity; set => rb.velocity = value; }
        [ShowInInspector]
        public bool IsGround => (physics?.IsGround)??false;
        [SerializeField] private float speed = 4;
        public float Speed { get => speed; set => speed = value; }
        [SerializeField] private float speedBoost = 2;
        public float SpeedBoost { get => speedBoost; set => speedBoost = value; }
        public int Face {
            get => transform.localScale.x > 0 ? 1 : -1;
            set {
                if (value == 0) return;
                var ls = transform.localScale;
                ls.x = value > 0 ? 1 : -1;
                transform.localScale = ls;
            }
        }
        [ShowInInspector]
        public bool AttackKey { get; set; }
        [ShowInInspector]
        public bool HurtKey { get; set; }
        [ShowInInspector]
        public bool DeathKey { get; set; }
        [ShowInInspector]
        public bool JumpKey { get; set; }
        [SerializeField] private float jumpForce = 6;
        public float JumpForce { get => jumpForce; set => jumpForce = value; }
        [SerializeField, Range(0f, 1f)] private float jumpingMoveDrag = 0.6f;
        public float JumpingMoveDrag { get => jumpingMoveDrag; set => jumpingMoveDrag = value; }
        [SerializeField, Range(0f, 1f)] private float attackingMoveDrag = 0.8f;
        public float AttackingMoveDrag { get => attackingMoveDrag; set => attackingMoveDrag = value; }
        public MoveState MoveState { get; set; }
        [SerializeField] private IEntityProps.KeySwitchType moveBoostKeyType;
        public IEntityProps.KeySwitchType MoveBoostKeyType { get => moveBoostKeyType; set => moveBoostKeyType = value; }
        [ShowInInspector]
        public bool MoveBoostKey { get; set; }
        [SerializeField] private Collider2D bodyCollider;

        public Collider2D BodyCollider { get => bodyCollider; set => bodyCollider = value; }

        //[Header("可选的")]
        //[RequireInputCtrl]
        //public PlayerEntityInputs inputs;
        //[HideInInspector]
        //public List<UnityEvent> RegisterInputsCtrls;


        private void Start()
        {
            //rb = GetComponent<Rigidbody2D>();
            //physics = GetComponent<GroundDetection>();

            InitProps();
        }

        public void InitProps()
        {
            //Health = MaxHealth;
        }
    }


    public class RequireInputCtrlAttribute : PropertyAttribute { }

    //#if UNITY_EDITOR
    //    public class RequireInputCtrlDrawer: PropertyDrawer
    //    {
    //        float height;
    //        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    //        {
    //            height = 0;
    //            position.height = InspectorHelper.singleLineMargin;

    //            EditorGUI.PropertyField(position, property, label);
    //            position.NextLine(ref height);

    //            var target = property.serializedObject.targetObject;
    //            var input = ReflectionHelper.GetField<IMyInputManager>(target);
    //            if (input == null) return;

    //            {   //注册所有键
    //                //var RegisterInputsCtrls = ReflectionHelper.GetField<List<Delegate>>(target);
    //                //if (input.Exist(input.InputActions.GamePlay.Move)) {
    //                //}
    //            }
    //        }

    //        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    //        {
    //            return height;
    //        }
    //    }
    //#endif
}
