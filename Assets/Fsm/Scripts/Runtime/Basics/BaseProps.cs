
using System;
using UnityEngine;

namespace GoldSprite.Fsm {
    [Serializable]
    public class BaseProps : IProps {
        public string Name { get; set; }
        public Vector2 Direction { get; set; }
        public Vector2 Velocity { get; set; }
        public bool IsGround { get; set; }
    }
}
