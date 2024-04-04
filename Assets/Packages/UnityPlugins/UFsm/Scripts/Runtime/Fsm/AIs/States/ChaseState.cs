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
    public class ChaseState : AIState {
        public bool StateSwitch;
        public Vector3 MoveVector;
        public bool VisionRangeGizmos = true;
        public Vector3 BodyPos => Fsm.ctrlFsm.Props.BodyCollider.bounds.center;
        public BaseFsm otherFsm;
        [ShowInInspector]
        public Vector2 Direction { get => Fsm.ctrlFsm.Props.Direction; set => Fsm.ctrlFsm.Props.Direction = value; }

        public ChaseState(AIFsm fsm) : base(fsm)
        {
            CanTranSelf = false;
        }

        public override void UpdateCondition()
        {
            if (Fsm.collCtrls.IsOutOfLandArea()) {
                LogTool.NLog("ChaseStateTest", "�����ڽ���, ����׷��.");
                StateSwitch = false;
            } else
            if (TryOverlap(Fsm.Props.VisionRange, out otherFsm)) {
                StateSwitch = true;
                MoveVector = otherFsm.Props.BodyCollider.bounds.center - BodyPos; MoveVector.y = 0;
                Fsm.ctrlFsm.Props.Direction = MoveVector;
                LogTool.NLog("ChaseStateTest", "��Ŀ��, ����׷��");
            } else {
                LogTool.NLog("ChaseStateTest", "û��һ��Ŀ��, �˳�׷��");
                otherFsm = null;
                StateSwitch = false;
            }
        }

        public override bool Enter() => StateSwitch;
        public override bool Exit() => !StateSwitch;

        public override void OnExit()
        {
            var dir = Direction;
            dir.x = 0;
            Direction = dir;
        }

        public override void OnEnter()
        {
        }

        public override void Update()
        {

        }


        private bool TryOverlap(Rect selfBounds, out BaseFsm otherFsm)
        {
            var min = selfBounds.min;
            var max = selfBounds.max;
            var colls = Physics2D.OverlapAreaAll(min, max);
            if (colls.Length != 0) {
                LogTool.NLog("ChaseStateTest", "�б�Ϊ��, �����ж��Ƿ���BaseFsm");
                foreach (var coll in colls) {
                    if (coll.TryGetComponent(out otherFsm) && otherFsm is not AIFsm) {
                        LogTool.NLog("ChaseStateTest", "�ҵ�BaseFsm�Ҳ�ΪAIFsm.");
                        if (otherFsm.Props.BodyCollider != null && (coll == otherFsm.Props.BodyCollider) && coll != Fsm.ctrlFsm.Props.BodyCollider) {
                            LogTool.NLog("ChaseStateTest", $"����һ�����������Fsm-BodyCollider, ��ײ(����).");
                            LogTool.NLog("ChaseStateTest", "���Ŀ��BaseFsm����, ���ظ�Fsm, ��Overlap���Ϊtrue.");
                            return true;
                        }
                    } else {
                        LogTool.NLog("ChaseStateTest", "����BaseFsm");
                    }
                }
            }
            LogTool.NLog("ChaseStateTest", "û�ҵ�BaseFsm, FsmΪ��, ��Overlap���Ϊfalse.");
            otherFsm = null;
            return false;
        }

        //public float Distance(Rect selfBounds, Collider2D p)
        //{
        //    var b1 = selfBounds;
        //    var b2 = p.bounds;
        //    var maxx = Math.Max(b1.min.x, b2.min.x);
        //    var minx = Math.Min(b1.max.x, b2.max.x);
        //    var disX = maxx - minx;
        //    var maxy = Math.Max(b1.min.y, b2.min.y);
        //    var miny = Math.Min(b1.max.y, b2.max.y);
        //    var disY = maxy - miny;
        //    var mag = new Vector2(disX, disY).magnitude;
        //    return mag;
        //}
    }
}
