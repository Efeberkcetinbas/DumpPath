using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class PanelManager : MonoBehaviour
{
    [SerializeField] private RectTransform StartPanel,DirectionPanel,StorePanel,SuccessPanel,FailPanel;

    [SerializeField] private GameObject[] sceneUI;
    [SerializeField] private GameObject directionText;
    [SerializeField] private Image Fade;

    [SerializeField] private float StartX,StartY,DirectionX,DirectionY,StoreX,StoreY,SuccessX,SuccessY,FailX,FailY,duration;

    [SerializeField] private GameData gameData;
    [Header("Success List")]
    [SerializeField] private Ease ease;
    [SerializeField] private List<Transform> successElements=new List<Transform>();

    //Waitforseconds
    private WaitForSeconds waitForSeconds1;
    private WaitForSeconds waitForSeconds2;


    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.AddHandler(GameEvent.OnSuccess,OnSuccess);
        EventManager.AddHandler(GameEvent.OnOpenSuccess,OnOpenSuccess);
        EventManager.AddHandler(GameEvent.OnDisableLetter,OnDisableLetter);
    }


    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.RemoveHandler(GameEvent.OnSuccess,OnSuccess);
        EventManager.RemoveHandler(GameEvent.OnOpenSuccess,OnOpenSuccess);
        EventManager.RemoveHandler(GameEvent.OnDisableLetter,OnDisableLetter);
    }

    private void Start() 
    {
        //UICanvas.SetActive(false);
        SceneUI(false);

        waitForSeconds1=new WaitForSeconds(2);
        waitForSeconds2=new WaitForSeconds(.5f);
    }

    private void OnSuccess()
    {
        DirectionPanel.DOAnchorPos(new Vector2(0,500),duration);
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
            gameData.isGameEnd=false;
            //StartPanel.gameObject.SetActive(false);
        });
        DirectionPanel.gameObject.SetActive(true);
        DirectionPanel.DOAnchorPos(new Vector2(0,-500),duration);

    
        if(gameData.isTextLevel)
            directionText.SetActive(true);
    }

    

    private void OnRestartLevel()
    {
        OnNextLevel();
    }

    private void OnNextLevel()
    {
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

    private void SceneUI(bool val)
    {
        for (int i = 0; i < sceneUI.Length; i++)
        {
            sceneUI[i].SetActive(val);
        }
    }

    private void OnOpenSuccess()
    {
        //Here Goes Some Improvements
        if(gameData.isTextLevel)
            directionText.SetActive(false);
        
        SceneUI(false);
        SuccessPanel.gameObject.SetActive(true);
        SuccessPanel.DOAnchorPos(Vector2.zero,0.2f).SetEase(Ease.InOutCubic).OnComplete(()=>{
            StartCoroutine(ItemsAnimation());
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

    private IEnumerator ItemsAnimation()
    {
        for (int i = 0; i < successElements.Count; i++)
        {
            successElements[i].transform.localScale=Vector3.zero;
        }

        for (int i = 0; i < successElements.Count; i++)
        {
            successElements[i].transform.DOScale(1f,1f).SetEase(ease);
            yield return waitForSeconds2;
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
   
