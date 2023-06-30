using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRewardDialog : MonoBehaviour
{
    [SerializeField]
    private Transform itemPanelParent;
    [SerializeField] 
    private GameObject itemPanelPrefab;
    
    public void Create(List<int> items)
    {
        for(int i = 0; i < items.Count; i++)
        {
            var panel = Instantiate(itemPanelPrefab, itemPanelParent);
            // 本来はアイテムのIDを受け取るがIDから読み取る機構は無いので無視する
            // イベントは特に指定しない
            panel.GetComponent<ItemPanel>().ViewItem(i.ToString(), "1", items[i]);
        }
    }

}
