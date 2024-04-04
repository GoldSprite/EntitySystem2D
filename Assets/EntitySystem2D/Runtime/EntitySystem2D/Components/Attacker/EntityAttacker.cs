using GoldSprite.EntitySystem2D;
using GoldSprite.UFsm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EntityAttacker : MonoBehaviour {
    public EntityUFsm22 fsm;
    public List<EntityUFsm22> targets;


    private void Start()
    {
        if (targets == null) targets = new();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        TryAttack(collision);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        TryAttack(collision);
    }

    private void TryAttack(Collider2D collision)
    {
        LogTool.NLog("EntityAttack", "攻击命中.");
        if (collision.TryGetComponent<EntityUFsm22>(out EntityUFsm22 otherFsm) && !targets.Contains(otherFsm)) {
            LogTool.NLog("EntityAttack", "是实体状态机且不在已攻击过的列表.");
            if (collision == otherFsm.Props.BodyCollider) {
                LogTool.NLog("EntityAttack", "命中的是一个BodyCollider.");
                if (otherFsm.Props.BodyCollider != fsm.Props.BodyCollider) {
                    LogTool.NLog("EntityAttack", "并且不是自身BodyCollider, 执行目标受击并添加进列表.");
                    otherFsm.Command(BaseFsmCommand.Hurt, fsm.Props);
                    targets.Add(otherFsm);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<EntityUFsm22>(out EntityUFsm22 otherFsm) && targets.Contains(otherFsm)) {
            LogTool.NLog("EntityAttack", "是实体状态机且在已攻击过的列表, 从列表移除.");
            targets.Remove(otherFsm);
        }
    }
}
