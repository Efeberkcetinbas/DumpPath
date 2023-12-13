using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgrationOfPath : Obstacleable
{
    [SerializeField] private GameObject mesh;
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private GameData gameData;
    [SerializeField] private PlayerData playerData;
    
    public UpgrationOfPath()
    {
        canStay=false;
        canDamageToPlayer=false;
    }

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnFalseMove,OnFalseMove);
        
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnFalseMove,OnFalseMove);
        
    }

    internal override void DoAction(TriggerControl player)
    {   
        playerData.isPathUpgrade=true;
        EventManager.Broadcast(GameEvent.OnGroundsSetGreen);
        EventManager.Broadcast(GameEvent.OnDirectionUpdate);
        gameData.isGameEnd=true;
        mesh.SetActive(false);
        boxCollider.enabled=false;
        Debug.Log("IT WORKS");
    }


    private void OnFalseMove()
    {
        
        mesh.SetActive(true);
        boxCollider.enabled=true;
    }
}
