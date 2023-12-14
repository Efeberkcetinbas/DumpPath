using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;

public class ButtonTrigger : Obstacleable
{
    public int id;

    [SerializeField] private bool isUp=false;
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private Transform button;

    [SerializeField] private float newX,oldX,duration;

    private bool isOpen=false;


    public ButtonTrigger()
    {
        canDamageToPlayer=false;
        canStay=false;
    }

    internal override void DoAction(TriggerControl player)
    {
        if(!isOpen)
        {
            EventManager.BroadcastId(GameEvent.OnOpenButton,id);
            button.DOLocalRotate(new Vector3(newX,-90,0),duration);
            particle.Play();
            isOpen=true;
        }
        
    }

    internal override void StopAction(TriggerControl player)
    {
        if(isUp)
        {
            EventManager.BroadcastId(GameEvent.OnCloseButton,id);
            button.DOLocalRotate(new Vector3(oldX,-90,0),duration);
            particle.Play();
        }
    }
}
