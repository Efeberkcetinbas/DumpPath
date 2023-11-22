using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GroundTrigger : Obstacleable
{
    public GroundData groundData;
    public GameData gameData;
    

    public GroundTrigger()
    {
        canStay=false;
    }
    [SerializeField] private ParticleSystem exitParticle;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Transform groundGameObject;

    [SerializeField] private bool canEnter;
    private bool canGetPoint=true;

    internal override void DoAction(TriggerControl player)
    {
        if(canEnter && !gameData.isGameEnd)
        {
            Debug.Log("HIT");
            exitParticle.Play();
            meshRenderer.material.DOFade(1,1f);
            meshRenderer.material.DOColor(Color.green,1);
            //groundGameObject.DOPunchScale(Vector3.one,0.25f,5,0.5f);
            EventManager.Broadcast(GameEvent.OnGround);
            if(canGetPoint)
            {
                groundData.tempPathNumber++;
                //EventManager.Broadcast(GameEvent.OnIncreaseScore);
                canGetPoint=false;
            }
        }
        else
        {
            meshRenderer.material.DOFade(1,1f);
            meshRenderer.material.DOColor(Color.red,1);
            //EventManager.Broadcast(GameEvent.OnGameOver);
        }
    }

    //Root.DOLocalRotate(new Vector3(0, 360, 0), .2f, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear);
    internal override void StopAction(TriggerControl player)
    {
        //ruzgar efekti
        //exitParticle.Play();
        /*if(playerData.playerUp) transform.DOLocalRotate(new Vector3(360,0,0),.5f,RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear);
        if(playerData.playerDown) transform.DOLocalRotate(new Vector3(-360,0,0),.5f,RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear);
        if(playerData.playerLeft) transform.DOLocalRotate(new Vector3(0,0,360),.5f,RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear);
        if(playerData.playerRight) transform.DOLocalRotate(new Vector3(0,0,-360),.5f,RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear);*/
    }
    
}
