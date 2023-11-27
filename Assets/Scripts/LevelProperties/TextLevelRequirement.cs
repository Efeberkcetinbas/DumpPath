using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextLevelRequirement : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private string textLetter;

    private void OnEnable() 
    {
        gameData.isTextLevel=true;
        gameData.LetterText=textLetter;
        EventManager.Broadcast(GameEvent.OnUpdateLetterDirectionText);
    }

    private void OnDisable() 
    {
        gameData.isTextLevel=false;
        EventManager.Broadcast(GameEvent.OnDisableLetter);
    }
}
