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


        private void Start()
        {
        }
    }

}
