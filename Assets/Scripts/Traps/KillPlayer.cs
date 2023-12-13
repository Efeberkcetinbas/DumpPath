using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class KillPlayer : Obstacleable
{
    
    public KillPlayer()
    {
        canStay=false;
        canDamageToPlayer=true;
    }

    internal override void DoAction(TriggerControl player)
    {
        //BUG COZULENE KADAR
        //SceneManager.LoadScene(0);
        //EventManager.Broadcast(GameEvent.OnFalseMove);
        EventManager.Broadcast(GameEvent.OnPlayerDead);
        EventManager.Broadcast(GameEvent.OnOpenFail);
        EventManager.Broadcast(GameEvent.OnFalseMove);
        Debug.Log("PLAYER IS DEAD");        
    }
}
