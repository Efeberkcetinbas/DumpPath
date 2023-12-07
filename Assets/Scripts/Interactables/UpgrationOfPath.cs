using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgrationOfPath : Obstacleable
{
    [SerializeField] private GameObject mesh;
    [SerializeField] private BoxCollider boxCollider;
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
        EventManager.Broadcast(GameEvent.OnGroundsSetGreen);
        EventManager.Broadcast(GameEvent.OnDirectionUpdate);
        mesh.SetActive(false);
        boxCollider.enabled=false;
    }


    private void OnFalseMove()
    {
        mesh.SetActive(true);
        boxCollider.enabled=true;
    }
}
