using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ButtonInfo : MonoBehaviour
{
    public int UpgradeID;
    public TMP_Text PriceText;
    public TMP_Text QuantityTxt;

    void Start()
    {
        UpdatePriceText();
    }

    public void UpdatePriceText()
    {
        if (ShopManagerScript.Instance != null)
        {
            PriceText.text = ShopManagerScript.Instance.shopItems[1, UpgradeID].ToString();
            QuantityTxt.text = ShopManagerScript.Instance.shopItems[2, UpgradeID].ToString();
        }
    }
}
