

public class ProductItem : IPurchasable
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICurrency CurrencyType { get; set; }
    public int Price { get; set; }

    public Item Item { get; set; }

    public int Stock { get; set; } = -1; // -1�Ŗ����̍݌ɐ���\��

    public void OnPurchase(int quantity)
    {
        bool result = UserCurrency.SpendCurrency(CurrencyManager.GoldInstance, Price * quantity);
        if (!result)
        {
            UnityEngine.Debug.LogError($"�w���ł��܂���ł��� ProductItem:{Id}");
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
            UnityEngine.Debug.LogError($"�݌ɕs���ł� ProductItem:{Id}");
        }
    }
}
