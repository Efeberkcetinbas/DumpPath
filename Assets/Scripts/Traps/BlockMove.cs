using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BlockMove : MonoBehaviour, ITrapMove
{
    [SerializeField] private bool isUpDown,isLeftRight,isJumper;

    [SerializeField] private float x,oldx,y,oldy,z,oldz,duration;
    [SerializeField] private Ease ease;

    [SerializeField] private Transform block;

    private void Start() 
    {
        Move();        
    }


    public void Move()
    {
        
        if(isLeftRight) block.DOLocalMoveX(x,duration).OnComplete(()=>block.DOLocalMoveX(oldx,duration)).SetLoops(-1,LoopType.Yoyo).SetEase(ease);
        if(isJumper) block.DOLocalMoveY(y,duration).OnComplete(()=>block.DOLocalMoveY(oldy,duration)).SetLoops(-1,LoopType.Yoyo).SetEase(ease);
        if(isUpDown) block.DOLocalMoveZ(z,duration).OnComplete(()=>block.DOLocalMoveZ(oldz,duration)).SetLoops(-1,LoopType.Yoyo).SetEase(ease);
    }

   
}
