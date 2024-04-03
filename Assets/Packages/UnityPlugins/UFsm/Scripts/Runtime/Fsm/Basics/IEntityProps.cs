
using System;
using UnityEngine;

namespace GoldSprite.UFsm {
    public interface IEntityProps : IProps, IAttacker, IVictim {
        public string Name { get; set; }
        public Vector2 Direction { get; set; }
        public Vector2 Velocity { get; set; }
        public bool IsGround { get;}
        public float Speed { get; set; }
        public int Face { get; set; }
    }

    public interface IAttacker
    {
        public bool AttackKey { get; set; }
    }
    public interface IVictim {
        public bool HurtKey { get; set; }
        public bool DeathKey { get; set; }
    }
}
