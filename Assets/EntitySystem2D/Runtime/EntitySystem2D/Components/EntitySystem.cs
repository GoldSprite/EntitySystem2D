using GoldSprite.UFsm;
using GoldSprite.UnityPlugins.MyAnimator;
using GoldSprite.UnityTools.MyDict;
using System.Collections.Generic;
using UnityEngine;

namespace GoldSprite.EntitySystem2D {
    public class EntitySystem : MonoBehaviour {
        [SerializeField]
        private List<BehaviourMeta> behaviourMetaList;
        public EntityUFsm22 fsm;
        public EntityMyAnimator animCtrls;
        public EntityUProps props;

        public void Start()
        {
            animCtrls.InitMetas(behaviourMetaList);
            fsm.InitMetas(props, animCtrls, behaviourMetaList);
            props.MoveState = fsm.GetState<MoveState>();
        }
    }
}