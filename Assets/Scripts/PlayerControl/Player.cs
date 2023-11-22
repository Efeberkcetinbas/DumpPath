using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{

    [SerializeField] private ParticleSystem upParticle;
    [SerializeField] private ParticleSystem downParticle;
    [SerializeField] private ParticleSystem leftParticle;
    [SerializeField] private ParticleSystem rightParticle;

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnPlayerUp,OnPlayerUp);
        EventManager.AddHandler(GameEvent.OnPlayerDown,OnPlayerDown);
        EventManager.AddHandler(GameEvent.OnPlayerLeft,OnPlayerLeft);
        EventManager.AddHandler(GameEvent.OnPlayerRight,OnPlayerRight);
        
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnPlayerUp,OnPlayerUp);
        EventManager.RemoveHandler(GameEvent.OnPlayerDown,OnPlayerDown);
        EventManager.RemoveHandler(GameEvent.OnPlayerLeft,OnPlayerLeft);
        EventManager.RemoveHandler(GameEvent.OnPlayerRight,OnPlayerRight);
    }


    private void OnPlayerLeft()
    {
        leftParticle.Play();
    }

    private void OnPlayerRight()
    {
        rightParticle.Play();
    }

    private void OnPlayerUp()
    {
        upParticle.Play();
    }

    private void OnPlayerDown()
    {
        downParticle.Play();
    }

    /*[SerializeField] private GameObject increaseScorePrefab;
    [SerializeField] private Transform pointPos;

    [Header("Data's")]
    [SerializeField] private GameData gameData;

    
    private void StartCoinMove()
    {
        GameObject coin=Instantiate(increaseScorePrefab,pointPos.transform.position,increaseScorePrefab.transform.rotation);
        coin.transform.LookAt(Camera.main.transform);
        var pos=coin.transform.localPosition;
        coin.transform.DOLocalJump(new Vector3(pos.x,pos.y+2,pos.z),1,1,1,false);
        //coin.transform.DOScale(Vector3.zero,1.5f);
        coin.transform.GetChild(0).GetComponent<TextMeshPro>().text=" + " + gameData.increaseScore.ToString();
        coin.transform.GetChild(0).GetComponent<TextMeshPro>().DOFade(0,1.5f).OnComplete(()=>coin.transform.GetChild(0).gameObject.SetActive(false));
        Destroy(coin,2);
    }*/
}
