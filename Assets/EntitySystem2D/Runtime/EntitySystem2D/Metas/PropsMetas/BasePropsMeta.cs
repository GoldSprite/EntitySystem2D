using GoldSprite.UFsm;
using UnityEngine;

namespace GoldSprite.EntitySystem2D {
    [CreateAssetMenu]
    public class BasePropsMeta : PropsMeta, IBaseProps {
        [SerializeField] private new string name;
        public string Name { get => name; set => name = value; }
        [SerializeField] private Vector2 direction;
        public Vector2 Direction { get => direction; set => direction = value.magnitude > 1 ? value.normalized : value; }
        public Vector2 velocity;
        public Vector2 Velocity { get => velocity; set => velocity = value; }
        public bool IsGround { get; } = true;
        [SerializeField] private string speed;
        public float Speed { get; set; } = 1;
    }

    public interface IBaseProps2 : IBaseProps { }
}