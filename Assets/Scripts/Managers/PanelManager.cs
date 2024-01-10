using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class PanelManager : MonoBehaviour
{
    [SerializeField] private RectTransform StartPanel,DirectionPanel,MovePanel,StorePanel,SuccessPanel,FailPanel,ScoreImage;

    [SerializeField] private GameObject[] sceneUI;
    [SerializeField] private GameObject directionText;
    [SerializeField] private Image Fade;
    [SerializeField] private Image Light;

    [SerializeField] private float StartX,StartY,StoreX,StoreY,ScoreX,ScoreOldX,duration;

    [SerializeField] private GameData gameData;
    [SerializeField] private PlayerData playerData;
    [Header("Success List")]
    [SerializeField] private Ease ease;
    [SerializeField] private List<Transform> successElements=new List<Transform>();
    [SerializeField] private List<Image> stars=new List<Image>();
    [SerializeField] private List<Transform> failElements=new List<Transform>();

    //Waitforseconds
    private WaitForSeconds waitForSeconds1;
    private WaitForSeconds waitForSeconds2;
    private WaitForSeconds waitForSecondsScore;
    private WaitForSeconds waitForFill;



    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.AddHandler(GameEvent.OnSuccess,OnSuccess);
        EventManager.AddHandler(GameEvent.OnOpenSuccess,OnOpenSuccess);
        EventManager.AddHandler(GameEvent.OnOpenFail,OnOpenFail);
        EventManager.AddHandler(GameEvent.OnDisableLetter,OnDisableLetter);
        EventManager.AddHandler(GameEvent.OnUndoBegin,OnUndoBegin);
        EventManager.AddHandler(GameEvent.OnRestartLevel,OnRestartLevel);
        EventManager.AddHandler(GameEvent.OnFalseMove,OnFalseMove);

    }


    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.RemoveHandler(GameEvent.OnSuccess,OnSuccess);
        EventManager.RemoveHandler(GameEvent.OnOpenFail,OnOpenFail);
        EventManager.RemoveHandler(GameEvent.OnOpenSuccess,OnOpenSuccess);
        EventManager.RemoveHandler(GameEvent.OnDisableLetter,OnDisableLetter);
        EventManager.RemoveHandler(GameEvent.OnUndoBegin,OnUndoBegin);
        EventManager.RemoveHandler(GameEvent.OnRestartLevel,OnRestartLevel);
        EventManager.RemoveHandler(GameEvent.OnFalseMove,OnFalseMove);
    }

    private void Start() 
    {
        //UICanvas.SetActive(false);
        SceneUI(false);

        waitForSeconds1=new WaitForSeconds(2);
        waitForSeconds2=new WaitForSeconds(.5f);
        waitForSecondsScore=new WaitForSeconds(2);
        waitForFill=new WaitForSeconds(1);
    }

    private void OnSuccess()
    {
        DirectionPanel.DOAnchorPos(new Vector2(0,500),duration);
        MovePanel.DOAnchorPos(new Vector2(115,500),duration);
    }

    private void OnDisableLetter()
    {
        directionText.SetActive(false);
    }

    
    
    public void StartGame() 
    {
        //EventManager.Broadcast(GameEvent.OnButtonClicked);
        StartPanel.DOAnchorPos(new Vector2(StartX,StartY),duration).OnComplete(()=>{
            SceneUI(true);
            ScoreImage.DOAnchorPosX(ScoreOldX,0.5f);
            StartCoroutine(ScoreMove());
            //player.transform.DOMoveY(0.5f,0.5f).OnComplete(()=>playerData.playerCanMove=true);
            gameData.isGameEnd=false;
            gameData.isGameStart=true;
            
            //EventManager.Broadcast(GameEvent.OnStartGame);
            if(gameData.isLightLevel)
            {
                Light.gameObject.SetActive(true);
                Light.color=new Color(0,0,0,0);
                Light.DOFade(1,gameData.lightTime); 
            }

            else
            {
                Light.gameObject.SetActive(false);
            }

            //StartPanel.gameObject.SetActive(false);
        });
        DirectionPanel.gameObject.SetActive(true);
        DirectionPanel.DOAnchorPos(new Vector2(0,-500),duration);
        MovePanel.gameObject.SetActive(true);
        MovePanel.DOAnchorPos(new Vector2(115,-500),duration);
    
        if(gameData.isTextLevel)
            directionText.SetActive(true);
    }

    

    
    private void OnUndoBegin()
    {
        ScoreImage.DOAnchorPosX(ScoreOldX,0.5f);
        StartCoroutine(ScoreMove());
    }

    private void OnRestartLevel()
    {
        FailPanel.DOAnchorPos(new Vector2(2500,0),0.1f).OnComplete(()=>{
            for (int i = 0; i < failElements.Count; i++)
            {
                failElements[i].transform.localScale=Vector3.zero;
                
            }
            FailPanel.gameObject.SetActive(false);
         });
        SceneUI(true);
        if(gameData.isLightLevel)
        {
            Light.gameObject.SetActive(true);
            //DOTween.Kill(Light);
            Light.color=new Color(0,0,0,0);
            Light.DOFade(1,gameData.lightTime); 
        }

        else
        {
            Light.gameObject.SetActive(false);
        }

        if(gameData.isTextLevel)
            directionText.SetActive(true);
        
        /*StartPanel.gameObject.SetActive(true);
        StartPanel.transform.localScale=Vector3.one;
        StartPanel.DOAnchorPos(Vector2.zero,0.1f).OnComplete(()=>EventManager.Broadcast(GameEvent.OnIncreaseScore));

        StartCoroutine(Blink(Fade.gameObject,Fade));
        for (int i = 0; i < sceneUI.Length; i++)
        {
            sceneUI[i].SetActive(false);
        }*/
    }

    private void OnNextLevel()
    {
        if(gameData.isLightLevel)
        {
            DOTween.Kill(Light);
        }
        SuccessPanel.DOAnchorPos(new Vector2(2500,0),0.1f).OnComplete(()=>{
            for (int i = 0; i < successElements.Count; i++)
            {
                successElements[i].transform.localScale=Vector3.zero;
                
            }
            SuccessPanel.gameObject.SetActive(false);
        });
        
        StartPanel.gameObject.SetActive(true);
        StartPanel.transform.localScale=Vector3.one;
        StartPanel.DOAnchorPos(Vector2.zero,0.1f).OnComplete(()=>EventManager.Broadcast(GameEvent.OnIncreaseScore));

        StartCoroutine(Blink(Fade.gameObject,Fade));
        for (int i = 0; i < sceneUI.Length; i++)
        {
            sceneUI[i].SetActive(false);
        }

    }

    private void OnFalseMove()
    {
        if(gameData.isLightLevel && !gameData.isGameEnd)
        {
            Light.gameObject.SetActive(true);
            DOTween.Kill(Light);
            Light.color=new Color(0,0,0,0);
            Light.DOFade(1,gameData.lightTime); 
        }
    }

    

    private void SceneUI(bool val)
    {
        for (int i = 0; i < sceneUI.Length; i++)
        {
            sceneUI[i].SetActive(val);
        }
    }

    private void OnOpenSuccess()
    {
        Light.gameObject.SetActive(false);
        //Here Goes Some Improvements
        if(gameData.isTextLevel)
            directionText.SetActive(false);
        
        SceneUI(false);
        SuccessPanel.gameObject.SetActive(true);
        SuccessPanel.DOAnchorPos(Vector2.zero,0.2f).SetEase(Ease.InOutCubic).OnComplete(()=>{
            StartCoroutine(ItemsAnimation(successElements));
            StartCoroutine(ItemsFillAnimation(stars));
        });
    }

    private void OnOpenFail()
    {
        Light.gameObject.SetActive(false);
        gameData.isGameStart=false;
        //Here Goes Some Improvements
        if(gameData.isTextLevel)
            directionText.SetActive(false);

        if(gameData.isLightLevel)
        {
            DOTween.Kill(Light);
        }
        
        SceneUI(false);
        FailPanel.gameObject.SetActive(true);
        FailPanel.DOAnchorPos(Vector2.zero,0.2f).SetEase(Ease.InOutCubic).OnComplete(()=>{
            StartCoroutine(ItemsAnimation(failElements));
        });
    }
  

    private IEnumerator Blink(GameObject gameObject,Image image)
    {
        
        gameObject.SetActive(true);
        image.color=new Color(0,0,0,1);
        image.DOFade(0,2f);
        yield return waitForSeconds1;
        gameObject.SetActive(false);
    }

    private IEnumerator ScoreMove()
    {
        yield return waitForSecondsScore;
        ScoreImage.DOAnchorPosX(ScoreX,0.5f);
    }

    private IEnumerator ItemsAnimation(List<Transform> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i].transform.localScale=Vector3.zero;
        }

        for (int i = 0; i < list.Count; i++)
        {
            list[i].transform.DOScale(1f,1f).SetEase(ease);
            yield return waitForSeconds2;
        }
    }

    private IEnumerator ItemsFillAnimation(List<Image> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i].fillAmount=0;
        }

        for (int i = 0; i < list.Count; i++)
        {
            yield return waitForFill;
            list[i].DOFillAmount(1,.5f);
        }
    }
  

    public void OpenStorePanel()
    {
        //EventManager.Broadcast(GameEvent.OnButtonClicked);
        StartPanel.DOAnchorPos(new Vector2(StartX,StartY),duration).OnComplete(()=>StartPanel.gameObject.SetActive(false));
        StorePanel.gameObject.SetActive(true);
        StorePanel.DOAnchorPos(new Vector2(0,750),duration);
        EventManager.Broadcast(GameEvent.OnShopOpen);
    }

    public void BackToStart()
    {

        StartPanel.gameObject.SetActive(true);
        StartPanel.DOAnchorPos(Vector2.zero,duration);
        StorePanel.DOAnchorPos(new Vector2(StoreX,StoreY),duration);
        //.OnComplete(()=>WeaponPanel.gameObject.SetActive(false));
        EventManager.Broadcast(GameEvent.OnShopClose);

        //EventManager.Broadcast(GameEvent.OnButtonClicked);

    }
}
   
