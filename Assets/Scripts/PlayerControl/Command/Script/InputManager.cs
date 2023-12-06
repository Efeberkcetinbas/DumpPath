using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InputManager : MonoBehaviour
{
    
    [SerializeField] private CharacterMove character;
    
    [SerializeField] private UICommandList uiCommandList;

    private Vector3 firstPosition;
    private Vector3 lastPosition;

    private float dragDistance;

    public PlayerData playerData;
    public GameData gameData;

    [SerializeField] private Transform metalHelmet;
    [SerializeField] private float y,oldy;
    
    [SerializeField] private Button undo;
    //Touch Y Axis
    [SerializeField] private float screenPercentageToExclude = 20f;
    

    private void OnEnable()
    {
        /*up?.onClick.AddListener(() => SendMoveCommand(character.transform, Vector3.forward, 1f));
        down?.onClick.AddListener(() => SendMoveCommand(character.transform, Vector3.back, 1f));
        left?.onClick.AddListener(() => SendMoveCommand(character.transform, Vector3.left, 1f));
        right?.onClick.AddListener(() => SendMoveCommand(character.transform, Vector3.right, 1f));*/

        //undo?.onClick.AddListener(() => character.UndoCommand());
        //redo?.onClick.AddListener(() => character.RedoCommand());
    }

    private void Start() 
    {
        dragDistance=Screen.height*15/100;
        Debug.Log(Screen.height);
        
    }

    private void SendMoveCommand(Transform objectToMove, Vector3 direction, float distance)
    {
        ICommand movement = new Move(objectToMove, direction, distance);
        character.AddCommand(movement as Move);
        //uiCommandList?.AddCommand(movement);
    }

    public void UndoMove()
    {
        character.UndoCommand();
        //gameData.score-=gameData.undoPrice;
        EventManager.Broadcast(GameEvent.OnDecreaseScore);
    }

    private void Update()
    {
        CheckSwipeInput();
    }

    private void CheckSwipeInput()
    {
        if(Input.touchCount>0 && playerData.playerCanMove && !gameData.isGameEnd)
        {
            Touch touch=Input.GetTouch(0);
            if(touch.phase==TouchPhase.Began)
            {
                firstPosition=touch.position;
                lastPosition=touch.position;
            }

            else if(touch.phase==TouchPhase.Moved)
            {
                lastPosition=touch.position;
            }

            else if(touch.phase==TouchPhase.Ended)
            {
                lastPosition=touch.position;
                Vector2 swipeDirection=lastPosition-firstPosition;
                if(firstPosition.y < (Screen.height * (1 - screenPercentageToExclude / 100)))
                {
                    Debug.Log("SWIPE");
                    if(Mathf.Abs(lastPosition.x-firstPosition.x)>Mathf.Abs(lastPosition.y-firstPosition.y))
                    {
                        if(lastPosition.x>firstPosition.x)
                        {
                            if(gameData.ReqRight>0)
                            {
                                //RotateYAxis(90);
                                //Rotate
                                //JumpXAxis(+1f,-360,0.5f);
                                
                                StartCoroutine(Roll(Vector3.right));
                                playerData.RightMove++;
                                gameData.ReqRight--;
                                SendMoveCommand(Vector3.right);
                                //GoXAxis(+1);
                                EventManager.Broadcast(GameEvent.OnPlayerRight);
                                EventManager.Broadcast(GameEvent.OnPlayerMove);
                            }

                            else
                            {
                                Debug.Log("RESTART");

                            }
                        }
                        else
                        {
                            if(gameData.ReqLeft>0)
                            {
                                //JumpXAxis(-1f,360,0.5f);
                                StartCoroutine(Roll(Vector3.left));
                                playerData.LeftMove++;
                                gameData.ReqLeft--;
                                SendMoveCommand(Vector3.left);
                                //GoXAxis(-1);
                                //RotateYAxis(-90);
                                EventManager.Broadcast(GameEvent.OnPlayerLeft);
                                EventManager.Broadcast(GameEvent.OnPlayerMove);
                            }

                            else
                            {
                                Debug.Log("RESTART");

                            }
                            
                        }
                    }

                    else
                    {
                        if(lastPosition.y>firstPosition.y)
                        {
                            if(gameData.ReqUp>0)
                            {
                                //JumpZAxis(+1f,360,0.5f);
                                StartCoroutine(Roll(Vector3.forward));
                                playerData.UpMove++;
                                gameData.ReqUp--;
                                SendMoveCommand(Vector3.forward);
                                //GoZAxis(+1);
                                //RotateYAxis(0);
                                EventManager.Broadcast(GameEvent.OnPlayerUp);
                                EventManager.Broadcast(GameEvent.OnPlayerMove);
                            }

                            else
                            {
                                Debug.Log("RESTART");

                            }
                            
                        }
                        else
                        {
                            if(gameData.ReqDown>0)
                            {
                                //JumpZAxis(-1f,-360,0.5f);
                                StartCoroutine(Roll(Vector3.back));
                                playerData.DownMove++;
                                gameData.ReqDown--;
                                SendMoveCommand(Vector3.back);
                                //GoZAxis(-1);
                                //RotateYAxis(180);
                                EventManager.Broadcast(GameEvent.OnPlayerDown);
                                EventManager.Broadcast(GameEvent.OnPlayerMove);
                            }

                            else
                            {
                                Debug.Log("RESTART");

                            }
                            

                        }
                    }
                }


            }
        }
    }

    
    private void SendMoveCommand(Vector3 direction)
    {
        ICommand movement = new Move(character.transform, direction, 1f);
        character.AddCommand(movement as Move);
        //uiCommandList?.AddCommand(movement);
    }

    #region MoveTypeRegion

    #region Rolling

    private IEnumerator Roll(Vector3 direction)
    {   
        gameData.isUndo=false;
        float remainingAngle=90;
        Vector3 rotationCenter=character.transform.position+direction/2 + Vector3.down/2;
        Vector3 rotationAxis=Vector3.Cross(Vector3.up,direction);

        while(remainingAngle>0)
        {
            float rotationAngle=Mathf.Min(Time.deltaTime*300,remainingAngle);
            character.transform.RotateAround(rotationCenter,rotationAxis,rotationAngle);
            remainingAngle-=rotationAngle;
            yield return null;
        }
    }
    #endregion

    

    private IEnumerator JumpToFalse()
    {
        yield return new WaitForSeconds(.1f);
        /*if(!gameManager.isGameEnd)
            gameManager.canPlayerJump=true;*/
    }

    #region Move
    private void GoXAxis(float direction)
    {
        var currentPosLeft=transform.position.x;
        character.transform.DOMoveX(currentPosLeft+direction,0.25f).OnComplete(()=>{
            StartCoroutine(JumpToFalse());
        });
    }

    private void GoZAxis(float direction)
    {
        var currentPosUp=transform.position.z;
        character.transform.DOMoveZ(currentPosUp+direction,0.25f).OnComplete(()=>{
            StartCoroutine(JumpToFalse());
        });
    }

    #endregion

    #region Jump
    private void JumpXAxis(float direction,float rot,float duration)
    {
        var currentPos=character.transform.position;
        //rotasyon istersen acarsin
        //transform.DORotate(new Vector3(0,0,rot),1f, RotateMode.FastBeyond360);
        character.transform.DOScale(Vector3.one/1.5f,duration);
        metalHelmet.DOLocalMoveY(y,duration/2).OnComplete(()=>metalHelmet.DOLocalMoveY(oldy,duration/2));
        metalHelmet.DORotate(new Vector3(0,360,0),1f, RotateMode.FastBeyond360);
        character.transform.DOJump(new Vector3(currentPos.x+direction,currentPos.y,currentPos.z),1,1,duration).OnComplete(()=>{
            character.transform.DOScale(Vector3.one,0.25f);
            StartCoroutine(JumpToFalse());
        });
    }

    private void JumpZAxis(float direction,float rot,float duration)
    {
        var currentPos=character.transform.position;
        //transform.DORotate(new Vector3(rot,0,0),1f, RotateMode.FastBeyond360);
        character.transform.DOScale(Vector3.one/1.5f,duration);
        metalHelmet.DOLocalMoveY(y,duration/2).OnComplete(()=>metalHelmet.DOLocalMoveY(oldy,duration/2));
        metalHelmet.DORotate(new Vector3(0,360,0),1f, RotateMode.FastBeyond360);
        character.transform.DOJump(new Vector3(currentPos.x,currentPos.y,currentPos.z + direction),1,1,duration).OnComplete(()=>{
            character.transform.DOScale(Vector3.one,0.25f);
            StartCoroutine(JumpToFalse());
        });
    }
    #endregion

    #region  Rotate
    private void RotateYAxis(float y)
    {
        transform.DORotate(new Vector3(0,y,0),0.25f);
    }

    #endregion


    #endregion

    



}
