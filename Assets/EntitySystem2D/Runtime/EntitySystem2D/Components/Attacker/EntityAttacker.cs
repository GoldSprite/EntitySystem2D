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
        LogTool.NLog("EntityAttack", "��������.");
        if (collision.TryGetComponent<EntityUFsm22>(out EntityUFsm22 otherFsm) && !targets.Contains(otherFsm)) {
            LogTool.NLog("EntityAttack", "��ʵ��״̬���Ҳ����ѹ��������б�.");
            if (collision == otherFsm.Props.BodyCollider) {
                LogTool.NLog("EntityAttack", "���е���һ��BodyCollider.");
                if (otherFsm.Props.BodyCollider != fsm.Props.BodyCollider) {
                    LogTool.NLog("EntityAttack", "���Ҳ�������BodyCollider, ִ��Ŀ���ܻ�����ӽ��б�.");
                    otherFsm.Command(BaseFsmCommand.Hurt, fsm.Props);
                    targets.Add(otherFsm);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<EntityUFsm22>(out EntityUFsm22 otherFsm) && targets.Contains(otherFsm)) {
            LogTool.NLog("EntityAttack", "��ʵ��״̬�������ѹ��������б�, ���б��Ƴ�.");
            targets.Remove(otherFsm);
        }
    }
}
