
using System;
using UnityEngine;

namespace GoldSprite.UFsm {
    public interface IAIProps : IProps, IRoamProps, IChaseProps {
        public string Name { get; set; }
        public RoamState RoamState { get; set; }
        public IEntityProps CtrlProps { get; set; }
    }

    public interface IRoamProps {
        public Rect RoamArea { get; set; }
    }

    public interface IChaseProps {
        public Rect VisionRange { get; set; }
    }
}
