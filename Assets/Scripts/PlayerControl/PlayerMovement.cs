using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private GameData gameData;
    public Transform player;

    private float beginX,beginZ,endX,endZ,valX,valZ;

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnUndoBegin,OnUndoBegin);
        EventManager.AddHandler(GameEvent.OnUndoEnd,OnUndoEnd);
        
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnUndoBegin,OnUndoBegin);
        EventManager.RemoveHandler(GameEvent.OnUndoEnd,OnUndoEnd);
        
    }
    private void OnUndoBegin()
    {
        beginX=player.transform.position.x;
        beginZ=player.transform.position.z;
    }

    private void OnUndoEnd()
    {
        endX=player.transform.position.x;
        endZ=player.transform.position.z;
        valX=beginX-endX;
        valZ=beginZ-endZ;

        Debug.Log("VAL X : " + valX + " / " + "VAL Z : " + valZ);
        
        CheckMove();
    }

    private void CheckMove()
    {
        if(valX>0)
        {
            playerData.RightMove-=Mathf.Round((Mathf.Abs(valX)*100)/100);
            gameData.ReqRight+=Mathf.Round((Mathf.Abs(valX)*100)/100);
            gameData.totalReq+=Mathf.Round((Mathf.Abs(valX)*100)/100);
            EventManager.Broadcast(GameEvent.OnPlayerRight);
        } 
        if(valX<0)
        {
            playerData.LeftMove-= Mathf.Round((Mathf.Abs(valX)*100)/100);
            gameData.ReqLeft+=Mathf.Round((Mathf.Abs(valX)*100)/100);
            gameData.totalReq+=Mathf.Round((Mathf.Abs(valX)*100)/100);
            EventManager.Broadcast(GameEvent.OnPlayerLeft);
        } 

        if(valZ>0)
        {
            playerData.UpMove-= Mathf.Round((Mathf.Abs(valZ)*100)/100);
            gameData.ReqUp+=Mathf.Round((Mathf.Abs(valZ)*100)/100);
            gameData.totalReq+=Mathf.Round((Mathf.Abs(valZ)*100)/100);
            EventManager.Broadcast(GameEvent.OnPlayerUp);
        } 
        
        if(valZ<0)
        {
            playerData.DownMove-= Mathf.Round((Mathf.Abs(valZ)*100)/100);
            gameData.ReqDown+=Mathf.Round((Mathf.Abs(valZ)*100)/100);
            gameData.totalReq+=Mathf.Round((Mathf.Abs(valZ)*100)/100);
            EventManager.Broadcast(GameEvent.OnPlayerDown);
        }

        EventManager.Broadcast(GameEvent.OnUndo);

    }

}
