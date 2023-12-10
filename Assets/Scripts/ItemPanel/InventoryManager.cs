using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

namespace ItemPanel
{
    public class InventoryManager : MonoBehaviour
    {
        public Item[] items; // 事前に設定したアイテムのリスト
        public GameObject slotPrefab; // スロットのプレハブ
        private List<GameObject> itemSlots = new List<GameObject>(); // 生成されたアイテムスロットを保持

        void Start()
        {
            foreach (var item in items)
            {
                AddSlot(item);
            }
        }

        public void AddItem(Item newItem)
        {
            foreach (var item in items)
            {
                if (item.itemName == newItem.itemName)
                {
                    item.quantity += newItem.quantity;
                    UpdateSlots();
                    return;
                }
            }
            var listOfItems = items.ToList();
            listOfItems.Add(newItem);
            items = listOfItems.ToArray();
            AddSlot(newItem);
        }

        private void AddSlot(Item item)
        {
            GameObject slot = Instantiate(slotPrefab, this.transform);
            itemSlots.Add(slot);
            UpdateSlot(slot, item);
        }

        private void UpdateSlots()
        {
            for (int i = 0; i < itemSlots.Count; i++)
            {
                UpdateSlot(itemSlots[i], items[i]);
            }
        }

        public void OnAddItem()
        {
            // テスト用のアイテムを追加
            AddItem(new Item
            {
                itemName = "New Item",
                itemIcon = Resources.Load<Sprite>("ItemPanel/Icons/ItemIcon"),
                description = "This is a new item.",
                quantity = 1
            });
            UpdateSlots();
        }

        // スロットのUIを更新
        private void UpdateSlot(GameObject slot, Item item)
        {
            slot.transform.Find("ItemIcon").GetComponent<Image>().sprite = item.itemIcon;
            slot.transform.Find("ItemName").GetComponent<Text>().text = item.itemName;

            var quantityText = slot.transform.Find("ItemQuantity").GetComponent<Text>();
            quantityText.text = item.quantity > 1 ? item.quantity.ToString() : "";
        }

        public void SortItemsAscending()
        {
            // アイテムを昇順ソート
            items = items.OrderBy(item => item.itemName).ToArray();
            UpdateSlots();
        }

        public void SortItemsDescending()
        {
            // アイテムを降順ソート
            items = items.OrderByDescending(item => item.itemName).ToArray();
            UpdateSlots();
        }
    }
}