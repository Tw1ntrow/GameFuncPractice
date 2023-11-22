using UnityEngine;
using UnityEngine.UI;

namespace ItemPanel
{
    public class InventoryManager : MonoBehaviour
    {
        public Item[] items; // 事前に設定したアイテムのリスト
        public GameObject slotPrefab; // スロットのプレハブ

        void Start()
        {
            foreach (var item in items)
            {
                GameObject slot = Instantiate(slotPrefab, this.transform);
                // スロットのUIをアイテム情報で更新
                slot.transform.Find("ItemIcon").GetComponent<Image>().sprite = item.itemIcon;
                slot.transform.Find("ItemName").GetComponent<Text>().text = item.itemName;
            }
        }
    }
}