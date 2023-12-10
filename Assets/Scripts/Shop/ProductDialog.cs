
using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 商品詳細ダイアログを表示
/// </summary>
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

    // 商品詳細ダイアログを表示
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

    // スライダーの値が変更された際に値段を更新する
    public void OnChanged()
    {
        currency.text = $"値段: {price * Mathf.RoundToInt(slider.value)}{currencyName} 個数:{Mathf.RoundToInt(slider.value)}";
    }

    // 購入ボタンが押された際に購入処理を行う
    public void OnCliickPurchase()
    {
        onPurchase(productId, Mathf.RoundToInt(slider.value));
        OnCloseDialog();
    }

    // 閉じるボタンが押された際にダイアログを閉じる
    public void OnCloseDialog()
    {
        Destroy(gameObject);
    }
}
