using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpikeMove : MonoBehaviour,ITrapMove
{
    [SerializeField] private Transform spike;
    [SerializeField] private float y,duration;

    private void Start() 
    {
        Move();
    }

    public void Move()
    {
        spike.DOLocalMoveY(y,duration).SetLoops(-1,LoopType.Yoyo);
    }
    
}
