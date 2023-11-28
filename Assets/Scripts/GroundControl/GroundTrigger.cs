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
    [SerializeField] private List<Material> materials=new List<Material>();
    List<Material> myMaterials;
    [SerializeField] private Transform groundGameObject;

    [SerializeField] private bool canEnter;

    

    internal override void DoAction(TriggerControl player)
    {
        if(!gameData.isGameEnd)
        {
            if(canEnter && !gameData.isUndo)
            {
                exitParticle.Play();
                for (int i = 0; i < materials.Count; i++)
                {
                    meshRenderer.materials[i].DOFade(1,1f);
                    meshRenderer.materials[i].DOColor(Color.green,1);
                }
                
                //Tween Atayipta da dene
                groundGameObject.DOScaleY(0.8f,0.25f).OnComplete(()=>groundGameObject.DOScaleY(1,0.25f));
                EventManager.Broadcast(GameEvent.OnGround);
                canEnter=false;
            }
            else
            {
                if(gameData.isUndo)
                {
                    for (int i = 0; i < materials.Count; i++)
                    {
                        meshRenderer.materials[i].DOFade(1,1f);
                        meshRenderer.materials[i].DOColor(Color.blue,1);
                        canEnter=true;    
                    }
                //EventManager.Broadcast(GameEvent.OnGameOver);
                }
                else
                {
                    for (int i = 0; i < materials.Count; i++)
                    {
                        meshRenderer.materials[i].DOFade(1,1f);
                        meshRenderer.materials[i].DOColor(Color.red,1);
                    }
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
            for (int i = 0; i < materials.Count; i++)
            {
                meshRenderer.materials[i].DOFade(1,1f);
                meshRenderer.materials[i].DOColor(Color.blue,1);
                canEnter=true;    
            }
        }
        

        //ruzgar efekti
        //exitParticle.Play();
        /*if(playerData.playerUp) transform.DOLocalRotate(new Vector3(360,0,0),.5f,RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear);
        if(playerData.playerDown) transform.DOLocalRotate(new Vector3(-360,0,0),.5f,RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear);
        if(playerData.playerLeft) transform.DOLocalRotate(new Vector3(0,0,360),.5f,RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear);
        if(playerData.playerRight) transform.DOLocalRotate(new Vector3(0,0,-360),.5f,RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear);*/
    }
    
}
