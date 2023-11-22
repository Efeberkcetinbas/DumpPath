using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerMovement : MonoBehaviour
{
    private Vector3 firstPosition;
    private Vector3 lastPosition;

    private float dragDistance;

    public PlayerData playerData;
    public GameData gameData;


    [SerializeField] private Transform metalHelmet;
    [SerializeField] private float y,oldy;



    private void Start() 
    {
        dragDistance=Screen.height*15/100;
    }




    

    void Update()
    {
        CheckMove();

    }

    private void CheckMove()
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
                            //GoXAxis(+1);
                            EventManager.Broadcast(GameEvent.OnPlayerRight);
                            EventManager.Broadcast(GameEvent.OnPlayerMove);
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
                            //GoXAxis(-1);
                            //RotateYAxis(-90);
                            EventManager.Broadcast(GameEvent.OnPlayerLeft);
                            EventManager.Broadcast(GameEvent.OnPlayerMove);
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
                            //GoZAxis(+1);
                            //RotateYAxis(0);
                            EventManager.Broadcast(GameEvent.OnPlayerUp);
                            EventManager.Broadcast(GameEvent.OnPlayerMove);
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
                            //GoZAxis(-1);
                            //RotateYAxis(180);
                            EventManager.Broadcast(GameEvent.OnPlayerDown);
                            EventManager.Broadcast(GameEvent.OnPlayerMove);
                        }
                        

                    }
                }

                //playerData.playerCanMove=false;

            }
        }
    }

    #region Rolling

    private IEnumerator Roll(Vector3 direction)
    {   
        float remainingAngle=90;
        Vector3 rotationCenter=transform.position+direction/2 + Vector3.down/2;
        Vector3 rotationAxis=Vector3.Cross(Vector3.up,direction);

        while(remainingAngle>0)
        {
            float rotationAngle=Mathf.Min(Time.deltaTime*300,remainingAngle);
            transform.RotateAround(rotationCenter,rotationAxis,rotationAngle);
            remainingAngle-=rotationAngle;
            yield return null;
        }
    }
    #endregion

    private void OnNextLevel()
    {
        playerData.UpMove=0;
        playerData.DownMove=0;
        playerData.LeftMove=0;
        playerData.RightMove=0;
    }

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
        transform.DOMoveX(currentPosLeft+direction,0.25f).OnComplete(()=>{
            StartCoroutine(JumpToFalse());
        });
    }

    private void GoZAxis(float direction)
    {
        var currentPosUp=transform.position.z;
        transform.DOMoveZ(currentPosUp+direction,0.25f).OnComplete(()=>{
            StartCoroutine(JumpToFalse());
        });
    }

    #endregion

    #region Jump
    private void JumpXAxis(float direction,float rot,float duration)
    {
        var currentPos=transform.position;
        //rotasyon istersen acarsin
        //transform.DORotate(new Vector3(0,0,rot),1f, RotateMode.FastBeyond360);
        transform.DOScale(Vector3.one/1.5f,duration);
        metalHelmet.DOLocalMoveY(y,duration/2).OnComplete(()=>metalHelmet.DOLocalMoveY(oldy,duration/2));
        metalHelmet.DORotate(new Vector3(0,360,0),1f, RotateMode.FastBeyond360);
        transform.DOJump(new Vector3(currentPos.x+direction,currentPos.y,currentPos.z),1,1,duration).OnComplete(()=>{
            transform.DOScale(Vector3.one,0.25f);
            StartCoroutine(JumpToFalse());
        });
    }

    private void JumpZAxis(float direction,float rot,float duration)
    {
        var currentPos=transform.position;
        //transform.DORotate(new Vector3(rot,0,0),1f, RotateMode.FastBeyond360);
        transform.DOScale(Vector3.one/1.5f,duration);
        metalHelmet.DOLocalMoveY(y,duration/2).OnComplete(()=>metalHelmet.DOLocalMoveY(oldy,duration/2));
        metalHelmet.DORotate(new Vector3(0,360,0),1f, RotateMode.FastBeyond360);
        transform.DOJump(new Vector3(currentPos.x,currentPos.y,currentPos.z + direction),1,1,duration).OnComplete(()=>{
            transform.DOScale(Vector3.one,0.25f);
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

}
