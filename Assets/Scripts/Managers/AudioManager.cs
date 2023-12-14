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

    private WaitForSeconds waitForSeconds;

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

    }

    

    private void OnPlayerDead()
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


    private IEnumerator PlaySecondSuccess()
    {
        yield return waitForSeconds;
        effectSource.PlayOneShot(SuccessSound2);
    }
}
