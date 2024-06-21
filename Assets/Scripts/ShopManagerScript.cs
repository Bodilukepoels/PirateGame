using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ShopManagerScript : MonoBehaviour
{
    public static ShopManagerScript Instance;

    public int[,] shopItems = new int[3, 4];
    public static float attackSpeed;
    public static float speed;
    public static float hp;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        InitializeShopItems();
    }

    public void InitializeShopItems()
    {
        // ID's
        shopItems[0, 1] = 1;
        shopItems[0, 2] = 2;
        shopItems[0, 3] = 3;

        // Prices
        shopItems[1, 1] = 10;
        shopItems[1, 2] = 10;
        shopItems[1, 3] = 10;

        // Quantities
        shopItems[2, 1] = 0;
        shopItems[2, 2] = 0;
        shopItems[2, 3] = 0;
    }

    public void Buy()
    {
        GameObject ButtonRef = EventSystem.current.currentSelectedGameObject;
        if (ButtonRef == null)
        {
            Debug.LogWarning("No button selected");
            return;
        }

        ButtonInfo buttonInfo = ButtonRef.GetComponent<ButtonInfo>();
        if (buttonInfo == null)
        {
            Debug.LogWarning("ButtonInfo component not found");
            return;
        }

        int upgradeID = buttonInfo.UpgradeID;
        int price = shopItems[1, upgradeID];

        Debug.Log("Trying to buy upgrade ID: " + upgradeID + ", Price: " + price + ", Current bananas: " + totalBananas.bananas);

        if (totalBananas.bananas >= price)
        {
            totalBananas.bananas -= price;
            shopItems[2, upgradeID]++;

            shopItems[1, upgradeID] = Mathf.CeilToInt(price * 1.15f);

            Debug.Log("Purchase successful. New quantity: " + shopItems[2, upgradeID] + ", New price: " + shopItems[1, upgradeID]);

            buttonInfo.QuantityTxt.text = shopItems[2, upgradeID].ToString();
            buttonInfo.UpdatePriceText();

            UpdatePlayerStats();
        }
        else
        {
            Debug.LogWarning("Not enough bananas to purchase upgrade");
        }
    }

    private void UpdatePlayerStats()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.UpdateStats();
        }
    }
}
