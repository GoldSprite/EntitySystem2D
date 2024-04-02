using GoldSprite.UFsm;
using GoldSprite.UnityTools.InspectorTools;
using GoldSprite.UnityTools.MyDict;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GoldSprite.EntitySystem2D {
    public class EntitySystem : MonoBehaviour {
        [SerializeField]
        private List<BehaviourMeta> behaviourMetaList;
        public EntityUFsm fsm;

        public void Start()
        {
            fsm = this.InsertComponentLater<EntityUFsm>();
            fsm.InitMetas(behaviourMetaList);
        }
    }


    public class EntityUFsm : BaseFsm {
        public void InitMetas(List<BehaviourMeta> behaviourMetaList)
        {
            for (int i = 0; i < behaviourMetaList.Count; i++) {
                var behaviourMeta = behaviourMetaList[i];
                var stateType = behaviourMeta.StateType;
                var state = (BaseState)Activator.CreateInstance(stateType);
                if (i == 0) InitState(state);
                else AddState(state);
            }
        }
    }
}