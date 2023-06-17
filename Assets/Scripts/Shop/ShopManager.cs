using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    private ShopListView shopListView;
    [SerializeField]
    private ProductDialog productDialog;
    [SerializeField]
    private CommonDialog commonDialog;

    [SerializeField]
    private Text gold;

    void Start()
    {
        UserCurrency.AddCurrency(CurrencyManager.GoldInstance, 1000);
        DebugGold();

        foreach (var product in ProductTable.TestTable)
        {
            shopListView.AddShopList(product, IsSoldOut(product), OnClickPanel);
        }

    }
    // ���i�̍݌ɐ���0�Ń��[�U�[�̎����Ă���ʉ݁i�w�肳�ꂽ��ނ̒ʉ݁j�����i�̉��i�������Ȃ��ꍇ�͔̔��s�Ƃ���
    private bool IsSoldOut(IPurchasable product)
    {
        // �݌ɂ�-1�̏ꍇ�͏�ɔ���؂�Ă���Ƃ݂Ȃ�
        if (product.Stock == 0)
        {
            return true;
        }

        return product.Price > UserCurrency.GetBalance(product.CurrencyType);
    }
    public void OnClickPanel(int ProductId)
    {
        ProductDialog dialog = Instantiate<ProductDialog>(productDialog, transform);
        IPurchasable product = ProductTable.TestTable.Find((product) => product.Id == ProductId);
        dialog.DisplayDialog(ProductId, product.Price, product.CurrencyType.Name,
            GetMaxPurchaseQuantity(product), OnSucsessPurchase);

    }

    // �w���\�ȍő吔���擾����
    private int GetMaxPurchaseQuantity(IPurchasable product)
    {
        int maxQuantityByBalance = UserCurrency.GetBalance(product.CurrencyType) / product.Price;

        if (product.Stock == -1) // Unlimited stock
        {
            return maxQuantityByBalance;
        }
        else
        {
            return Math.Min(maxQuantityByBalance, product.Stock);
        }
    }

    private void OnSucsessPurchase(int productId, int quantity)
    {
        CommonDialog dialog = Instantiate<CommonDialog>(commonDialog, transform);
        dialog.ViewDialog("�w������", $"���iID:{productId}��{quantity}�w�����܂����B");

        IPurchasable product = ProductTable.TestTable.Find((product) => product.Id == productId);
        product.OnPurchase(quantity);

        foreach (var productTable in ProductTable.TestTable)
        {
            shopListView.UpdateShopList(productTable.Id, IsSoldOut(productTable));
        }

        DebugGold();
    }

    private void DebugGold()
    {
        gold.text = $"�c�z:{UserCurrency.GetBalance(CurrencyManager.GoldInstance).ToString()}";

    }

}
