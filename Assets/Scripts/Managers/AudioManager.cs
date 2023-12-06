using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip GameLoop,BuffMusic;
    public AudioClip GameOverSound,BridgeSound,CageSound,MoveSound,SuccessSound,SirenSound,BombSound,JumpSound;

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
        EventManager.AddHandler(GameEvent.OnBombActive,OnBombActive);
        EventManager.AddHandler(GameEvent.OnBombExplode,OnBombExplode);
        EventManager.AddHandler(GameEvent.OnCageOpen,OnCageOpen);
        EventManager.AddHandler(GameEvent.OnPlayerMove,OnPlayerMove);
        EventManager.AddHandler(GameEvent.OnSuccess,OnSuccess);
        EventManager.AddHandler(GameEvent.OnJump,OnJump);
    }
    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnGameOver,OnGameOver);
        EventManager.RemoveHandler(GameEvent.OnBridgeOpen,OnBridgeOpen);
        EventManager.RemoveHandler(GameEvent.OnBombActive,OnBombActive);
        EventManager.RemoveHandler(GameEvent.OnBombExplode,OnBombExplode);
        EventManager.RemoveHandler(GameEvent.OnCageOpen,OnCageOpen);
        EventManager.RemoveHandler(GameEvent.OnPlayerMove,OnPlayerMove);
        EventManager.RemoveHandler(GameEvent.OnSuccess,OnSuccess);
        EventManager.RemoveHandler(GameEvent.OnJump,OnJump);
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

    private void OnPlayerMove()
    {
        effectSource.PlayOneShot(MoveSound);
    }

    private void OnBombActive()
    {
        effectSource.PlayOneShot(SirenSound);
    }

    private void OnBombExplode()
    {
        effectSource.PlayOneShot(BombSound);
    }

    private void OnJump()
    {
        effectSource.PlayOneShot(JumpSound);
    }

    private void OnSuccess()
    {
        effectSource.PlayOneShot(SuccessSound);
    }
}
