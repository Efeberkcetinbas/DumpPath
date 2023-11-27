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
    [SerializeField] private List<Color> fogColors=new List<Color>();

    private int skyboxIndex;
    private int backgroundIndex;
    private int fogColorIndex;

    private void Start() 
    {
        Check();
    }

    private void Check()
    {
        backgroundGameObjects[backgroundIndex].SetActive(true);
        RenderSettings.skybox=skyboxMaterials[skyboxIndex];
        RenderSettings.fogColor=fogColors[fogColorIndex];
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

    //Environment Generator ile de Objelerin Yerlerini Random bir sekilde dagitirsin. Renk Atamasini Random Yapabilirsin

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
        fogColorIndex++;
        skyboxIndex=skyboxIndex % skyboxMaterials.Count;
        fogColorIndex=fogColorIndex % fogColors.Count;
        RenderSettings.skybox=skyboxMaterials[skyboxIndex];
        RenderSettings.fogColor=fogColors[fogColorIndex];
    }
}
