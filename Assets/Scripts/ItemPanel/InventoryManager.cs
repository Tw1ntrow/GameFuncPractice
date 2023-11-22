using UnityEngine;
using UnityEngine.UI;

namespace ItemPanel
{
    public class InventoryManager : MonoBehaviour
    {
        public Item[] items; // ���O�ɐݒ肵���A�C�e���̃��X�g
        public GameObject slotPrefab; // �X���b�g�̃v���n�u

        void Start()
        {
            foreach (var item in items)
            {
                GameObject slot = Instantiate(slotPrefab, this.transform);
                // �X���b�g��UI���A�C�e�����ōX�V
                slot.transform.Find("ItemIcon").GetComponent<Image>().sprite = item.itemIcon;
                slot.transform.Find("ItemName").GetComponent<Text>().text = item.itemName;
            }
        }
    }
}