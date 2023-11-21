using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [Header("Scriptable Data's")]
    public GameData gameData;
    public PlayerData playerData;

    //Level Progress


    [Header("Game Ending")]
    public GameObject successPanel;

    //Bir Canvas‘ı gizlemek için SetActive(false) yerine enabled=false‘u tercih edin
    public GameObject failPanel;
    [SerializeField] private Ease ease;


    [Header("Open/Close")]
    [SerializeField] private GameObject[] open_close;
    //Boss Ball

    private WaitForSeconds waitForSeconds;


    private void Awake() 
    {
        ClearData();
        //StarterPack();
    }

    private void Start() 
    {
        waitForSeconds=new WaitForSeconds(2);
    }
    
    
    

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.AddHandler(GameEvent.OnRestartLevel,OnRestartLevel);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.RemoveHandler(GameEvent.OnRestartLevel,OnRestartLevel);
    }
   

    private void OnRestartLevel()
    {
        OnNextLevel();
        failPanel.transform.localScale=Vector3.zero;
        failPanel.SetActive(false);
    }
  

    private IEnumerator OpenFailPanel()
    {
        yield return waitForSeconds;
        OnGameOver();
    }
   

  

    



    void OnGameOver()
    {
        failPanel.SetActive(true);
        failPanel.transform.DOScale(Vector3.one,1f).SetEase(ease);
        playerData.playerCanMove=false;
        gameData.isGameEnd=true;
    }
    private void OnNextLevel()
    {
        ClearData();
        EventManager.Broadcast(GameEvent.OnUIRequirementUpdate);
    }

    
    
    
    void ClearData()
    {
        gameData.isGameEnd=true;
        gameData.ProgressNumber=0;
        gameData.levelProgressNumber=0;
    }


    public void OpenClose(GameObject[] gameObjects,bool canOpen)
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            if(canOpen)
                gameObjects[i].SetActive(true);
            else
                gameObjects[i].SetActive(false);
        }
    }

    
}
