using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Player : MonoBehaviour
{

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
