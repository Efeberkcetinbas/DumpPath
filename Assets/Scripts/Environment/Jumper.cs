using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Jumper : Obstacleable
{
    [SerializeField] private Transform jumperMesh;
    [SerializeField] private Vector3 targetPoint;
    [SerializeField] private Ease easePlayer,easeJumper;
    [SerializeField] private float power,duration,jumperY,oldJumperY;
    [SerializeField] private int jumpAmount;
    [SerializeField] private ParticleSystem particle;

    private bool canJump=true;

    public Jumper()
    {
        canStay=false;
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
        if(canJump)
        {
            particle.Play();
            player.transform.DOJump(targetPoint,power,jumpAmount,duration).SetEase(easePlayer);
            canJump=false;
        }
        
    }

    internal override void StopAction(TriggerControl player)
    {
        jumperMesh.DOScaleY(jumperY,0.5f).SetEase(easeJumper);
        EventManager.Broadcast(GameEvent.OnJump);
        
    }

    private void OnFalseMove()
    {
        canJump=true;
        jumperMesh.DOScaleY(oldJumperY,0.25f).SetEase(easeJumper);
    }


}
