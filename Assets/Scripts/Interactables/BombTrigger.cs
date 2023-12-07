using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTrigger : Obstacleable
{
    [SerializeField] private int id;
    public BombTrigger()
    {
        canStay=false;
    }

    internal override void DoAction(TriggerControl player)
    {
        EventManager.BroadcastId(GameEvent.OnBombActive,id);
        Destroy(gameObject);
    }
}
