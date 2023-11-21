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

    [Header("Temp Requirements")]
    [SerializeField] private int totalReq;

    [SerializeField] private GameObject upImage;
    [SerializeField] private GameObject downImage;
    [SerializeField] private GameObject leftImage;
    [SerializeField] private GameObject rightImage;

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
        waitForSeconds=new WaitForSeconds(1);
        UpdateRequirement();
    }
    
    private void UpdateRequirement()
    {
        gameData.ReqUp=FindObjectOfType<LevelRequirement>().localReqUp;
        gameData.ReqDown=FindObjectOfType<LevelRequirement>().localReqDown;
        gameData.ReqLeft=FindObjectOfType<LevelRequirement>().localReqLeft;
        gameData.ReqRight=FindObjectOfType<LevelRequirement>().localReqRight;


        gameData.tempUp=gameData.ReqUp;
        gameData.tempDown=gameData.ReqDown;
        gameData.tempRight=gameData.ReqRight;
        gameData.tempLeft=gameData.ReqLeft;
        totalReq=gameData.tempUp+gameData.tempRight+gameData.tempLeft+gameData.tempDown;
        EventManager.Broadcast(GameEvent.OnUIRequirementUpdate);
        CheckZeroCondition();


    }


    private void CheckZeroCondition()
    {
        if(gameData.ReqUp==0) StartCoroutine(CloseImage(upImage));
        if(gameData.ReqDown==0) StartCoroutine(CloseImage(downImage));
        if(gameData.ReqRight==0) StartCoroutine(CloseImage(rightImage));
        if(gameData.ReqLeft==0) StartCoroutine(CloseImage(leftImage));
    }

    private IEnumerator CloseImage(GameObject gameObject)
    {
        yield return waitForSeconds;
        gameObject.transform.DOScale(Vector3.zero,0.25f).OnComplete(()=>gameObject.SetActive(false));
    }
    
    

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.AddHandler(GameEvent.OnRestartLevel,OnRestartLevel);
        EventManager.AddHandler(GameEvent.OnPlayerMove,CheckZeroCondition);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.RemoveHandler(GameEvent.OnRestartLevel,OnRestartLevel);
        EventManager.RemoveHandler(GameEvent.OnPlayerMove,CheckZeroCondition);
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
        UpdateRequirement();
    }

    
    
    
    void ClearData()
    {
        gameData.isGameEnd=true;
        gameData.ProgressNumber=0;
        gameData.levelProgressNumber=0;
        
        playerData.UpMove=0;
        playerData.DownMove=0;
        playerData.LeftMove=0;
        playerData.RightMove=0;
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
