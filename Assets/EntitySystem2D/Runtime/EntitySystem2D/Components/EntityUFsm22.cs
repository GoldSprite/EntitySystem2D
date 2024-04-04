using GoldSprite.UFsm;
using System;
using System.Collections.Generic;

namespace GoldSprite.EntitySystem2D {
    public class EntityUFsm22 : BaseFsm {
        public void InitMetas(EntityUProps props, EntityMyAnimator animCtrls, List<BehaviourMeta> behaviourMetaList)
        {
            Props = props;
            AnimCtrls = animCtrls;

            Cmd = new BaseFsmCommandManager();
            InitCommands();


            for (int i = 0; i < behaviourMetaList.Count; i++) {
                var behaviourMeta = behaviourMetaList[i];
                var stateType = behaviourMeta.StateType;
                var obj = Activator.CreateInstance(stateType, new object[] { this });
                BaseState state = obj as BaseState;
                state.AnimName = behaviourMeta.AnimClip.name;
                if (i == 0) InitState(state);
                else AddStateFix(state, behaviourMeta.Priority);
            }
        }
    }
}