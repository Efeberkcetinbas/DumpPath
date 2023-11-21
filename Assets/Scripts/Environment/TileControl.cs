using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileControl : MonoBehaviour
{
    private WaitForSeconds waitForSeconds;
    [SerializeField] private float fallTime;

    private Vector3 startPosition;
    [SerializeField] private float amount;
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
}
