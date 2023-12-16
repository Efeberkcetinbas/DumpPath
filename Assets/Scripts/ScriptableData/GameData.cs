using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


[CreateAssetMenu(fileName = "GameData", menuName = "Data/GameData", order = 0)]
public class GameData : ScriptableObject 
{

    public int increaseScore;
    public int ProgressNumber;
    public int levelProgressNumber;
    public int undoPrice;
    public int backgroundIndex;
    public int fogColorIndex;
    public int lightTime=5;

    public bool isGameEnd=true;
    public bool isGameStart;
    public bool isTextLevel=false;
    public bool isUndo=false;
    public bool isLightLevel=false;

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

    /*public void SaveData()
    {
        string filePath = Application.persistentDataPath + "/gameData.json";
        string jsonData = JsonUtility.ToJson(this);
        File.WriteAllText(filePath, jsonData);
    }

    public void LoadData()
    {
        string filePath = Application.persistentDataPath + "/gameData.json";

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            JsonUtility.FromJsonOverwrite(jsonData, this);
        }
    }*/
}
