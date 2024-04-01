using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace GoldSprite.UnityPlugins.EntitySystem2D {
    [Serializable]
    public class PhysicsManager: IEntityProvider {
        [Header("事件")]
        public UnityEvent<Collider2D> OnTriggerEnter2D;
        public UnityEvent<Collider2D> OnTriggerExit2D;
        public string GroundLayer = "Ground";
        public List<GameObject> GroundList;
        [Header("实时")]
        public bool IsGround;

        public void Awake()
        {
            var collders = GameObject.FindObjectsOfType<Collider2D>();
            if (collders.Length != 0)
                GroundList = collders.Where(p => p.gameObject.layer == LayerMask.NameToLayer(GroundLayer)).Select(p => p.gameObject).ToList();

            OnTriggerEnter2D.AddListener((collider) => {
                if (collider.gameObject.layer == LayerMask.NameToLayer("Ground")) {
                    IsGround = true;
                }
            });
            OnTriggerExit2D.AddListener((collider) => {
                if (collider.gameObject.layer == LayerMask.NameToLayer("Ground")) {
                    IsGround = false;
                }
            });
        }

        public bool Init()
        {
            var msgs = new List<string>();
            //if (GroundList.Count == 0) msgs.Add($"[PhysicsManager-Awake]: GroundLayer[{GroundLayer}]层找不到任何地面物体, 地面检测将不起作用.");
            return IEntityProvider.PrintInitLog(this, msgs);
        }
    }
}