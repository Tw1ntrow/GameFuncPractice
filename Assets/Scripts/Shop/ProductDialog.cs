
using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���i�ڍ׃_�C�A���O��\��
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

    // ���i�ڍ׃_�C�A���O��\��
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

    // �X���C�_�[�̒l���ύX���ꂽ�ۂɒl�i���X�V����
    public void OnChanged()
    {
        currency.text = $"�l�i: {price * Mathf.RoundToInt(slider.value)}{currencyName} ��:{Mathf.RoundToInt(slider.value)}";
    }

    // �w���{�^���������ꂽ�ۂɍw���������s��
    public void OnCliickPurchase()
    {
        onPurchase(productId, Mathf.RoundToInt(slider.value));
        OnCloseDialog();
    }

    // ����{�^���������ꂽ�ۂɃ_�C�A���O�����
    public void OnCloseDialog()
    {
        Destroy(gameObject);
    }
}
