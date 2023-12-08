using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryView : MonoBehaviour
{
    [SerializeField]
    private Transform ItemPanelParent;
    [SerializeField]
    private GameObject ItemPanelPrefab;

    /// <summary>
    /// アイテムパネルを生成
    /// </summary>
    /// <param name="inventory"></param>
    /// <param name="onClock"></param>
    public void DisplayInventory(List<InventoryItem> inventory,Action<int> onClock)
    {
        foreach (InventoryItem item in inventory)
        {
            //ItemPanel itemPanel = Instantiate(ItemPanelPrefab, ItemPanelParent).GetComponent<ItemPanel>();
            //itemPanel.ViewItem(item.Item.Name, item.Quantity.ToString(), item.Item.Id, onClock);
        }

    }

}
