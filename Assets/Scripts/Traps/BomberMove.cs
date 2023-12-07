using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BomberMove : MonoBehaviour,ITrapMove
{
    [SerializeField] private List<Transform> bombs=new List<Transform>();
    [SerializeField] private List<Transform> bombPositions=new List<Transform>();
    [SerializeField] private List<ParticleSystem> bombParticles=new List<ParticleSystem>();
    [SerializeField] private float duration;

    private WaitForSeconds waitForSeconds;

    [SerializeField] private int Id;
    [SerializeField] private Ease ease;

    [SerializeField] private float waitforsecond;

    private void Start() 
    {
        waitForSeconds=new WaitForSeconds(waitforsecond);
    }

    private void OnEnable() 
    {
        EventManager.AddIdHandler(GameEvent.OnBombActive,OnBombActive);
        
    }

    private void OnDisable() 
    {
        EventManager.RemoveIdHandler(GameEvent.OnBombActive,OnBombActive);
        
    }

    public void Move()
    {
        StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        yield return waitForSeconds;
        for (int i = 0; i < bombs.Count; i++)
        {
            bombs[i].gameObject.SetActive(true);
            bombs[i].DOJump(bombPositions[i].position,3,1,duration).SetEase(ease).OnComplete(()=>{
                bombs[i].gameObject.SetActive(false);
                bombParticles[i].Play();
                EventManager.Broadcast(GameEvent.OnBombExplode);
            });
            yield return waitForSeconds;            
        }
    }

    //Bomb Track Calistirinca Bombalari Atacak
    private void OnBombActive(int id)
    {
        if(id==Id)
        {
            for (int i = 0; i < bombPositions.Count; i++)
            {
                //Carpilar Acilir. Boylelikle nereye dusecegini Goruruz
                bombPositions[i].gameObject.SetActive(true);
            }
            Move();
        }
       
        //Siren Sesi Gelir. Audio Managerda
    }

    
}
