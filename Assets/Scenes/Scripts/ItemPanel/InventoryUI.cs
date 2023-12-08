using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    [SerializeField]
    private GameObject inventoryPanel; // �A�C�e���E�C���h�E�̃p�l��

    void Start()
    {
        inventoryPanel.SetActive(true); // ������Ԃł͔�\��
    }

    public void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }
}