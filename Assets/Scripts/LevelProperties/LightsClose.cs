using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsClose : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private int _lightTime;
    private void OnEnable() 
    {
        StartCoroutine(SetTrue());
    }

    private IEnumerator SetTrue()
    {
        yield return null;
        gameData.isLightLevel=true;
        gameData.lightTime=_lightTime;
    }

    


}
