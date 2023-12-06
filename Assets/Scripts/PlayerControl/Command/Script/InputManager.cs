using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public enum MovementType
{
    Roll,
    Dash,
    Jump,
}
public class InputManager : MonoBehaviour
{
    
    [SerializeField] private CharacterMove character;
    [SerializeField] private UICommandList uiCommandList;
    private Vector3 firstPosition;
    private Vector3 lastPosition;

    private float dragDistance;

    public PlayerData playerData;
    public GameData gameData;

    //Touch Y Axis
    [SerializeField] private float screenPercentageToExclude = 20f;

    [SerializeField] private MovementType movementType;
    

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

        //Denemelik
        OnMovementTypeChange();
        
    }

    private void OnMovementTypeChange()
    {
        movementType=playerData.movementType;
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
                                switch(movementType)
                                {
                                    case MovementType.Roll:
                                        StartCoroutine(Roll(Vector3.right));
                                        break;
                                    case MovementType.Dash:
                                        GoXAxis(Vector3.right,1);
                                        break;
                                    case MovementType.Jump:
                                        RotateYAxis(90);
                                        JumpXAxis(Vector3.right,-360,0.5f,+1);
                                        break;
                                }
                                
                                //StartCoroutine(Roll(Vector3.right));
                                playerData.RightMove++;
                                gameData.ReqRight--;
                                SendMoveCommand(Vector3.right);
                                //GoXAxis(+1);
                                EventManager.Broadcast(GameEvent.OnPlayerRight);
                                EventManager.Broadcast(GameEvent.OnPlayerMove);
                            }

                            else
                            {
                                EventManager.Broadcast(GameEvent.OnFalseMove);

                            }
                        }
                        else
                        {
                            if(gameData.ReqLeft>0)
                            {
                                switch(movementType)
                                {
                                    case MovementType.Roll:
                                        StartCoroutine(Roll(Vector3.left));
                                        break;
                                    case MovementType.Dash:
                                        GoXAxis(Vector3.left,-1);
                                        break;
                                    case MovementType.Jump:
                                        RotateYAxis(-90);
                                        JumpXAxis(Vector3.left,360,0.5f,-1);
                                        break;
                                }
                                //JumpXAxis(-1f,360,0.5f);
                                //StartCoroutine(Roll(Vector3.left));
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
                                EventManager.Broadcast(GameEvent.OnFalseMove);

                            }
                            
                        }
                    }

                    else
                    {
                        if(lastPosition.y>firstPosition.y)
                        {
                            if(gameData.ReqUp>0)
                            {
                                switch(movementType)
                                {
                                    case MovementType.Roll:
                                        StartCoroutine(Roll(Vector3.forward));
                                        break;
                                    case MovementType.Dash:
                                        GoZAxis(Vector3.forward,1);
                                        break;
                                    case MovementType.Jump:
                                        RotateYAxis(0);
                                        JumpZAxis(Vector3.forward,360,0.5f,+1);
                                        break;
                                }
                                //JumpZAxis(+1f,360,0.5f);
                                //StartCoroutine(Roll(Vector3.forward));
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
                                EventManager.Broadcast(GameEvent.OnFalseMove);

                            }
                            
                        }
                        else
                        {
                            if(gameData.ReqDown>0)
                            {
                                switch(movementType)
                                {
                                    case MovementType.Roll:
                                        StartCoroutine(Roll(Vector3.back));
                                        break;
                                    case MovementType.Dash:
                                        GoZAxis(Vector3.back,-1);
                                        break;
                                    case MovementType.Jump:
                                        RotateYAxis(180);
                                        JumpZAxis(Vector3.back,-360,0.5f,-1);
                                        break;
                                }
                                //JumpZAxis(-1f,-360,0.5f);
                                //StartCoroutine(Roll(Vector3.back));
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
                                EventManager.Broadcast(GameEvent.OnFalseMove);

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
        playerData.playerCanMove=false;


        while(remainingAngle>0)
        {
            float rotationAngle=Mathf.Min(Time.deltaTime*300,remainingAngle);
            character.transform.RotateAround(rotationCenter,rotationAxis,rotationAngle);
            remainingAngle-=rotationAngle;
            playerData.playerCanMove=true;
            yield return null;
        }
    }
    #endregion

    

    

    #region Move
    private void GoXAxis(Vector3 direction, float amount)
    {
        var currentPos=character.transform.position;
        Debug.Log("CURRENT POS " + currentPos);
        playerData.playerCanMove=false;
        Debug.Log("DIRECTION X " + direction.x);
        character.transform.DOMove(new Vector3(currentPos.x+direction.x + amount,currentPos.y,currentPos.z),0.2f).OnComplete(()=>{
            playerData.playerCanMove=true;
        });
    }

    private void GoZAxis(Vector3 direction, float amount)
    {
        var currentPos=character.transform.position;
        playerData.playerCanMove=false;
        character.transform.DOMove(new Vector3(currentPos.x,currentPos.y,currentPos.z+direction.z + amount),0.2f).OnComplete(()=>{
            playerData.playerCanMove=true;
        });
    }

    #endregion

    #region Jump
    private void JumpXAxis(Vector3 direction,float rot,float duration, float amount)
    {
        var currentPos=character.transform.position;
        //rotasyon istersen acarsin
        //transform.DORotate(new Vector3(0,0,rot),1f, RotateMode.FastBeyond360);
        //character.transform.DOScale(Vector3.one/1.5f,duration);
        playerData.playerCanMove=false;
        character.transform.DOJump(new Vector3(currentPos.x+direction.x + amount,currentPos.y,currentPos.z),1,1,duration).OnComplete(()=>{
            //character.transform.DOScale(Vector3.one,0.25f);
            playerData.playerCanMove=true;
        });
    }

    private void JumpZAxis(Vector3 direction,float rot,float duration, float amount)
    {
        var currentPos=character.transform.position;
        //transform.DORotate(new Vector3(rot,0,0),1f, RotateMode.FastBeyond360);
        //character.transform.DOScale(Vector3.one/1.5f,duration);
        playerData.playerCanMove=false;
        character.transform.DOJump(new Vector3(currentPos.x,currentPos.y,currentPos.z + direction.z + amount),1,1,duration).OnComplete(()=>{
            //character.transform.DOScale(Vector3.one,0.25f);
            playerData.playerCanMove=true;
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
