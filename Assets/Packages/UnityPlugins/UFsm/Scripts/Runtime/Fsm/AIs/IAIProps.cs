
using System;
using UnityEngine;

namespace GoldSprite.UFsm {
    public interface IAIProps : IProps, IRoamProps {
        public string Name { get; set; }
        public RoamState RoamState { get; set; }
    }

    public interface IRoamProps {
        public Rect RoamArea { get; set; }
    }
}
