using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData", order = 1)]
public class PlayerData : ScriptableObject 
{
    public bool playerCanMove=true;
    public bool isPathUpgrade=false;
    public bool playerInGround;
    public bool playerInSomething;
    
    public MovementType movementType;
    //Direction
    public float UpMove;
    public float DownMove;
    public float LeftMove;
    public float RightMove;

    public int selectedIndex;

}
