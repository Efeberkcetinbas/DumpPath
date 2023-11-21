using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    [Header("Indexes")]
    public int levelIndex;
    
    public GameData gameData;
    public List<GameObject> levels;

    private void Awake() 
    {
        LoadLevel();
    }
    

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnLoadNextLevel,LoadNextLevel);
        
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnLoadNextLevel,LoadNextLevel);
    }
    private void LoadLevel()
    {


        levelIndex = PlayerPrefs.GetInt("NumberOfLevel");
        if (levelIndex == levels.Count) levelIndex = 0;
        PlayerPrefs.SetInt("NumberOfLevel", levelIndex);
       

        for (int i = 0; i < levels.Count; i++)
        {
            levels[i].SetActive(false);
        }
        Debug.Log(levelIndex);
        levels[levelIndex].SetActive(true);
    }

    private void LoadNextLevel()
    {
        PlayerPrefs.SetInt("NumberOfLevel", levelIndex + 1);
        PlayerPrefs.SetInt("RealNumberLevel", PlayerPrefs.GetInt("RealNumberLevel", 0) + 1);
        gameData.LevelNumberIndex++;
        LoadLevel();
        EventManager.Broadcast(GameEvent.OnNextLevel);
    }
    
    public void RestartLevel()
    {
        LoadLevel();
        Debug.Log("RESTART LEVEL");
        EventManager.Broadcast(GameEvent.OnRestartLevel);
    }
    
    
}
