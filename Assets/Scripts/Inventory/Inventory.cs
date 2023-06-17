using System.Collections.Generic;
using System.Linq;

public class Inventory
{
    public static List<InventoryItem> Items { get; private set; } = new List<InventoryItem>();

    // 仮のデータを作成
    static Inventory()
    {
        for (int i = 1; i <= 24; i++)
        {
            Items.Add(new InventoryItem
            {
                Item = new Item
                {
                    Id = i,
                    Name = $"Item {i}",
                    Description = $"アイテムの説明 Item {i}"
                },
                Quantity = i //　適当に個数を設定
            });
        }
    }

    public static void AddItem(Item item, int quantity)
    {
        var inventoryItem = Items.FirstOrDefault(i => i.Item.Id == item.Id);
        if (inventoryItem == null)
        {
            inventoryItem = new InventoryItem { Item = item, Quantity = quantity };
            Items.Add(inventoryItem);
        }
        else
        {
            inventoryItem.Quantity += quantity;
        }
    }
}

public class InventoryItem
{
    public Item Item;
    public int Quantity;
}