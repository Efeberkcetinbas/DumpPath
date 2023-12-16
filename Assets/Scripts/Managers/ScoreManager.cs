using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private LevelData levelData;
    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnIncreaseScore, OnIncreaseScore);
        EventManager.AddHandler(GameEvent.OnDecreaseScore,OnDecreaseScore);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnIncreaseScore, OnIncreaseScore);
        EventManager.RemoveHandler(GameEvent.OnDecreaseScore,OnDecreaseScore);
    }
    private void OnIncreaseScore()
    {
        //gameData.score += 50;
        DOTween.To(GetScore,ChangeScore,levelData.score+gameData.increaseScore,.25f).OnUpdate(UpdateUI);
    }

    private void OnDecreaseScore()
    {
        DOTween.To(GetScore,ChangeScore,levelData.score-gameData.undoPrice,.25f).OnUpdate(UpdateUI);
    }

    private int GetScore()
    {
        return levelData.score;
    }

    private void ChangeScore(int value)
    {
        levelData.score=value;
    }

    private void UpdateUI()
    {
        EventManager.Broadcast(GameEvent.OnUIUpdate);
        EventManager.Broadcast(GameEvent.OnCheckUndo);
    }
}
