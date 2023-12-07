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
    [SerializeField] private Color firstColor;

    [SerializeField] private bool canEnter;
    [SerializeField] private bool canLetter;
    [SerializeField] private bool isTargetForBomb;
    [SerializeField] private int ID;

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnFalseMove,OnFalseMove);
        EventManager.AddIdHandler(GameEvent.OnBombActive,OnBombActive);
        
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnFalseMove,OnFalseMove);
        EventManager.RemoveIdHandler(GameEvent.OnBombActive,OnBombActive);
        
    }
    

    internal override void DoAction(TriggerControl player)
    {
        if(!gameData.isGameEnd)
        {
            if(canEnter && !gameData.isUndo)
            {
                exitParticle.Play();
                meshRenderer.material.DOFade(1,1);
                meshRenderer.material.DOColor(Color.green,1);
                
                //Tween Atayipta da dene
                groundGameObject.DOScaleY(0.3f,0.25f).OnComplete(()=>groundGameObject.DOScaleY(0.5f,0.25f));
                EventManager.Broadcast(GameEvent.OnGround);
                canEnter=false;
            }
            else
            {
                if(gameData.isUndo)
                {
                    meshRenderer.material.DOFade(1,1);
                    meshRenderer.material.DOColor(Color.blue,1);
                    canEnter=true;
                //EventManager.Broadcast(GameEvent.OnGameOver);
                }
                else
                {
                    meshRenderer.material.DOFade(1,1);
                    meshRenderer.material.DOColor(Color.red,1);
                    Debug.Log("GAME IS END");

                }
            }
        }


    }

    //Root.DOLocalRotate(new Vector3(0, 360, 0), .2f, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear);
    internal override void StopAction(TriggerControl player)
    {
        if(gameData.isUndo)
        {
            meshRenderer.material.DOFade(1,1);
            meshRenderer.material.DOColor(Color.blue,1);
            canEnter=true;
        }
        

        //ruzgar efekti
        //exitParticle.Play();
        /*if(playerData.playerUp) transform.DOLocalRotate(new Vector3(360,0,0),.5f,RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear);
        if(playerData.playerDown) transform.DOLocalRotate(new Vector3(-360,0,0),.5f,RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear);
        if(playerData.playerLeft) transform.DOLocalRotate(new Vector3(0,0,360),.5f,RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear);
        if(playerData.playerRight) transform.DOLocalRotate(new Vector3(0,0,-360),.5f,RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear);*/
    }

    private void OnFalseMove()
    {
        meshRenderer.material.DOFade(1,1);
        meshRenderer.material.DOColor(firstColor,1);
        canEnter=true;
    }

    internal void SetGreen()
    {
        if(canEnter && canLetter)
        {
            meshRenderer.material.DOFade(1,0.2f);
            meshRenderer.material.DOColor(Color.green,0.2f);
        }
    }


    private void OnBombActive(int id)
    {
        if(isTargetForBomb && id==ID)
        {
            meshRenderer.material.DOFade(1,0.2f);
            meshRenderer.material.DOColor(Color.red,0.2f);
        }
    }
    
}
