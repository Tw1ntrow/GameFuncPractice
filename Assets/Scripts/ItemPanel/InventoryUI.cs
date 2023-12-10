using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    [SerializeField]
    private GameObject inventoryPanel; // アイテムウインドウのパネル

    void Start()
    {
        inventoryPanel.SetActive(true); // 初期状態では非表示
    }

    public void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }
}