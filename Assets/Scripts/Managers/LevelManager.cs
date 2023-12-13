using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    [Header("Indexes")]
    public int levelIndex;
    
    public GameData gameData;
    public List<GameObject> levels;

    private WaitForSeconds waitForSeconds;

    private void Awake() 
    {
        LoadLevel();
        waitForSeconds=new WaitForSeconds(.5f);
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

    public void LoadNextLevel()
    {
        PlayerPrefs.SetInt("NumberOfLevel", levelIndex + 1);
        PlayerPrefs.SetInt("RealNumberLevel", PlayerPrefs.GetInt("RealNumberLevel", 0) + 1);
        gameData.IndexOfLevel++;
        gameData.testValue++;
        LoadLevel();
        EventManager.Broadcast(GameEvent.OnNextLevel);
    }
    
    public void RestartLevel()
    {
        //LoadLevel();
        Debug.Log("RESTART LEVEL");
        EventManager.Broadcast(GameEvent.OnRestartLevel);
        EventManager.Broadcast(GameEvent.OnStartGame);
        StartCoroutine(WaitforStart());
        //EventManager.Broadcast(GameEvent.OnFalseMove);
    }

    private IEnumerator WaitforStart()
    {
        yield return waitForSeconds;
        gameData.isGameStart=true;
    }
    
    
}
