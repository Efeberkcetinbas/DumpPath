using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Obstacleable
{
    public Coin()
    {
        canStay=false;
    }

    internal override void DoAction(TriggerControl player)
    {
        Debug.Log("Coin");
    }
}
    
