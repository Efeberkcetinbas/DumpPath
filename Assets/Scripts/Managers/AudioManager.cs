using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip GameLoop,BuffMusic;
    public AudioClip HitSound1,HitSound2,GameOverSound;

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
        EventManager.AddHandler(GameEvent.OnTargetHit,OnHit);
        EventManager.AddHandler(GameEvent.OnGameOver,OnGameOver);
    }
    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnTargetHit,OnHit);
        EventManager.RemoveHandler(GameEvent.OnGameOver,OnGameOver);
    }

    void OnHit()
    {
        hit=!hit;
        if(hit)
            effectSource.PlayOneShot(HitSound1);
        else
            effectSource.PlayOneShot(HitSound2);
    }

    void OnGameOver()
    {
        effectSource.PlayOneShot(GameOverSound);
    }

}
