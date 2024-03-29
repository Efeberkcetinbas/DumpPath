using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip GameLoop,BuffMusic;
    public AudioClip GameOverSound,BridgeSound,CageSound,RollMoveSound,JumpMoveSound,DashMoveSound,SuccessSound1,SuccessSound2,SirenSound,BombSound,JumpSound
    ,CoinSound,NextLevelSound,UndoSound;

    AudioSource musicSource,effectSource;

    [SerializeField] private PlayerData playerData;

    [SerializeField] private GameObject soundOn,soundOff;

    private WaitForSeconds waitForSeconds;

    private bool isSoundOn=true;

    private void Start() 
    {
        musicSource = GetComponent<AudioSource>();
        musicSource.clip = GameLoop;
        musicSource.Play();
        effectSource = gameObject.AddComponent<AudioSource>();
        effectSource.volume=.75f;
        waitForSeconds=new WaitForSeconds(2);
        OnNextLevel();
    }

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnPlayerDead,OnPlayerDead);
        EventManager.AddHandler(GameEvent.OnBridgeOpen,OnBridgeOpen);
        EventManager.AddHandler(GameEvent.OnBombActive,OnBombActive);
        EventManager.AddHandler(GameEvent.OnBombExplode,OnBombExplode);
        EventManager.AddHandler(GameEvent.OnCageOpen,OnCageOpen);
        EventManager.AddHandler(GameEvent.OnPlayerMove,OnPlayerMove);
        EventManager.AddHandler(GameEvent.OnSuccess,OnSuccess);
        EventManager.AddHandler(GameEvent.OnJump,OnJump);
        EventManager.AddHandler(GameEvent.OnCollectCoin,OnCollectCoin);
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.AddHandler(GameEvent.OnUndoBegin,OnUndoBegin);
        EventManager.AddHandler(GameEvent.OnAudioOnOff,OnAudioOnOff);

    }
    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnPlayerDead,OnPlayerDead);
        EventManager.RemoveHandler(GameEvent.OnBridgeOpen,OnBridgeOpen);
        EventManager.RemoveHandler(GameEvent.OnBombActive,OnBombActive);
        EventManager.RemoveHandler(GameEvent.OnBombExplode,OnBombExplode);
        EventManager.RemoveHandler(GameEvent.OnCageOpen,OnCageOpen);
        EventManager.RemoveHandler(GameEvent.OnPlayerMove,OnPlayerMove);
        EventManager.RemoveHandler(GameEvent.OnSuccess,OnSuccess);
        EventManager.RemoveHandler(GameEvent.OnJump,OnJump);
        EventManager.RemoveHandler(GameEvent.OnCollectCoin,OnCollectCoin);
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.RemoveHandler(GameEvent.OnUndoBegin,OnUndoBegin);
        EventManager.RemoveHandler(GameEvent.OnAudioOnOff,OnAudioOnOff);

    }

    

    private void OnPlayerDead()
    {
        effectSource.PlayOneShot(GameOverSound);
        Debug.Log("HOWWWWWWWW");
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
        switch(playerData.movementType)
        {
            case MovementType.Roll:
                effectSource.PlayOneShot(RollMoveSound);
                break;
            case MovementType.Jump:
                effectSource.PlayOneShot(JumpMoveSound);
                break;
            case MovementType.Dash:
                effectSource.PlayOneShot(DashMoveSound);
                break;
        }
        
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
        effectSource.PlayOneShot(SuccessSound1);
        StartCoroutine(PlaySecondSuccess());
    }

    private void OnCollectCoin()
    {
        effectSource.PlayOneShot(CoinSound);
    }

    private void OnNextLevel()
    {
        effectSource.PlayOneShot(NextLevelSound);
    }

    private void OnUndoBegin()
    {
        effectSource.PlayOneShot(UndoSound);
    }

    private void OnAudioOnOff()
    {
        isSoundOn=!isSoundOn;

        if(isSoundOn)
            SoundOnOff(false,true,soundOn,soundOff,effectSource);
        else
            SoundOnOff(true,false,soundOn,soundOff,effectSource);
    }

    private void SoundOnOff(bool val1,bool val2,GameObject gameObject1,GameObject gameObject2,AudioSource audioSource)
    {
        gameObject1.SetActive(val1);
        gameObject2.SetActive(val2);
        audioSource.mute=val1;
        
    }

    private IEnumerator PlaySecondSuccess()
    {
        yield return waitForSeconds;
        effectSource.PlayOneShot(SuccessSound2);
    }
}
