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
        [HideInInspector]  //这里莫名报序列化错误所以隐藏
        public RoamState RoamState { get; set; }
        [SerializeField] private Rect landArea;
        public Rect LandArea { get => landArea; set => landArea = value; }
        [SerializeField] private Collider2D visionRange;
        public Collider2D VisionRange { get => visionRange; set => visionRange = value; }
        [SerializeField] private IEntityProps ctrlProps;
        public IEntityProps CtrlProps { get => ctrlProps; set => ctrlProps = value; }
        [SerializeField] private Collider2D attackRange;
        public Collider2D AttackRange { get => attackRange; set => attackRange = value; }


        private void Start()
        {
        }

        private void OnDrawGizmos()
        {
            DrawRoamArea();
            //DrawVisionRange();
        }

        private void DrawRoamArea()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(LandArea.center, LandArea.size);
        }

        //private void DrawVisionRange()
        //{
        //    var center = (Vector3)VisionRange.center;
        //    var size = VisionRange.size;
        //    Gizmos.color = Color.green;
        //    Gizmos.DrawWireCube(center, size);
        //}
    }

}
