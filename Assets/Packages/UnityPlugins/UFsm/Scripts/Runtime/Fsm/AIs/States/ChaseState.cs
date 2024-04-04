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
                LogTool.NLog("ChaseStateTest", "自身在界外, 不可追击.");
                StateSwitch = false;
            } else
            if (Fsm.collCtrls.TryOverlap(Fsm.Props.VisionRange, out otherFsm)) {
                StateSwitch = true;
                MoveVector = otherFsm.Props.BodyCollider.bounds.center - BodyPos; MoveVector.y = 0;
                LogTool.NLog("ChaseStateTest", "有目标, 进入追击");
            } else {
                LogTool.NLog("ChaseStateTest", "没有一个目标, 退出追击");
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
            Fsm.ctrlFsm.Props.Direction = MoveVector;
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
