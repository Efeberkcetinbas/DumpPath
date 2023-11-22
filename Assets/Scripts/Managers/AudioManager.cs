using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip GameLoop,BuffMusic;
    public AudioClip GameOverSound,BridgeSound,CageSound;

    AudioSource musicSource,effectSource;

    private bool hit;

    private void Start() 
    {
        musicSource = GetComponent<AudioSource>();
        musicSource.clip = GameLoop;
        //musicSource.Play();
        effectSource = gameObject.AddComponent<AudioSource>();
        effectSource.volume=0.4f;
    }

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnGameOver,OnGameOver);
        EventManager.AddHandler(GameEvent.OnBridgeOpen,OnBridgeOpen);
        EventManager.AddHandler(GameEvent.OnCageOpen,OnCageOpen);
    }
    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnGameOver,OnGameOver);
        EventManager.RemoveHandler(GameEvent.OnBridgeOpen,OnBridgeOpen);
        EventManager.RemoveHandler(GameEvent.OnCageOpen,OnCageOpen);
    }

    

    void OnGameOver()
    {
        effectSource.PlayOneShot(GameOverSound);
    }

    private void OnBridgeOpen()
    {
        effectSource.PlayOneShot(BridgeSound);
    }

    private void OnCageOpen()
    {
        effectSource.PlayOneShot(CageSound);
    }

}
