using System;
using UnityEngine;

namespace GoldSprite.UnityPlugins.EntitySystem2D {
    [Serializable]
    public class LivingNode {
        public PropertyProvider Props {  get; private set; }
        public bool IsAlive;
        public float MaxHealth = 10;
        public float Health;
        public float AttackPower = 2;

        public void Init(PropertyProvider props)
        {
            this.Props = props;

            Spawn();
        }

        public void Attack(LivingNode living)
        {
            Debug.Log($"[{Props.GetProp<string>("Name")}]: 攻击了 [{living.Props.GetProp<string>("Name")}]: 攻击力 {AttackPower}.");
            living.Hurt(AttackPower);
        }

        public void Hurt(float attackPower)
        {
            if (!IsAlive) return;
            var oldHealth = Health;
            var health = oldHealth - attackPower;
            if (health > 0) {
                Health = health;
                Debug.Log($"[{Props.GetProp<string>("Name")}]: 受伤: old {oldHealth}, now {Health}");
            } else {
                IsAlive = false;
                Health = 0;
                Debug.Log($"[{Props.GetProp<string>("Name")}]: 死亡: old {health}");
            }
        }

        public void Spawn()
        {
            Debug.Log($"[{Props.GetProp<string>("Name")}]: 出生了.");
            Health = MaxHealth;
            IsAlive = true;
        }

        //public void ReSpawn()
        //{
        //    Debug.Log($"[{Props.GetProp<string>("Name")}]: 重生了.");
        //    IsDeath = false;
        //    Spawn();
        //}
    }
}