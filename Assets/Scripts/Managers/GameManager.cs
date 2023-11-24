using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [Header("Scriptable Data's")]
    public GameData gameData;
    public PlayerData playerData;

    [Header("Player")]
    [SerializeField] private Transform player;
    //Level Progress

    [Header("Temp Requirements")]
    [SerializeField] private int totalReq;
    [SerializeField] private GameObject upImage;
    [SerializeField] private GameObject downImage;
    [SerializeField] private GameObject leftImage;
    [SerializeField] private GameObject rightImage;
    [SerializeField] private List<Transform> directionImages=new List<Transform>();
    

    //Bir Canvas‘ı gizlemek için SetActive(false) yerine enabled=false‘u tercih edin
    public GameObject failPanel;
    [SerializeField] private Ease ease;


    [Header("Open/Close")]
    [SerializeField] private GameObject[] open_close;
    
    //Boss Ball

    private WaitForSeconds waitForSeconds;
    private WaitForSeconds waitDirectionForSeconds;


    private void Awake() 
    {
        ClearData();
        //StarterPack();
    }

    private void Start() 
    {
        waitForSeconds=new WaitForSeconds(1);
        waitDirectionForSeconds=new WaitForSeconds(2);
        UpdateRequirement();
        UpdatePlayerPosition();
    }


    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.AddHandler(GameEvent.OnRestartLevel,OnRestartLevel);
        EventManager.AddHandler(GameEvent.OnPlayerMove,CheckZeroCondition);
        EventManager.AddHandler(GameEvent.OnPlayerMove,OnPlayerMove);
        EventManager.AddHandler(GameEvent.OnSuccess,OnSuccess);
        EventManager.AddHandler(GameEvent.OnUpdateReqDirection,OnUpdateReqDirection);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.RemoveHandler(GameEvent.OnRestartLevel,OnRestartLevel);
        EventManager.RemoveHandler(GameEvent.OnPlayerMove,CheckZeroCondition);
        EventManager.RemoveHandler(GameEvent.OnPlayerMove,OnPlayerMove);
        EventManager.RemoveHandler(GameEvent.OnSuccess,OnSuccess);
        EventManager.RemoveHandler(GameEvent.OnUpdateReqDirection,OnUpdateReqDirection);
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

    private void OnUpdateReqDirection()
    {
        StartCoroutine(StartNewReq());
    }

    private IEnumerator StartNewReq()
    {
        yield return waitDirectionForSeconds;
        UpdateRequirement();

        for (int i = 0; i < directionImages.Count; i++)
        {
            directionImages[i].gameObject.SetActive(true);
            directionImages[i].DOScale(Vector3.one,0.2f);
        }
        playerData.UpMove=0;
        playerData.DownMove=0;
        playerData.LeftMove=0;
        playerData.RightMove=0;
    }



    private void UpdatePositionOfDirections()
    {
        directionImages.Shuffle(directionImages.Count);
    }

    private void UpdatePlayerPosition()
    {
        player.position=FindObjectOfType<LevelPlayerPosition>().position;
    }

    private void OnPlayerMove()
    {
        totalReq--;
        if(totalReq==0)
        {
            EventManager.Broadcast(GameEvent.OnPortalOpen);
        }
    }


    private void CheckZeroCondition()
    {
        if(gameData.ReqUp==0) StartCoroutine(CloseImage(upImage));
        if(gameData.ReqDown==0) StartCoroutine(CloseImage(downImage));
        if(gameData.ReqLeft==0) StartCoroutine(CloseImage(leftImage));
        if(gameData.ReqRight==0) StartCoroutine(CloseImage(rightImage));
    }

    private IEnumerator CloseImage(GameObject gameObject)
    {
        yield return waitForSeconds;
        gameObject.transform.DOScale(Vector3.zero,0.25f).OnComplete(()=>gameObject.SetActive(false));
    }
    
    
    private void OnSuccess()
    {
        StartCoroutine(OpenSuccessPanel());
    }

    private IEnumerator OpenSuccessPanel()
    {
        yield return waitForSeconds;
        EventManager.Broadcast(GameEvent.OnOpenSuccess);
    }
    
    
    

    private void OnRestartLevel()
    {
        OnNextLevel();
        failPanel.transform.localScale=Vector3.zero;
        failPanel.SetActive(false);
    }
  

    

    



    private void OnNextLevel()
    {
        ClearData();
        //Startda da kullaniyorum. Starter Pack Methoduna Al
        UpdateRequirement();
        //UpdatePositionOfDirections();
        UpdatePlayerPosition();
    }

    
    
    
    void ClearData()
    {
        gameData.isGameEnd=true;
        gameData.ProgressNumber=0;
        gameData.levelProgressNumber=0;

        for (int i = 0; i < directionImages.Count; i++)
        {
            directionImages[i].gameObject.SetActive(true);
            directionImages[i].localScale=Vector3.one;
        }
        
        playerData.UpMove=0;
        playerData.DownMove=0;
        playerData.LeftMove=0;
        playerData.RightMove=0;

        failPanel.SetActive(false);
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
