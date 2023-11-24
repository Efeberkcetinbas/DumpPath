using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgrationOfPath : Obstacleable
{
    public UpgrationOfPath()
    {
        canStay=false;
        canDamageToPlayer=false;
    }

    internal override void DoAction(TriggerControl player)
    {
        EventManager.Broadcast(GameEvent.OnDirectionUpdate);
        Destroy(gameObject);
    }
}
