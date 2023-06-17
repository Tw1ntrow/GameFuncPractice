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
    // 商品の在庫数が0でユーザーの持っている通貨（指定された種類の通貨）が商品の価格よりも少ない場合は販売不可とする
    private bool IsSoldOut(IPurchasable product)
    {
        // 在庫が-1の場合は常に売り切れているとみなす
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

    // 購入可能な最大数を取得する
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
        dialog.ViewDialog("購入成功", $"商品ID:{productId}を{quantity}個購入しました。");

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
        gold.text = $"残額:{UserCurrency.GetBalance(CurrencyManager.GoldInstance).ToString()}";

    }

}
