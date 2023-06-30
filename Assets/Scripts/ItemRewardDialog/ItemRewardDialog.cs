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
            // �{���̓A�C�e����ID���󂯎�邪ID����ǂݎ��@�\�͖����̂Ŗ�������
            // �C�x���g�͓��Ɏw�肵�Ȃ�
            panel.GetComponent<ItemPanel>().ViewItem(i.ToString(), "1", items[i]);
        }
    }

}
