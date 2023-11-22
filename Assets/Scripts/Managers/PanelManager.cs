using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class PanelManager : MonoBehaviour
{
    [SerializeField] private RectTransform StartPanel,BuffPanel,BallsPanel;


    [SerializeField] private Image Fade;

    [SerializeField] private float StartX,StartY,BuffX,BuffY,BallX,BallY,duration;

    [SerializeField] private GameData gameData;


    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
    }


    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
    }

    private void Start() 
    {
        //UICanvas.SetActive(false);
    }

    
    
    
    public void StartGame() 
    {
        //Gecikmeli olsun isGameEndFalse
        StartPanel.transform.DOScale(Vector3.zero,0.5f).OnComplete(()=>{
            gameData.isGameEnd=false;
            StartPanel.gameObject.SetActive(false);
        });
        
            //UICanvas.SetActive(true);
        //EventManager.Broadcast(GameEvent.OnGameStart);
    }

    

    private void OnRestartLevel()
    {
        OnNextLevel();
    }

    private void OnNextLevel()
    {

        StartPanel.gameObject.SetActive(true);
        StartPanel.transform.localScale=Vector3.one;
        StartPanel.DOAnchorPos(Vector2.zero,0.1f);
        StartCoroutine(Blink(Fade.gameObject,Fade));
    }


  

    private IEnumerator Blink(GameObject gameObject,Image image)
    {
        
        gameObject.SetActive(true);
        image.color=new Color(0,0,0,1);
        image.DOFade(0,2f);
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);

    }


    public void OpenBuffsPanel()
    {
        //EventManager.Broadcast(GameEvent.OnButtonClicked);
        StartPanel.DOAnchorPos(new Vector2(StartX,StartY),duration).OnComplete(()=>StartPanel.gameObject.SetActive(false));
        BuffPanel.gameObject.SetActive(true);
        BuffPanel.DOAnchorPos(Vector2.zero,duration);
    }

    public void OpenBallsPanel()
    {
        //EventManager.Broadcast(GameEvent.OnButtonClicked);
        StartPanel.DOAnchorPos(new Vector2(StartX,StartY),duration).OnComplete(()=>StartPanel.gameObject.SetActive(false));
        BallsPanel.gameObject.SetActive(true);
        BallsPanel.DOAnchorPos(Vector2.zero,duration);
        EventManager.Broadcast(GameEvent.OnShopOpen);
    }

    public void BackToStart(bool isOnCharacter)
    {

        if(isOnCharacter)
        {
            StartPanel.gameObject.SetActive(true);
            StartPanel.DOAnchorPos(Vector2.zero,duration);
            BuffPanel.DOAnchorPos(new Vector2(BuffX,BuffY),duration);
            //.OnComplete(()=>CharacterPanel.gameObject.SetActive(false));
        }
        else
        {
            StartPanel.gameObject.SetActive(true);
            StartPanel.DOAnchorPos(Vector2.zero,duration);
            BallsPanel.DOAnchorPos(new Vector2(BallX,BallY),duration);
            //.OnComplete(()=>WeaponPanel.gameObject.SetActive(false));
        }

        EventManager.Broadcast(GameEvent.OnShopClose);

        //EventManager.Broadcast(GameEvent.OnButtonClicked);

    }
}
   
