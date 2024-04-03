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

        //元属性
        public string Name { get; set; } = "AI";
        public MoveState MoveState { get; set; }
        [SerializeField] private Rect roamArea;
        public Rect RoamArea { get => roamArea; set => roamArea = value; }
        [SerializeField] private Collider2D bodyCollider;
        public Collider2D BodyCollider { get => bodyCollider; set => bodyCollider = value; }


        private void OnDrawGizmos()
        {
            DrawRoamArea();
        }

        private void DrawRoamArea()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(RoamArea.position, RoamArea.size);
        }
    }

}
