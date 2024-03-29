using System;
using UnityEngine;

namespace GoldSprite.UnityPlugins.EntitySystem2D.Tests {
    public class LivingNode {
        private PropertyProvider props;
        public bool IsAlive;
        public bool IsDeath;
        public float MaxHealth;
        public float Health;
        public float AttackPower;

        public void Attack(LivingNode living)
        {
            living.Hurt(AttackPower);
        }

        public void Hurt(float attackPower)
        {
            var health = Health-attackPower;
            if(health > 0) {
                Health = health;
                Debug.Log($"[{props.GetProp<string>("Name")}]: 受伤: old-{health}, now-{Health}");
            } else {
                IsAlive = false;
                Health = 0;
                Debug.Log($"[{props.GetProp<string>("Name")}]: 死亡: old-{health}");
            }
        }

        public void Spawn()
        {
            Debug.Log($"[{props.GetProp<string>("Name")}]: 出生了.");
            Health = MaxHealth;
            IsAlive = true;
        }

        public void ReSpawn()
        {
            Debug.Log($"[{props.GetProp<string>("Name")}]: 重生了.");
            IsDeath = false;
            Spawn();
        }
    }
}