using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class LevelEnvironmentGenerator : MonoBehaviour
{
    [SerializeField] private GameData gameData;

    //Dotween ile guzel bir background olussun
    [SerializeField] private List<GameObject> backgroundGameObjects=new List<GameObject>();
    [SerializeField] private List<Material> skyboxMaterials=new List<Material>();

    private int skyboxIndex;
    private int backgroundIndex;

    private void Start() 
    {
        backgroundGameObjects[backgroundIndex].SetActive(true);
        RenderSettings.skybox=skyboxMaterials[skyboxIndex];
        
    }
    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
        
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
        
    }

    private void OnNextLevel()
    {
        if(gameData.LevelNumberIndex % 5 == 0)
        {
            Generate();
            ChangeSkybox();
        }
    }

    private void Generate()
    {
        if (backgroundGameObjects.Count == 0)
        {
            throw new InvalidExpressionException("No background game objects available.");
        }
        else
        {
            backgroundIndex++;
            backgroundIndex = backgroundIndex % backgroundGameObjects.Count;

            for (int i = 0; i < backgroundGameObjects.Count; i++)
            {
                backgroundGameObjects[i].SetActive(false);
            }

            backgroundGameObjects[backgroundIndex].SetActive(true);

            
}
    }

    private void ChangeSkybox()
    {
        skyboxIndex++;
        skyboxIndex=skyboxIndex % skyboxMaterials.Count;
        RenderSettings.skybox=skyboxMaterials[skyboxIndex];
    }
}
