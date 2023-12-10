

/// <summary>
/// アイテムが取得できる商品を表現するクラス
/// 値段、名前、取得アイテム、在庫数を持つ
/// パッケージ商品のような、複数商品が手に入るものは別クラスとして定義されることを想定
/// </summary>
public class ProductItem : IPurchasable
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICurrency CurrencyType { get; set; }
    public int Price { get; set; }

    public Item Item { get; set; }

    public int Stock { get; set; } = -1; // -1で無限の在庫数を表現

    public void OnPurchase(int quantity)
    {
        bool result = UserCurrency.SpendCurrency(CurrencyManager.GoldInstance, Price * quantity);
        if (!result)
        {
            UnityEngine.Debug.LogError($"購入できませんでした ProductItem:{Id}");
            return;
        }

        if (Stock == -1 || Stock >= quantity)
        {
            if (Stock != -1)
            {
                Stock -= quantity;
            }
            Inventory.AddItem(Item, quantity);
        }
        else
        {
            UnityEngine.Debug.LogError($"在庫不足です ProductItem:{Id}");
        }
    }
}
