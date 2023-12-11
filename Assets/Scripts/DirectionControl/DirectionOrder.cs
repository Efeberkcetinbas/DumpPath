using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionOrder : MonoBehaviour
{
    [SerializeField] private List<LevelRequirement> directions=new List<LevelRequirement>();
    [SerializeField] private List<GroundTrigger> grounds=new List<GroundTrigger>();

    private int index;

    private WaitForSeconds waitForSeconds;

    private void Start() 
    {
        waitForSeconds=new WaitForSeconds(.25f);
        
    }
    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnDirectionUpdate,OnDirectionUpdate);
        EventManager.AddHandler(GameEvent.OnGroundsSetGreen,OnGroundsSetGreen);
        EventManager.AddHandler(GameEvent.OnFalseMove,OnFalseMove);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnDirectionUpdate,OnDirectionUpdate);
        EventManager.RemoveHandler(GameEvent.OnGroundsSetGreen,OnGroundsSetGreen);
        EventManager.RemoveHandler(GameEvent.OnFalseMove,OnFalseMove);
    }

    // !!!!! COK UZUN YOLLAR OLURSA DIGER MAPI DIRECTION LEVEL REQ ALTINA ATARSIN. BU SAYEDE DRAW CALLS VE BATCHING AZALIR. MAP ILERLEDIKCE DOGAR YANI
    private void OnDirectionUpdate()
    {
        if(index>=directions.Count)
        {
            return;
        }
        index++;

        for (int i = 0; i < directions.Count; i++)
        {
            directions[i].gameObject.SetActive(false);
        }

        directions[index].gameObject.SetActive(true);
        EventManager.Broadcast(GameEvent.OnUpdateReqDirection);
    }

    private void OnGroundsSetGreen()
    {
        StartCoroutine(SetGroundsGreen());
    }

    private IEnumerator SetGroundsGreen()
    {
        for (int i = 0; i < grounds.Count; i++)
        {
            yield return waitForSeconds;
            grounds[i].SetGreen();
        }
    }

    private void OnFalseMove()
    {
        for (int i = 0; i < directions.Count; i++)
        {
            directions[i].gameObject.SetActive(false);
        }

        directions[0].gameObject.SetActive(true);
        index=0;
    }
}
