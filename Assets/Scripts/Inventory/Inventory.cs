using System.Collections.Generic;

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
}

public class InventoryItem
{
    public Item Item;
    public int Quantity;
}