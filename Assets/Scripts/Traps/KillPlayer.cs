using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class KillPlayer : Obstacleable
{
    private bool isPlayerDead=false;
    public KillPlayer()
    {
        canStay=false;
        canDamageToPlayer=true;
    }

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnStartGame,OnStartGame);
        EventManager.AddHandler(GameEvent.OnRestartLevel,OnRestartLevel);
        
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnStartGame,OnStartGame);
        EventManager.RemoveHandler(GameEvent.OnRestartLevel,OnRestartLevel);
        
    }

    internal override void DoAction(TriggerControl player)
    {
        if(!isPlayerDead)
        {
            EventManager.Broadcast(GameEvent.OnPlayerDead);
            EventManager.Broadcast(GameEvent.OnOpenFail);
            EventManager.Broadcast(GameEvent.OnFalseMove);
            Debug.Log("PLAYER IS DEAD");        
            isPlayerDead=true;
        }
        
    }

    private void OnStartGame()
    {
        isPlayerDead=false;
    }

    private void OnRestartLevel()
    {
        isPlayerDead=false;
    }
}
