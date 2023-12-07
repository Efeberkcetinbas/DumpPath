using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class KillPlayer : Obstacleable
{
    
    public KillPlayer()
    {
        canStay=false;
        canDamageToPlayer=true;
    }

    internal override void DoAction(TriggerControl player)
    {
        EventManager.Broadcast(GameEvent.OnFalseMove);
        Debug.Log("PLAYER IS DEAD");        
    }
}
