
using System;
using UnityEngine;

namespace GoldSprite.UFsm {
    public interface IAIProps : IProps, IRoamProps, IChaseProps, IAutoAttackProps {
        public string Name { get; set; }
        public RoamState RoamState { get; set; }
        public IEntityProps CtrlProps { get; set; }
        public Rect LandArea { get; set; }
    }

    public interface IRoamProps {
    }

    public interface IChaseProps {
        public Collider2D VisionRange { get; set; }
    }

    public interface IAutoAttackProps {
        public Collider2D AttackRange { get; set; }
    }
}
