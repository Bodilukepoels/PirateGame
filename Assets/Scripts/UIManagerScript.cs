using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManagerScript : MonoBehaviour
{
    public static UIManagerScript Instance;

    public TMP_Text bananasTxt;

    void Awake()
    {
        // Controleer of er nog geen UIManagerScript bestaat.
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
        // Zet de tekst van de bananen op het scherm.
        bananasTxt.text = totalBananas.bananas.ToString();

        // Luistert naar veranderingen in het aantal bananen.
        totalBananas.OnBananasChanged += UpdateBananasText;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        totalBananas.OnBananasChanged -= UpdateBananasText;

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Controleert of de nieuwe scène "youDied" is. Zo ja, delete de gameObject
        if (scene.name == "youDied")
        {
            Destroy(gameObject);
        }
    }

    private void UpdateBananasText(int newBananas)
    {
        // Past de tekst aan om het nieuwe aantal bananen te laten zien
        bananasTxt.text = newBananas.ToString();
    }
}
