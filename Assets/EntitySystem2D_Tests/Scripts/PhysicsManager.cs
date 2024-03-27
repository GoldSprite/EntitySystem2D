using System;
using UnityEngine;
using UnityEngine.Events;

namespace GoldSprite.UnityPlugins.EntitySystem2D.Tests {
    [Serializable]
    public class PhysicsManager {
        [Header("事件")]
        public UnityEvent<Collider2D> OnTriggerEnter2D;
        public UnityEvent<Collider2D> OnTriggerExit2D;
        [Header("实时")]
        public bool IsGround;

        public void Init()
        {
            OnTriggerEnter2D.AddListener((collider) => {
                if (collider.CompareTag("Ground")) {
                    IsGround = true;
                }
            });
            OnTriggerExit2D.AddListener((collider) => {
                if (collider.CompareTag("Ground")) {
                    IsGround = false;
                }
            });
        }
    }
}