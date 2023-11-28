using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameData", menuName = "Data/GameData", order = 0)]
public class GameData : ScriptableObject 
{

    public int score;
    public int increaseScore;
    public int LevelNumberIndex;
    public int ProgressNumber;
    public int levelProgressNumber;
    public int undoPrice;

    public bool isGameEnd=false;
    public bool isTextLevel=false;
    public bool isUndo=false;

    //Directions
    public float ReqUp;
    public float ReqDown;
    public float ReqLeft;
    public float ReqRight;

    //Temp
    public float tempUp;
    public float tempDown;
    public float tempRight;
    public float tempLeft;
    public float totalReq;

    //Text
    public string LetterText;
}
