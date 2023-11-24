using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTrigger : Obstacleable
{
    public BombTrigger()
    {
        canStay=false;
    }

    internal override void DoAction(TriggerControl player)
    {
        EventManager.Broadcast(GameEvent.OnBombActive);
        Destroy(gameObject);
    }
}
