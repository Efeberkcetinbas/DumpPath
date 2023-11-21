using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonTrigger : Obstacleable
{
    public int id;

    private MeshRenderer meshRenderer;

    [SerializeField] private Material greenMat,redMat;

    [SerializeField] private bool isUp=false;
    [SerializeField] private ParticleSystem particle;

    private float oldScale;



    private void Start() 
    {
        meshRenderer=GetComponent<MeshRenderer>();
        oldScale=transform.localScale.y;
    }

    public ButtonTrigger()
    {
        canDamageToPlayer=false;
        canStay=false;
    }

    internal override void DoAction(TriggerControl player)
    {
            EventManager.BroadcastId(GameEvent.OnOpenButton,id);
            meshRenderer.material=greenMat;
            particle.Play();
            transform.DOScaleY(oldScale/1.5f,0.5f);
    }

    internal override void StopAction(TriggerControl player)
    {
        if(isUp)
        {
            EventManager.BroadcastId(GameEvent.OnCloseButton,id);
            meshRenderer.material=redMat;
            particle.Play();
            transform.DOScaleY(oldScale,0.5f);
        }
    }
}
