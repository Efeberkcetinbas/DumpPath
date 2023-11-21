using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnIncreaseScore, OnIncreaseScore);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnIncreaseScore, OnIncreaseScore);
    }
    void OnIncreaseScore()
    {
        //gameData.score += 50;
        DOTween.To(GetScore,ChangeScore,gameData.score+gameData.increaseScore,1f).OnUpdate(UpdateUI);
    }

    private int GetScore()
    {
        return gameData.score;
    }

    private void ChangeScore(int value)
    {
        gameData.score=value;
    }

    private void UpdateUI()
    {
        EventManager.Broadcast(GameEvent.OnUIUpdate);
    }
}
