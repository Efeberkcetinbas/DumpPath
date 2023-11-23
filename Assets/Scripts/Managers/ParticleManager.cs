using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> successParticles=new List<ParticleSystem>();


    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnSuccess,OnSuccess);
        
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnSuccess,OnSuccess);
    }

    private void OnSuccess()
    {
        for (int i = 0; i < successParticles.Count; i++)
        {
            successParticles[i].Play();
        }
    }
}
