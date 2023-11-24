using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionOrder : MonoBehaviour
{
    [SerializeField] private List<LevelRequirement> directions=new List<LevelRequirement>();

    private int index;
    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnDirectionUpdate,OnDirectionUpdate);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnDirectionUpdate,OnDirectionUpdate);
    }

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
}
