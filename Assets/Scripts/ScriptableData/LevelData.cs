using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[CreateAssetMenu(fileName = "LevelData", menuName = "Data/LevelData", order = 4)]
public class LevelData : ScriptableObject 
{

    public int IndexOfLevel;
    public int score;
    
    public int skyboxIndex;

    //private string filePath;

    

    public void SaveData()
    {
        PlayerPrefs.SetInt("IndexOfLevel",IndexOfLevel);
        PlayerPrefs.SetInt("Score",score);
        PlayerPrefs.SetInt("SkyboxIndex",skyboxIndex);

        /*filePath = Application.persistentDataPath + "/levelData.json";
        string jsonData = JsonUtility.ToJson(this);
        File.WriteAllText(filePath, jsonData);*/
    }

    public void LoadData()
    {
        /*filePath = Application.persistentDataPath + "/levelData.json";

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            JsonUtility.FromJsonOverwrite(jsonData, this);
        }*/

        IndexOfLevel=PlayerPrefs.GetInt("IndexOfLevel");
        score=PlayerPrefs.GetInt("Score");
        skyboxIndex=PlayerPrefs.GetInt("SkyboxIndex");
    }

    /*public void DeleteJsonData()
    {
        // Check if the file exists before attempting to delete it
        if (File.Exists(filePath))
        {
            // Delete the file
            File.Delete(filePath);
            Debug.Log("JSON data deleted successfully.");
        }
        else
        {
            Debug.Log("No JSON data file found to delete.");
        }
    }*/
}
