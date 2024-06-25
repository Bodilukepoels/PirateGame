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
        bananasTxt.text = totalBananas.bananas.ToString();
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
        if (scene.name == "youDied")
        {
            Destroy(gameObject);
        }
    }

    private void UpdateBananasText(int newBananas)
    {
        bananasTxt.text = newBananas.ToString();
    }
}
