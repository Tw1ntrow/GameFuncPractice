using System.Collections.Generic;

public class ProductTable
{
    public static List<ProductItem> TestTable { get; } = CreateTestTable();

    private static List<ProductItem> CreateTestTable()
    {
        List<ProductItem> table = new List<ProductItem>();

        // 10�̃e�X�g�p�A�C�e�����쐬
        for (int i = 1; i <= 10; i++)
        {
            var item = new Item
            {
                Id = i,
                Name = $"Item {i}",
                Description = $"�A�C�e���̐��� Item {i}"
            };

            var productItem = new ProductItem
            {
                Id = i,
                Name = $"ProductItem {i}",
                CurrencyType = CurrencyManager.GoldInstance,
                Price = i * 100,
                Item = item
            };

            // �݌ɐ��̐ݒ�(�e�X�g�p)
            if (i >= 7)
            {
                productItem.Stock = 10; // �݌ɐ����w��
            }

            table.Add(productItem);
        }

        return table;
    }
}
