using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private InventoryView inventoryView;
    [SerializeField]
    private CommonDialog commonDialog;
    void Start()
    {
        // �C���x���g�����_�E�����[�h(�{���̓T�[�o�[���玝���Ă���)
        List<InventoryItem> items = new List<InventoryItem>();
        items = DownloadInventory();

        // �A�C�e���ꗗ��ʂ�\��
        inventoryView.DisplayInventory(items,OnClickItem);
    }

    private void OnClickItem(int itemId)
    {
        // �A�C�e���ڍ׃_�C�A���O��\��
        InventoryItem item = Inventory.Items.Find(x => x.Item.Id == itemId);
        CommonDialog commondialog = Instantiate<CommonDialog>(commonDialog,this.transform);
        commondialog.ViewDialog(item.Item.Name, item.Item.Description);
    }

    private List<InventoryItem> DownloadInventory()
    {
        return Inventory.Items;
    }

}
