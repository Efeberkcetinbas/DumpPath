using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShopCharacterSelection : MonoBehaviour
{
    public List<ShopCharacters> shopCharacters=new List<ShopCharacters>();
    
    public GameData gameData;
    public PlayerData playerData;
    public LevelData levelData;

    private void Start() 
    {
        //shopBalls[ballData.selectedBallIndex].button.image.color=Color.green;
    }

    //Purchase
    public void SelectCharacter(int selectedIndex)
    {
        if(shopCharacters[selectedIndex].button.interactable)
        {
            //Throw Event
            shopCharacters[selectedIndex].lockImage.SetActive(false);
            if(!shopCharacters[selectedIndex].isPurchased)
            {
                levelData.score-=shopCharacters[selectedIndex].price;
                shopCharacters[selectedIndex].shopCharacterData.isPurchased=true;
                EventManager.Broadcast(GameEvent.OnShopCharacterSelected);
                EventManager.Broadcast(GameEvent.OnUIUpdate);
            }
            
            

            for (int i = 0; i < shopCharacters.Count; i++)
            {
                shopCharacters[i].button.image.color=Color.white;
                shopCharacters[i].transform.DOScale(Vector3.one,0.2f);
            }

            shopCharacters[selectedIndex].button.image.color=Color.green;
            shopCharacters[selectedIndex].transform.DOScale(Vector3.one*1.1f,0.2f);
            playerData.selectedIndex=selectedIndex;
            EventManager.Broadcast(GameEvent.OnCharacterChange);
        }
    }
}
