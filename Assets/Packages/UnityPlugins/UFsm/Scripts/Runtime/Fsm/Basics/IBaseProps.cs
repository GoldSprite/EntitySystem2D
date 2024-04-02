
using System;
using UnityEngine;

namespace GoldSprite.UFsm {
    public interface IBaseProps : IProps {
        public string Name { get; set; }
        public Vector2 Direction { get; set; }
        public Vector2 Velocity { get; set; }
        public bool IsGround { get;}
        public float Speed { get; set; }
    }

    [Serializable]
    public class BaseProps : IBaseProps {
        public string Name { get; set; } = "UNKNOWN";
        private Vector2 direction;
        public Vector2 Direction { get => direction; set => direction = value.magnitude > 1 ? value.normalized : value; }
        public Vector2 Velocity { get; set; }
        public bool IsGround { get; } = true;
        public float Speed { get; set; } = 1;
    }
}
