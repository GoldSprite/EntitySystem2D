using GoldSprite.EntitySystem2D;
using GoldSprite.UFsm;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAttacker : MonoBehaviour
{
    public EntityUFsm22 fsm;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ЙЅЛїУќжа.");
        if (!collision.TryGetComponent<EntityUFsm22>(out EntityUFsm22 otherFsm)) return;
        otherFsm.Command(BaseFsmCommand.Hurt, fsm.Props);
    }
}
