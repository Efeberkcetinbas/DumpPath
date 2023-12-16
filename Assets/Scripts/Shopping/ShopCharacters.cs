using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopCharacters : MonoBehaviour
{
    public int price;

    public bool isPurchased=false;
    public bool canBuy=false;


    public GameObject lockImage,goldImage;

    internal Button button;

    public TextMeshProUGUI priceText;


    public ShopCharacterData shopCharacterData;
    public GameData gameData;
    public LevelData levelData;
    private void Start() 
    {
        button=GetComponent<Button>();
        priceText.text=price.ToString();
        CheckPurchase();
    }

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnShopCharacterSelected,OnBallSelected);
        EventManager.AddHandler(GameEvent.OnShopOpen,OnShopOpen);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnShopCharacterSelected,OnBallSelected);
        EventManager.RemoveHandler(GameEvent.OnShopOpen,OnShopOpen);
    }


    

    private void OnBallSelected()
    {
        CheckPurchase();
    }

    private void OnShopOpen()
    {
        CheckPurchase();
    }
    

    private void CheckPurchase()
    {
        if(shopCharacterData.isPurchased)
        {
            //priceText.text="B";
            
            lockImage.SetActive(false);
            button.interactable=true;
            

            //button.image.color=Color.green;
            goldImage.SetActive(false);
            //tickImage.SetActive(true);
            priceText.gameObject.SetActive(false);
            isPurchased=true;

        }

        if(levelData.score>=price || shopCharacterData.isPurchased)
        {
            button.interactable=true;
            canBuy=true;
        }

        if(!shopCharacterData.isPurchased)
        {
            if(levelData.score<price)
            {
                button.interactable=false;
                canBuy=false;
            }
        }
    }
}
