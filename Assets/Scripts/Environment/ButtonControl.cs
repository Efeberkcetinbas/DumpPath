using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ButtonControl : MonoBehaviour
{
    private void OnEnable() 
    {
        EventManager.AddIdHandler(GameEvent.OnOpenButton,OnOpenButton);
        EventManager.AddIdHandler(GameEvent.OnCloseButton,OnCloseButton);
    }

    private void OnDisable() 
    {
        EventManager.RemoveIdHandler(GameEvent.OnOpenButton,OnOpenButton);
        EventManager.RemoveIdHandler(GameEvent.OnCloseButton,OnCloseButton);
    }
    
    internal virtual void OnOpenButton(int id)
    {
        throw new System.NotImplementedException();
    }

    internal virtual void OnCloseButton(int id)
    {
        throw new System.NotImplementedException();
    }
}
