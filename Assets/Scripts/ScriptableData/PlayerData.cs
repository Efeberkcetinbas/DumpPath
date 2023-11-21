using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData", order = 1)]
public class PlayerData : ScriptableObject 
{
    public bool playerCanMove=true;
    

    //Direction
    public int UpMove;
    public int DownMove;
    public int LeftMove;
    public int RightMove;

}
