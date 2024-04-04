using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GoldSprite.UnityPlugins.PhysicsManager {
    [Serializable]
    public class GroundDetection : MonoBehaviour {
        [Header("配置")]
        public Collider2D footColl;
        [Header("事件")]
        public Action<Collider2D> OnEnterGround;
        public Action<Collider2D> OnExitGround;
        public static string GroundLayer = "Ground";
        public int GroundCount;
        public static bool InitGroundList;
        public static List<GameObject> GroundList;
        [Header("实时")]
        public bool IsGround;
        public List<Collider2D> CollisionList;

        private static int groundMask;
        public static int GroundMask { get => groundMask; private set => groundMask = value; }

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
            GroundMask = LayerMask.GetMask(new string[] { GroundLayer });
            SingletonInitGroundList();
            GroundCount = GroundList.Count;

            OnEnterGround += (Action<Collider2D>)((collider) => {
                if (collider.gameObject.layer == LayerMask.NameToLayer(GroundLayer)) {
                    if (!footColl.IsTouching(collider)) return;
                    if (!CollisionList.Contains(collider)) CollisionList.Add(collider);
                    if (CollisionList.Count != 0) IsGround = true;
                }
            });
            OnExitGround += (collider) => {
                if (collider.gameObject.layer == LayerMask.NameToLayer(GroundLayer)) {
                    if (CollisionList.Contains(collider)) CollisionList.Remove(collider);
                    if (CollisionList.Count == 0) IsGround = false;
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
            Debug.Log($"退出Collider{collision.gameObject.name}");
            OnExitGround?.Invoke(collision);
        }
    }
}