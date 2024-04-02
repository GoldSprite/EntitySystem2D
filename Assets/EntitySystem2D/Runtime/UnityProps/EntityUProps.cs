﻿using GoldSprite.UnityPlugins.PhysicsManager;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using GoldSprite.UnityTools.InspectorTools;
using GoldSprite.GUtils;
using GoldSprite.UnityPlugins.MyInputSystem;
using System.Collections.Generic;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GoldSprite.UFsm {
    public class EntityUProps : MonoBehaviour, IBaseProps {
        //元属性
        public string Name { get; set; } = "UNKNOWN";
        private Vector2 direction;
        public Vector2 Direction { get => direction; set => direction = value.magnitude > 1 ? value.normalized : value; }
        public Vector2 Velocity { get => rb.velocity; set => rb.velocity = value; }
        public bool IsGround => physics.IsGround;
        public float Speed { get; set; } = 1;

        //Unity属性
        [ManualRequire]
        [Header("控制组件")]
        public Rigidbody2D rb;
        [ManualRequire]
        [Header("依赖物理")]
        public PhysicsManager physics;
        //[Header("可选的")]
        //[RequireInputCtrl]
        //public EntityInputs inputs;
        //[HideInInspector]
        //public List<UnityEvent> RegisterInputsCtrls;


        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            physics = GetComponent<PhysicsManager>();
        }
    }


    public class RequireInputCtrlAttribute: PropertyAttribute { }

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
