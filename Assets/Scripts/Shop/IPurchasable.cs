
public interface IPurchasable
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICurrency CurrencyType { get; set; }
    public int Price { get; set; }
    public int Stock { get; set; }

    public void OnPurchase(int quantity);
}
