using GoldSprite.UnityPlugins.PhysicsManager;
using UnityEngine;

namespace GoldSprite.UFsm {
    public class BaseUProps : MonoBehaviour, IBaseProps {
        //元属性
        public string Name { get; set; } = "UNKNOWN";
        private Vector2 direction;
        public Vector2 Direction { get => direction; set => direction = value.magnitude > 1 ? value.normalized : value; }
        public Vector2 Velocity { get => rb.velocity; set => rb.velocity = value; }
        public bool IsGround => physics.IsGround;
        public float Speed { get; set; } = 1;

        //Unity属性
        [ManualRequire]
        public Rigidbody2D rb;
        [ManualRequire]
        public PhysicsManager physics;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            physics = GetComponent<PhysicsManager>();
        }
    }
}
