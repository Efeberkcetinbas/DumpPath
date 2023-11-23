using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PortalControl : Obstacleable
{

    [SerializeField] private List<GameObject> CageCubes=new List<GameObject>();

    private WaitForSeconds waitForSeconds;
    
    [SerializeField] private BoxCollider boxCollider;

    [SerializeField] private float y;


    public PortalControl()
    {
        canStay=false;

    }

    private void Start() 
    {
        waitForSeconds=new WaitForSeconds(.25f);
        
    }
    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnPortalOpen,OnPortalOpen);
        
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnPortalOpen,OnPortalOpen);
    }

    internal override void DoAction(TriggerControl player)
    {
        player.transform.DOLocalMoveY(y,1);
        Debug.Log("NEW LEVEL");
        //OPEN SUCCESS ENDING EVENT
        //HAVAI FISEKLER OLUR ONSUCCESSOPENDAN ONCE ONSUCCESS EVENT FIRLAT KAMERA HAREKETI VE PARTICLE ICIN
        EventManager.Broadcast(GameEvent.OnSuccess);
    }


    private void OnPortalOpen()
    {
        StartCoroutine(Open());
    }


    private IEnumerator Open()
    {
        for (int i = 0; i < CageCubes.Count; i++)
        {
            CageCubes[i].transform.DOScale(Vector3.zero,0.5f);
            EventManager.Broadcast(GameEvent.OnCageOpen);
            yield return waitForSeconds;
        }

        boxCollider.enabled=true;
    }

}
