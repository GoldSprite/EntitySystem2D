
using System;
using UnityEngine;

namespace GoldSprite.UFsm {
    public interface IAIProps : IProps, IRoamProps {
        public string Name { get; set; }
    }

    public interface IRoamProps {
        public MoveState MoveState { get; set; }
    }
}
