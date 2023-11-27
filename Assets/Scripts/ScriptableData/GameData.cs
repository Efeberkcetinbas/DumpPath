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

    public bool isGameEnd=false;
    public bool isTextLevel=false;

    //Directions
    public int ReqUp;
    public int ReqDown;
    public int ReqLeft;
    public int ReqRight;

    //Temp
    public int tempUp;
    public int tempDown;
    public int tempRight;
    public int tempLeft;

    //Text
    public string LetterText;
}
