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
    public class AIUProps : SerializedMonoBehaviour, IAIProps {
        public PhysicsManager physics;
        //元属性
        public string Name { get; set; } = "AI";
        [HideInInspector]  //这里莫名报序列化错误所以隐藏
        public RoamState RoamState { get; set; }
        [SerializeField] private Rect roamArea;
        public Rect RoamArea { get => roamArea; set => roamArea = value; }
        [SerializeField] private Collider2D bodyCollider;
        public Collider2D BodyCollider { get => bodyCollider; set => bodyCollider = value; }
        public bool IsCollisionGround { get; set; }


        private void Start()
        {
            physics.OnEnterGround += (c) => { IsCollisionGround = true; };
        }

        private void OnDrawGizmos()
        {
            DrawRoamArea();
        }

        private void DrawRoamArea()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(RoamArea.center, RoamArea.size);
        }
    }

}
