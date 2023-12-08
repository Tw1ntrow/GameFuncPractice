using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

namespace ItemPanel
{
    public class InventoryManager : MonoBehaviour
    {
        public Item[] items; // ���O�ɐݒ肵���A�C�e���̃��X�g
        public GameObject slotPrefab; // �X���b�g�̃v���n�u
        private List<GameObject> itemSlots = new List<GameObject>(); // �������ꂽ�A�C�e���X���b�g��ێ�

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
            // �e�X�g�p�̃A�C�e����ǉ�
            AddItem(new Item
            {
                itemName = "New Item",
                itemIcon = Resources.Load<Sprite>("ItemPanel/Icons/ItemIcon"),
                description = "This is a new item.",
                quantity = 1
            });
            UpdateSlots();
        }

        // �X���b�g��UI���X�V
        private void UpdateSlot(GameObject slot, Item item)
        {
            slot.transform.Find("ItemIcon").GetComponent<Image>().sprite = item.itemIcon;
            slot.transform.Find("ItemName").GetComponent<Text>().text = item.itemName;

            var quantityText = slot.transform.Find("ItemQuantity").GetComponent<Text>();
            quantityText.text = item.quantity > 1 ? item.quantity.ToString() : "";
        }

        public void SortItemsAscending()
        {
            // �A�C�e���������\�[�g
            items = items.OrderBy(item => item.itemName).ToArray();
            UpdateSlots();
        }

        public void SortItemsDescending()
        {
            // �A�C�e�����~���\�[�g
            items = items.OrderByDescending(item => item.itemName).ToArray();
            UpdateSlots();
        }
    }
}