
using System;
using UnityEngine;
using UnityEngine.UI;

public class ProductDialog : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private Text currency;

    private int price;
    private string currencyName;
    private int productId;
    private Action<int, int> onPurchase;
    public void DisplayDialog(int productid,int Price,string currencyname,int purchasableQuantity, Action<int, int> onpurchase)
    {
        slider.maxValue = purchasableQuantity;
        slider.minValue = 1;
        productId = productid;
        onPurchase = onpurchase;
        price = Price;
        currencyName = currencyname;

        OnChanged();
    }

    public void OnChanged()
    {
        currency.text = $"ílíi: {price * Mathf.RoundToInt(slider.value)}{currencyName} å¬êî:{Mathf.RoundToInt(slider.value)}";
    }

    public void OnCliickPurchase()
    {
        onPurchase(productId, Mathf.RoundToInt(slider.value));
        OnCloseDialog();
    }

    public void OnCloseDialog()
    {
        Destroy(gameObject);
    }
}
