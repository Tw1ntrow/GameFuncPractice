using System.Collections.Generic;
using System.Linq;

public class Inventory
{
    public static List<InventoryItem> Items { get; private set; } = new List<InventoryItem>();

    // ���̃f�[�^���쐬
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
                    Description = $"�A�C�e���̐��� Item {i}"
                },
                Quantity = i //�@�K���Ɍ���ݒ�
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