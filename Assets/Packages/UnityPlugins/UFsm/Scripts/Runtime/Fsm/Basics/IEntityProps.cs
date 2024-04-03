
using System;
using UnityEngine;

namespace GoldSprite.UFsm {
    public interface IEntityProps : IProps, IAttacker, IVictim, IJumper {
        public string Name { get; set; }
        public Vector2 Direction { get; set; }
        public Vector2 Velocity { get; set; }
        public PhysicsMaterial2D[] SmoothOrRoughMaterial { get; set; }
        public PhysicsMaterial2D PhysicsMaterial { get; set; }
        public bool IsGround { get;}
        public bool MoveBoostKey { get; set; }
        public KeySwitchType MoveBoostKeyType { get; set; }
        public float Speed { get; set; }
        public float SpeedBoost { get; set; }
        public int Face { get; set; }
        //public bool AnyKey { get; set; }
        public Collider2D BodyCollider { get; set; }

        public enum KeySwitchType
        {
            KeyDown, Key
        }
    }

    public interface IAttacker
    {
        public bool AttackKey { get; set; }
        public MoveState MoveState { get; }
        public float AttackingMoveDrag { get; set; }
        public float AttackPower { get; set; }
    }
    public interface IVictim: ILiving {
        public bool HurtKey { get; set; }
        public bool DeathKey { get; set; }
    }
    public interface IJumper {
        public bool JumpKey { get; set; }
        public float JumpForce { get; set; }
        public MoveState MoveState { get; }
        public float JumpingMoveDrag { get; set; }
    }

    public interface ILiving
    {
        public float Health { get; set;}
        public float MaxHealth { get; set;}
    }
}
