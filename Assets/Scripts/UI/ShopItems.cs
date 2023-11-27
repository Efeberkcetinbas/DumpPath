using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShopItems : MonoBehaviour
{
    [SerializeField] private List<GameObject> items=new List<GameObject>();
    private WaitForSeconds waitForSeconds;


    private void Start() 
    {
        waitForSeconds=new WaitForSeconds(.25f);

        
    }
    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnShopOpen,OnShopOpen);
        
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnShopOpen,OnShopOpen);

    }


    private void OnShopOpen()
    {
        StartCoroutine(ItemsAnimation());
    }


    private IEnumerator ItemsAnimation()
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].transform.localScale=Vector3.zero;
        }

        for (int i = 0; i < items.Count; i++)
        {
            items[i].transform.DOScale(1f,1f).SetEase(Ease.OutBounce);
            yield return waitForSeconds;
        }
    }
}
