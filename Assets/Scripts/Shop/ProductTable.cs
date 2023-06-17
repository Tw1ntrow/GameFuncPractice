using System.Collections.Generic;

public class ProductTable
{
    public static List<ProductItem> TestTable { get; } = CreateTestTable();

    private static List<ProductItem> CreateTestTable()
    {
        List<ProductItem> table = new List<ProductItem>();

        // 10個のテスト用アイテムを作成
        for (int i = 1; i <= 10; i++)
        {
            var item = new Item
            {
                Id = i,
                Name = $"Item {i}",
                Description = $"アイテムの説明 Item {i}"
            };

            var productItem = new ProductItem
            {
                Id = i,
                Name = $"ProductItem {i}",
                CurrencyType = CurrencyManager.GoldInstance,
                Price = i * 100,
                Item = item
            };

            // 在庫数の設定(テスト用)
            if (i >= 7)
            {
                productItem.Stock = 10; // 在庫数を指定
            }

            table.Add(productItem);
        }

        return table;
    }
}
