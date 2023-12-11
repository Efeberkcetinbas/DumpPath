using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextLevelRequirement : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private string textLetter;

    private void OnEnable() 
    {
        StartCoroutine(SetTrue());
    }

    

    private IEnumerator SetTrue()
    {
        yield return null;
        gameData.isTextLevel=true;
        gameData.LetterText=textLetter;
        EventManager.Broadcast(GameEvent.OnUpdateLetterDirectionText);
    }
}
