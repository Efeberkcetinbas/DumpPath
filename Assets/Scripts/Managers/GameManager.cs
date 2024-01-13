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
    [SerializeField] private GameObject upImage;
    [SerializeField] private GameObject downImage;
    [SerializeField] private GameObject leftImage;
    [SerializeField] private GameObject rightImage;
    [SerializeField] private List<Transform> directionImages=new List<Transform>();
    
    [SerializeField] private Transform directionPanel;
    

    //Bir Canvas‘ı gizlemek için SetActive(false) yerine enabled=false‘u tercih edin
    public GameObject failPanel;
    [SerializeField] private Ease ease;


    [Header("Open/Close")]
    [SerializeField] private GameObject[] open_close;

    [Header("Guide")]
    public List<MeshRenderer> selectedPath=new List<MeshRenderer>();
    [SerializeField] private Material selectedMaterial;
    private GuideCube guideCube;
    
    //Boss Ball
    

    private WaitForSeconds waitForSeconds;
    private WaitForSeconds waitDirectionForSeconds;
    private WaitForSeconds waitForList;


    private void Awake() 
    {
        ClearData();
        //StarterPack();
    }

    private void Start() 
    {
        waitForList=new WaitForSeconds(.5f);
        waitForSeconds=new WaitForSeconds(1);
        waitDirectionForSeconds=new WaitForSeconds(2);
        UpdatePositionOfDirections();
        UpdateRequirement();
        UpdatePlayerPosition();
        EventManager.Broadcast(GameEvent.OnIncreaseScore);
    }


    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.AddHandler(GameEvent.OnPlayerMove,CheckZeroCondition);
        EventManager.AddHandler(GameEvent.OnPlayerMove,OnPlayerMove);
        EventManager.AddHandler(GameEvent.OnSuccess,OnSuccess);
        EventManager.AddHandler(GameEvent.OnUpdateReqDirection,OnUpdateReqDirection);
        EventManager.AddHandler(GameEvent.OnUndo,OnUndo);
        EventManager.AddHandler(GameEvent.OnFalseMove,OnFalseMove);
        EventManager.AddHandler(GameEvent.OnPlayerDead,OnPlayerDead);
        EventManager.AddHandler(GameEvent.OnRestartLevel,OnRestartLevel);

    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.RemoveHandler(GameEvent.OnPlayerMove,CheckZeroCondition);
        EventManager.RemoveHandler(GameEvent.OnPlayerMove,OnPlayerMove);
        EventManager.RemoveHandler(GameEvent.OnSuccess,OnSuccess);
        EventManager.RemoveHandler(GameEvent.OnUpdateReqDirection,OnUpdateReqDirection);
        EventManager.RemoveHandler(GameEvent.OnUndo,OnUndo);
        EventManager.RemoveHandler(GameEvent.OnFalseMove,OnFalseMove);
        EventManager.RemoveHandler(GameEvent.OnPlayerDead,OnPlayerDead);
        EventManager.RemoveHandler(GameEvent.OnRestartLevel,OnRestartLevel);

    }

    private void OnUndo()
    {
        CheckOneCondition();
        EventManager.Broadcast(GameEvent.OnUIRequirementUpdate);
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
        //gameData.totalReq=gameData.tempUp+gameData.tempRight+gameData.tempLeft+gameData.tempDown;
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
        playerData.isPathUpgrade=false;
    }


    //Reklamdan sonra calisacak

    public void StartGuidetation()
    {
        guideCube=FindObjectOfType<GuideCube>();
        selectedPath.Clear();
        selectedPath=guideCube.chosenPath;
        StartCoroutine(StartGuide());
    }

    private IEnumerator StartGuide()
    {
        for (int i = 0; i < selectedPath.Count; i++)
        {
            yield return waitForList;
            selectedPath[i].material=selectedMaterial;
        }
    }

    private void UpdatePositionOfDirections()
    {
        //directionImages.Shuffle(directionImages.Count);
        ShuffleParentsAndChangeChildOrder();
    }

    void ShuffleParentsAndChangeChildOrder()
    {
        directionImages.Shuffle(directionImages.Count);
        int child=directionPanel.childCount;
        for (int i = 0; i < child; i++)
        {
            directionImages[i].SetSiblingIndex(i);
        }
    }

    private void UpdatePlayerPosition()
    {
        player.position=FindObjectOfType<LevelPlayerPosition>().position;
        player.transform.DOMoveY(0.5f,0.2f);
        UpdateTotalDirection();
    }

    private void UpdateTotalDirection()
    {
        gameData.totalReq=FindObjectOfType<LevelPlayerPosition>().totalDirectionNumber;
        EventManager.Broadcast(GameEvent.OnUpdateTotalNumber);
    }

    private void OnPlayerMove()
    {
        gameData.totalReq--;
        EventManager.Broadcast(GameEvent.OnUpdateTotalNumber);
        if(gameData.totalReq==0)
        {
            gameData.isGameEnd=true;
            playerData.playerCanMove=false;
            EventManager.Broadcast(GameEvent.OnPortalOpen);
        }
    }

    private void OnPlayerDead()
    {
        gameData.isGameEnd=true;
    }

    private void CheckOneCondition()
    {
        if(gameData.ReqUp>0) StartCoroutine(ImageActivity(upImage,true,false,Vector3.one));
        if(gameData.ReqDown>0) StartCoroutine(ImageActivity(downImage,true,false,Vector3.one));
        if(gameData.ReqLeft>0) StartCoroutine(ImageActivity(leftImage,true,false,Vector3.one));
        if(gameData.ReqRight>0) StartCoroutine(ImageActivity(rightImage,true,false,Vector3.one));
    }

    private void CheckZeroCondition()
    {
        if(gameData.ReqUp==0) StartCoroutine(ImageActivity(upImage,false,true,Vector3.zero));
        if(gameData.ReqDown==0) StartCoroutine(ImageActivity(downImage,false,true,Vector3.zero));
        if(gameData.ReqLeft==0) StartCoroutine(ImageActivity(leftImage,false,true,Vector3.zero));
        if(gameData.ReqRight==0) StartCoroutine(ImageActivity(rightImage,false,true,Vector3.zero));
    }

    private IEnumerator ImageActivity(GameObject gameObject,bool val,bool zero,Vector3 scale)
    {
        yield return waitForSeconds;
        if(zero)
            gameObject.transform.DOScale(scale,0.25f).OnComplete(()=>gameObject.SetActive(val));
        else
        {
            gameObject.SetActive(true);
            gameObject.transform.DOScale(scale,0.25f);
        }
            
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
    
    
    

   
  

    
    private void OnFalseMove()
    {
        StartCoroutine(WaitFalseMove());
    }

    private IEnumerator WaitFalseMove()
    {
        yield return null;
        playerData.UpMove=0;
        playerData.DownMove=0;
        playerData.LeftMove=0;
        playerData.RightMove=0;

        UpdateRequirement();
        UpdatePlayerPosition();
        CheckOneCondition();
    }
    



    private void OnNextLevel()
    {
        ClearData();
        //Startda da kullaniyorum. Starter Pack Methoduna Al
        UpdateRequirement();
        UpdatePositionOfDirections();
        //UpdatePositionOfDirections();
        UpdatePlayerPosition();
    }

    //Sometimes Player Position Restart False, So it checks one more time
    private void OnRestartLevel()
    {
        //Startda da kullaniyorum. Starter Pack Methoduna Al
        UpdateRequirement();
        UpdatePlayerPosition();
    }
    
    
    void ClearData()
    {
        gameData.isGameEnd=true;
        gameData.isLightLevel=false;
        gameData.isTextLevel=false;
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
        playerData.playerCanMove=true;
        failPanel.SetActive(false);
        playerData.isPathUpgrade=false;

        
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
