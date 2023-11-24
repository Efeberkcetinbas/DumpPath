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

    private void Start() 
    {
        waitForSeconds=new WaitForSeconds(3);
    }

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnBombActive,OnBombActive);
        
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnBombActive,OnBombActive);
        
    }

    public void Move()
    {
        StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        for (int i = 0; i < bombs.Count; i++)
        {
            bombs[i].gameObject.SetActive(true);
            bombs[i].DOLocalJump(bombPositions[i].position,3,1,duration).OnComplete(()=>{
                bombs[i].gameObject.SetActive(false);
                bombParticles[i].Play();
                EventManager.Broadcast(GameEvent.OnBombExplode);
            });
            yield return waitForSeconds;            
        }
    }

    //Bomb Track Calistirinca Bombalari Atacak
    private void OnBombActive()
    {
        for (int i = 0; i < bombPositions.Count; i++)
        {
            //Carpilar Acilir. Boylelikle nereye dusecegini Goruruz
            bombPositions[i].gameObject.SetActive(true);
        }
        

        Move();
        //Siren Sesi Gelir. Audio Managerda
    }

    
}
