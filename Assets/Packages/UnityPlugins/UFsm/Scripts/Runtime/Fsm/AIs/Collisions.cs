using UnityEngine;

namespace GoldSprite.UFsm {
    public class Collisions {
        private AIFsm Fsm;
        public Collisions(AIFsm fsm) { this.Fsm = fsm; }


        public bool IsOutOfLandArea()
        {
            var collBounds = Fsm.ctrlFsm.Props.BodyCollider.bounds;
            var b2 = Fsm.Props.LandArea;
            var rect = b2;
            return collBounds.min.x < rect.min.x || collBounds.max.x > rect.max.x || collBounds.min.y < rect.min.y || collBounds.max.y > rect.max.y;
        }

        public bool TryOverlap(Collider2D collider, out BaseFsm otherFsm)
        {
            Bounds selfBounds = collider.bounds;
            var min = selfBounds.min;
            var max = selfBounds.max;
            var colls = Physics2D.OverlapAreaAll(min, max);
            if (colls.Length != 0) {
                LogTool.NLog("TryOverlapTest", "列表不为空, 遍历判断是否有BaseFsm");
                foreach (var coll in colls) {
                    if (coll.TryGetComponent(out otherFsm) && otherFsm is not AIFsm) {
                        LogTool.NLog("TryOverlapTest", "找到BaseFsm且不为AIFsm.");
                        if (otherFsm.Props.BodyCollider != null && (coll == otherFsm.Props.BodyCollider) && coll != Fsm.ctrlFsm.Props.BodyCollider) {
                            LogTool.NLog("TryOverlapTest", $"且是一个不是自身的Fsm-BodyCollider, 碰撞(距离).");
                            LogTool.NLog("TryOverlapTest", "完成目标BaseFsm查找, 返回该Fsm, 且Overlap结果为true.");
                            return true;
                        }
                    } else {
                        LogTool.NLog("TryOverlapTest", "不是BaseFsm");
                    }
                }
            }
            LogTool.NLog("TryOverlapTest", "没找到BaseFsm, Fsm为空, 且Overlap结果为false.");
            otherFsm = null;
            return false;
        }
    }
}
