
using System;
using UnityEngine;

namespace GoldSprite.UFsm {
    public interface IAIProps : IProps, IRoamProps {
        public string Name { get; set; }
    }

    public interface IRoamProps {
        public MoveState MoveState { get; set; }
        public Rect RoamArea { get; set; }
        public Collider2D BodyCollider { get; set; }
    }
}
