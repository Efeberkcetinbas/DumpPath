using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera cm;
    public CinemachineVirtualCamera cm2;

    [Header("Shake Control")]
    [SerializeField] private float shakeTime = 0.5f;
    [SerializeField] private float amplitudeGain=1;
    [SerializeField] private float frequencyGain=1;
    [SerializeField] private float newFieldOfView;
    [SerializeField] private float oldFieldOfView;
    private CinemachineBasicMultiChannelPerlin noise;

    private bool isFirstCam=true;
    

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.AddHandler(GameEvent.OnShopOpen,OnShopOpen);
        EventManager.AddHandler(GameEvent.OnShopClose,OnShopClose);
        EventManager.AddHandler(GameEvent.OnGround,OnGround);
        EventManager.AddHandler(GameEvent.OnSuccess,OnSuccess);
        EventManager.AddHandler(GameEvent.OnBombExplode,OnBombExplode);
        EventManager.AddHandler(GameEvent.OnCameraChange,OnCameraChange);
        EventManager.AddHandler(GameEvent.OnJump,OnJump);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.RemoveHandler(GameEvent.OnShopOpen,OnShopOpen);
        EventManager.RemoveHandler(GameEvent.OnShopClose,OnShopClose);
        EventManager.RemoveHandler(GameEvent.OnGround,OnGround);
        EventManager.RemoveHandler(GameEvent.OnSuccess,OnSuccess);
        EventManager.RemoveHandler(GameEvent.OnBombExplode,OnBombExplode);
        EventManager.RemoveHandler(GameEvent.OnCameraChange,OnCameraChange);
        EventManager.RemoveHandler(GameEvent.OnJump,OnJump);
    }

   

    private void OnShopOpen()
    {
        ChangePriority(8);
    }

    private void OnShopClose()
    {
        ChangePriority(11);
    }


    private void OnNextLevel()
    {
        ChangeFieldOfView(oldFieldOfView,2);
        isFirstCam=true;
    }

    private void OnGround()
    {
        Noise(amplitudeGain,frequencyGain,shakeTime);
    }

    private void OnSuccess()
    {
        ChangeFieldOfView(newFieldOfView,2);
    }

    private void OnBombExplode()
    {
        Noise(3,3,1);
    }

    private void OnJump()
    {
        ChangeFieldOfViewHit(newFieldOfView,oldFieldOfView,0.25f);
    }

    private void OnCameraChange()
    {
        if(isFirstCam)
        {
            cm2.gameObject.SetActive(true);
            cm2.m_Follow=FindObjectOfType<PortalControl>().transform;
            cm2.m_LookAt=FindObjectOfType<PortalControl>().transform;
        } 
        else
        {
            cm2.gameObject.SetActive(false);            
        }

        isFirstCam=!isFirstCam;
    }
    

    private void Start() 
    {
        noise=cm.GetComponentInChildren<CinemachineBasicMultiChannelPerlin>();
        if(noise == null)
            Debug.LogError("No MultiChannelPerlin on the virtual camera.", this);
        else
            Debug.Log($"Noise Component: {noise}");

    }

    private void Noise(float amplitudeGain,float frequencyGain,float shakeTime) 
    {
        noise.m_AmplitudeGain = amplitudeGain;
        noise.m_FrequencyGain = frequencyGain;
        StartCoroutine(ResetNoise(shakeTime));    
    }

    private IEnumerator ResetNoise(float duration)
    {
        yield return new WaitForSeconds(duration);
        noise.m_AmplitudeGain = 0;
        noise.m_FrequencyGain = 0;    
    }
    public void ChangeFieldOfView(float fieldOfView, float duration = 1)
    {
        DOTween.To(() => cm.m_Lens.FieldOfView, x => cm.m_Lens.FieldOfView = x, fieldOfView, duration);
    }

    

    public void ChangeFieldOfViewHit(float newFieldOfView, float oldFieldOfView, float duration = 1)
    {
        DOTween.To(() => cm.m_Lens.FieldOfView, x => cm.m_Lens.FieldOfView = x, newFieldOfView, duration).OnComplete(()=>{
            DOTween.To(() => cm.m_Lens.FieldOfView, x => cm.m_Lens.FieldOfView = x, oldFieldOfView, duration);
        });
    }

    public void ChangeFollow(Transform Ball)
    {
        cm.m_Follow=Ball;
    }

    private void ChangePriority(int val)
    {
        cm.m_Priority=val;
    }
}
