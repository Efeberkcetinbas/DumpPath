using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{

    [SerializeField] private ParticleSystem upParticle;
    [SerializeField] private ParticleSystem downParticle;
    [SerializeField] private ParticleSystem leftParticle;
    [SerializeField] private ParticleSystem rightParticle;

    [SerializeField] private PlayerData playerData;

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnPlayerUp,OnPlayerUp);
        EventManager.AddHandler(GameEvent.OnPlayerDown,OnPlayerDown);
        EventManager.AddHandler(GameEvent.OnPlayerLeft,OnPlayerLeft);
        EventManager.AddHandler(GameEvent.OnPlayerRight,OnPlayerRight);
        EventManager.AddHandler(GameEvent.OnCharacterChange,OnCharacterChange);
        
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnPlayerUp,OnPlayerUp);
        EventManager.RemoveHandler(GameEvent.OnPlayerDown,OnPlayerDown);
        EventManager.RemoveHandler(GameEvent.OnPlayerLeft,OnPlayerLeft);
        EventManager.RemoveHandler(GameEvent.OnPlayerRight,OnPlayerRight);
        EventManager.RemoveHandler(GameEvent.OnCharacterChange,OnCharacterChange);
    }


    private void OnPlayerLeft()
    {
        leftParticle.Play();
    }

    private void OnPlayerRight()
    {
        rightParticle.Play();
    }

    private void OnPlayerUp()
    {
        upParticle.Play();
    }

    private void OnPlayerDown()
    {
        downParticle.Play();
    }

    private void OnCharacterChange()
    {
        Debug.Log("CHARACTER: " + playerData.selectedIndex);
    }

    
}
