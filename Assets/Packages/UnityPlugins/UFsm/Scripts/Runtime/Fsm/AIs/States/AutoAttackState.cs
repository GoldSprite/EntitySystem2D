using GoldSprite.GUtils;
using GoldSprite.UnityPlugins.PhysicsManager;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GoldSprite.UFsm {
    [Serializable]
    public class AutoAttackState : AIState {
        public bool StateSwitch;
        public BaseFsm otherFsm;
        [ShowInInspector]
        public Vector2 Direction { get => Fsm.ctrlFsm.Props.Direction; set => Fsm.ctrlFsm.Props.Direction = value; }

        private float AttackCache;
        public float AttackCacheInterval = 1.5f;
        [ShowInInspector]
        public float AttackCacheNormalized => (AttackCache-Time.time)/AttackCacheInterval;


        public AutoAttackState(AIFsm fsm) : base(fsm)
        {
            CanTranSelf = false;
        }

        public override void UpdateCondition()
        {
            if (Fsm.collCtrls.TryOverlap(Fsm.Props.AttackRange, out otherFsm)) {
                StateSwitch = true;
                LogTool.NLog("AutoAttackStateTest", "����������Ŀ��, �����Զ�����");
            } else {
                LogTool.NLog("AutoAttackStateTest", "û��һ��Ŀ��, �˳��Զ�����");
                otherFsm = null;
                StateSwitch = false;
            }
        }

        public override bool Enter() => StateSwitch;
        public override bool Exit() => !StateSwitch;

        public override void OnExit()
        {
        }

        public override void OnEnter()
        {
            AttackCache = Time.time + AttackCacheInterval;
        }

        public override void Update()
        {
            if (Fsm.ctrlFsm.CState is not AttackState && AttackCacheNormalized <= 0) {
                LogTool.NLog("AutoAttackStateTest", "��Ŀ�귢�𹥻�.");
                Fsm.ctrlFsm.Command(BaseFsmCommand.Attack, true);
                AttackCache = Time.time + AttackCacheInterval;
            }
        }
    }
}
