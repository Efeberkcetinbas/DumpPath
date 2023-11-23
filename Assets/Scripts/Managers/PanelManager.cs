using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class PanelManager : MonoBehaviour
{
    [SerializeField] private RectTransform StartPanel,DirectionPanel,StorePanel,SuccessPanel,FailPanel;

    [SerializeField] private GameObject[] sceneUI;
    [SerializeField] private Image Fade;

    [SerializeField] private float StartX,StartY,DirectionX,DirectionY,StoreX,StoreY,SuccessX,SuccessY,FailX,FailY,duration;

    [SerializeField] private GameData gameData;


    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.AddHandler(GameEvent.OnSuccess,OnSuccess);
        EventManager.AddHandler(GameEvent.OnOpenSuccess,OnOpenSuccess);
    }


    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.RemoveHandler(GameEvent.OnSuccess,OnSuccess);
        EventManager.RemoveHandler(GameEvent.OnOpenSuccess,OnOpenSuccess);
    }

    private void Start() 
    {
        //UICanvas.SetActive(false);
        for (int i = 0; i < sceneUI.Length; i++)
        {
            sceneUI[i].SetActive(false);
        }
    }

    private void OnSuccess()
    {
        DirectionPanel.DOAnchorPos(new Vector2(0,500),duration);
    }

    
    
    
    public void StartGame() 
    {
        //EventManager.Broadcast(GameEvent.OnButtonClicked);
        StartPanel.DOAnchorPos(new Vector2(StartX,StartY),duration).OnComplete(()=>{
            for (int i = 0; i < sceneUI.Length; i++)
            {
                sceneUI[i].SetActive(true);
            }
            gameData.isGameEnd=false;
            StartPanel.gameObject.SetActive(false);
        });
        DirectionPanel.gameObject.SetActive(true);
        DirectionPanel.DOAnchorPos(new Vector2(0,-500),duration);
    }

    

    private void OnRestartLevel()
    {
        OnNextLevel();
    }

    private void OnNextLevel()
    {
        SuccessPanel.DOAnchorPos(new Vector2(2500,0),0.1f).OnComplete(()=>SuccessPanel.gameObject.SetActive(false));
        StartPanel.gameObject.SetActive(true);
        StartPanel.transform.localScale=Vector3.one;
        StartPanel.DOAnchorPos(Vector2.zero,0.1f).OnComplete(()=>EventManager.Broadcast(GameEvent.OnIncreaseScore));

        StartCoroutine(Blink(Fade.gameObject,Fade));
        for (int i = 0; i < sceneUI.Length; i++)
        {
            sceneUI[i].SetActive(false);
        }

    }

    private void OnOpenSuccess()
    {
        SuccessPanel.gameObject.SetActive(true);
        SuccessPanel.DOAnchorPos(Vector2.zero,0.2f).SetEase(Ease.InOutCubic);
    }
  

    private IEnumerator Blink(GameObject gameObject,Image image)
    {
        
        gameObject.SetActive(true);
        image.color=new Color(0,0,0,1);
        image.DOFade(0,2f);
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);

    }

  

    public void OpenStorePanel()
    {
        //EventManager.Broadcast(GameEvent.OnButtonClicked);
        StartPanel.DOAnchorPos(new Vector2(StartX,StartY),duration).OnComplete(()=>StartPanel.gameObject.SetActive(false));
        StorePanel.gameObject.SetActive(true);
        StorePanel.DOAnchorPos(new Vector2(0,-500),duration);
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
   
