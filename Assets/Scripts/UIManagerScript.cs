using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        }
    }

    void Start()
    {
        bananasTxt.text = totalBananas.bananas.ToString();
        totalBananas.OnBananasChanged += UpdateBananasText;
    }

    void OnDestroy()
    {
        totalBananas.OnBananasChanged -= UpdateBananasText;
    }

    private void UpdateBananasText(int newBananas)
    {
        bananasTxt.text = newBananas.ToString();
    }
}
