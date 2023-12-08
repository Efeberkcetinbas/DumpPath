using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TileControl : Obstacleable
{
    private WaitForSeconds waitForSeconds;
    [SerializeField] private float fallTime;

    private Vector3 startPosition;
    [SerializeField] private float amount;

    [SerializeField] private MeshRenderer meshRenderer;

    public TileControl()
    {
        canStay=false;
    }
    private void Start() 
    {
        waitForSeconds=new WaitForSeconds(fallTime);
        startPosition=this.transform.position;
        this.transform.position+=Vector3.up*amount;
        StartCoroutine(FloatDown());
    }


    private IEnumerator FloatDown()
    {
        while (this.transform.position.y>startPosition.y+0.05)
        {
            this.transform.position=Vector3.Lerp(this.transform.position,startPosition,0.1f);
            yield return waitForSeconds;
        }

        this.transform.position=startPosition;
    }

    internal override void DoAction(TriggerControl player)
    {
        meshRenderer.material.DOFade(1,1);
        meshRenderer.material.DOColor(Color.green,1);
    }
}
