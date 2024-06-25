using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonInfo : MonoBehaviour
{
    public int UpgradeID;
    public TMP_Text PriceText;
    public TMP_Text QuantityTxt;
    private Button button;

    void Start()
    {
        //De button wordt meteen toegevoegd aan de onclick() omdat het anders bij scene changes leeg blijft
        button = GetComponent<Button>();
        button.onClick.AddListener(() => ShopManagerScript.Instance.Buy(UpgradeID));

        UpdatePriceText();
    }

    //De prijzentext die wordt weergeven op de buttons in de shop
    public void UpdatePriceText()
    {
            PriceText.text = ShopManagerScript.Instance.shopItems[1, UpgradeID].ToString();
            QuantityTxt.text = ShopManagerScript.Instance.shopItems[2, UpgradeID].ToString();
    }
}
