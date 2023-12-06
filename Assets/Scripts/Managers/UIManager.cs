using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Text's")]
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI failScore;
    [SerializeField] private TextMeshProUGUI successScore;
    [SerializeField] private TextMeshProUGUI shoppingScore;
    [SerializeField] private TextMeshProUGUI priceText;


   
    [SerializeField] private TextMeshProUGUI levelText;
    
    [Header("Direciton Text's")]
    [SerializeField] private TextMeshProUGUI upText;
    [SerializeField] private TextMeshProUGUI downText;
    [SerializeField] private TextMeshProUGUI leftText;
    [SerializeField] private TextMeshProUGUI rightText;
    [SerializeField] private TextMeshProUGUI directionLetterText;

    [Header("Image's")]
    [SerializeField] private Image upProgressBar;
    [SerializeField] private Image downProgressBar;
    [SerializeField] private Image leftProgressBar;
    [SerializeField] private Image rightProgressBar;


    private WaitForSeconds waitForSeconds;



    
    
    [Header("Data's")]
    public GameData gameData;
    public PlayerData playerData;



    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnUIUpdate, OnUIUpdate);
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.AddHandler(GameEvent.OnUIRequirementUpdate,OnUIRequirementUpdate);
        EventManager.AddHandler(GameEvent.OnPlayerLeft,OnPlayerLeft);
        EventManager.AddHandler(GameEvent.OnPlayerRight,OnPlayerRight);
        EventManager.AddHandler(GameEvent.OnPlayerUp,OnPlayerUp);
        EventManager.AddHandler(GameEvent.OnPlayerDown,OnPlayerDown);
        EventManager.AddHandler(GameEvent.OnOpenSuccess,OnOpenSuccess);
        EventManager.AddHandler(GameEvent.OnDirectionUpdate,OnDirectionUpdate);
        EventManager.AddHandler(GameEvent.OnUpdateLetterDirectionText,OnUpdateLetterDirectionText);
        EventManager.AddHandler(GameEvent.OnShopCharacterSelected,OnShopBallSelected);
        EventManager.AddHandler(GameEvent.OnShopOpen,OnShopBallSelected);
        EventManager.AddHandler(GameEvent.OnFalseMove,OnFalseMove);
    }
    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnUIUpdate, OnUIUpdate);
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.RemoveHandler(GameEvent.OnUIRequirementUpdate,OnUIRequirementUpdate);
        EventManager.RemoveHandler(GameEvent.OnPlayerLeft,OnPlayerLeft);
        EventManager.RemoveHandler(GameEvent.OnPlayerRight,OnPlayerRight);
        EventManager.RemoveHandler(GameEvent.OnPlayerUp,OnPlayerUp);
        EventManager.RemoveHandler(GameEvent.OnPlayerDown,OnPlayerDown);
        EventManager.RemoveHandler(GameEvent.OnOpenSuccess,OnOpenSuccess);
        EventManager.RemoveHandler(GameEvent.OnDirectionUpdate,OnDirectionUpdate);
        EventManager.RemoveHandler(GameEvent.OnUpdateLetterDirectionText,OnUpdateLetterDirectionText);
        EventManager.RemoveHandler(GameEvent.OnShopCharacterSelected,OnShopBallSelected);
        EventManager.RemoveHandler(GameEvent.OnShopOpen,OnShopBallSelected);
        EventManager.RemoveHandler(GameEvent.OnFalseMove,OnFalseMove);
    }

    private void Start() 
    {
        waitForSeconds=new WaitForSeconds(1);
        OnNextLevel();
    }
    
    private void OnUIUpdate()
    {
        score.SetText(gameData.score.ToString());
        score.transform.DOScale(new Vector3(1.5f,1.5f,1.5f),0.2f).OnComplete(()=>score.transform.DOScale(new Vector3(1,1f,1f),0.2f));
    }

    private void OnNextLevel()
    {
        levelText.SetText("LEVEL " + (gameData.LevelNumberIndex+1).ToString());
        upProgressBar.fillAmount=0;
        downProgressBar.fillAmount=0;
        leftProgressBar.fillAmount=0;
        rightProgressBar.fillAmount=0;
    }

    private void OnUpdateLetterDirectionText()
    {
        directionLetterText.SetText(gameData.LetterText);
    }

    private void OnUIRequirementUpdate()
    {
        upText.SetText((gameData.ReqUp).ToString());
        downText.SetText((gameData.ReqDown).ToString());
        leftText.SetText((gameData.ReqLeft).ToString());
        rightText.SetText((gameData.ReqRight).ToString());
        priceText.SetText(gameData.undoPrice.ToString());
    }

    private void OnOpenSuccess()
    {
        successScore.SetText("+ " +  (gameData.score+gameData.increaseScore).ToString());
    }

    private void OnShopBallSelected()
    {
        shoppingScore.SetText(gameData.score.ToString());
    }


    #region Player Direction Events
    private void OnPlayerUp()
    {
        float val=(float)playerData.UpMove/gameData.tempUp;
        upProgressBar.DOFillAmount(val,0.25f);
        upText.SetText((gameData.ReqUp).ToString());
    }

    private void OnPlayerDown()
    {
        float val=(float)playerData.DownMove/gameData.tempDown;
        downProgressBar.DOFillAmount(val,0.25f);
        downText.SetText((gameData.ReqDown).ToString());
    }

    private void OnPlayerLeft()
    {
        float val=(float)playerData.LeftMove/gameData.tempLeft;
        leftProgressBar.DOFillAmount(val,0.25f);
        leftText.SetText((gameData.ReqLeft).ToString());
    }

    private void OnPlayerRight()
    {
        float val=(float)playerData.RightMove/gameData.tempRight;
        rightProgressBar.DOFillAmount(val,0.25f);
        rightText.SetText((gameData.ReqRight).ToString());
    }

    private void OnDirectionUpdate()
    {
        StartCoroutine(ResetProgressBar());
    }

    private void OnFalseMove()
    {
        StartCoroutine(ResetProgressBar());
    }

    private IEnumerator ResetProgressBar()
    {
        yield return waitForSeconds;
        upProgressBar.DOFillAmount(0,0.1f);
        downProgressBar.DOFillAmount(0,0.1f);
        leftProgressBar.DOFillAmount(0,0.1f);
        rightProgressBar.DOFillAmount(0,0.1f);
    }


    #endregion
   


    
}
