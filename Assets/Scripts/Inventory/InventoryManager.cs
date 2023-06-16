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
        // インベントリをダウンロード(本来はサーバーから持ってくる)
        List<InventoryItem> items = new List<InventoryItem>();
        items = DownloadInventory();

        // アイテム一覧画面を表示
        inventoryView.DisplayInventory(items,OnClickItem);
    }

    private void OnClickItem(int itemId)
    {
        // アイテム詳細ダイアログを表示
        InventoryItem item = Inventory.Items.Find(x => x.Item.Id == itemId);
        CommonDialog commondialog = Instantiate<CommonDialog>(commonDialog,this.transform);
        commondialog.ViewDialog(item.Item.Name, item.Item.Description);
    }

    private List<InventoryItem> DownloadInventory()
    {
        return Inventory.Items;
    }

}
