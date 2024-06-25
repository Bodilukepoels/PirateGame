using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ShopManagerScript : MonoBehaviour
{
    public static ShopManagerScript Instance;

    public int[,] shopItems = new int[3, 5]; // 2D-array voor winkelitems: [rij: ID's, prijzen, hoeveelheden] x [kolom: upgrade categorieën]

    public static float attackSpeed;
    public static float speed;
    public static float hp;
    public static float attackDamage;

    public AudioSource angryMonkeySound;

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
            return;
        }
    }

    void Start()
    {
        InitializeShopItems(); // Initialiseer shop
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "youDied")
        {
            Destroy(gameObject);
        }
    }

    public void InitializeShopItems()
    {
        // ID's
        shopItems[0, 1] = 1; 
        shopItems[0, 2] = 2;
        shopItems[0, 3] = 3;
        shopItems[0, 4] = 4;

        // Prijzen
        shopItems[1, 1] = 10;
        shopItems[1, 2] = 10;
        shopItems[1, 3] = 10;
        shopItems[1, 4] = 10;

        // Hoeveelheden
        shopItems[2, 1] = 0;
        shopItems[2, 2] = 0;
        shopItems[2, 3] = 0;
        shopItems[2, 4] = 0;
    }

    public void Buy(int upgradeID)
    {
        GameObject ButtonRef = EventSystem.current.currentSelectedGameObject;

        if (ButtonRef == null)
        {
            Debug.LogWarning("No button?");
            return;
        }

        ButtonInfo buttonInfo = ButtonRef.GetComponent<ButtonInfo>();

        int price = shopItems[1, upgradeID]; // Prijs van de upgrade

        // Controleert of de player genoeg bananen heeft om iets te kopen in de shop
        if (totalBananas.bananas >= price)
        {
            totalBananas.bananas -= price; // Verminder het aantal bananen met de prijs
            shopItems[2, upgradeID]++; // Verhoog de hoeveelheid van de gekochte upgrade

            // Bereken de nieuwe prijs met 15% verhoging
            int newPrice = Mathf.CeilToInt(price * 1.15f);
            if (newPrice < 1) newPrice = 1;
            shopItems[1, upgradeID] = newPrice; // Update de nieuwe prijs in de shopItems

            // Update de tekst van hoeveelheid en prijs op de knop
            buttonInfo.QuantityTxt.text = shopItems[2, upgradeID].ToString();
            buttonInfo.UpdatePriceText();

            UpdateUpgradeStats(upgradeID);
            UpdatePlayerStats(); // Update de stats van de player na aankoop
        }
        else
        {
            Debug.LogWarning("Not enough bananas...");

            // Speelt een boos aap geluid af als je niet genoeg bananen hebt
            angryMonkeySound.Play();
        }
    }

    private void UpdateUpgradeStats(int upgradeID)
    {
        // Update player stats gebasseert op de upgradeID met een switch-case
        switch (upgradeID)
        {
            case 1:
                attackSpeed = shopItems[2, upgradeID];
                break;
            case 2:
                speed = shopItems[2, upgradeID];
                break;
            case 3:
                hp = shopItems[2, upgradeID];
                break;
            case 4:
                attackDamage = shopItems[2, upgradeID];
                break;
        }

        Debug.Log($"ATK SPD: {attackSpeed}, SPD: {speed}, HP: {hp}, ATK DMG: {attackDamage}");
    }

    private void UpdatePlayerStats()
    {
        // Update player stats in PlayerController
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.UpdateStats(); // Dit geeft de player zijn nieuwe stats
        }
    }
}
