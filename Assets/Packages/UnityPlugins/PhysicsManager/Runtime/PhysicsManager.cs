using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GoldSprite.UnityPlugins.PhysicsManager {
    [Serializable]
    public class PhysicsManager: MonoBehaviour {
        [Header("事件")]
        public Action<Collider2D> OnEnterGround;
        public Action<Collider2D> OnExitGround;
        public static string GroundLayer = "Ground";
        public int GroundCount;
        public static bool InitGroundList;
        public static List<GameObject> GroundList;
        [Header("实时")]
        public bool IsGround;


        public static void SingletonInitGroundList()
        {
            if (!InitGroundList) {
                var collders = GameObject.FindObjectsOfType<Collider2D>();
                if (collders.Length != 0) {
                    GroundList = collders.Where(p => p.gameObject.layer == LayerMask.NameToLayer(GroundLayer)).Select(p => p.gameObject).ToList();
                }
            }
        }


        public void Awake()
        {
            SingletonInitGroundList();
            GroundCount = GroundList.Count;

            OnEnterGround +=(collider) => {
                if (collider.gameObject.layer == LayerMask.NameToLayer(GroundLayer)) {
                    IsGround = true;
                }
            };
            OnExitGround+=(collider) => {
                if (collider.gameObject.layer == LayerMask.NameToLayer(GroundLayer)) {
                    IsGround = false;
                }
            };
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnEnterGround?.Invoke(collision.collider);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            OnEnterGround?.Invoke(collision);
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            OnExitGround?.Invoke(collision.collider);
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            OnExitGround?.Invoke(collision);
        }
    }
}