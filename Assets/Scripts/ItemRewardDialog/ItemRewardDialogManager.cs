using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテムの報酬ダイアログを生成する、ItemRewardDialogは本来ダウンロードするのでMonoBehaviourは継承しない
/// </summary>
public class ItemRewardDialogManager : MonoBehaviour
{
    [SerializeField]
    private ItemRewardDialog itemRewardDialogPrefab;
    [SerializeField]
    private Transform dialogParent;

    //　シングルトンインスタンス
    public static ItemRewardDialogManager instance;

    private void Awake()
    {
        //　シングルトンインスタンス生成
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //　インスタンスが生成されている場合はインスタンスを破棄
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        for(int i = 0; i < 10; i++)
        {
            CreateItemRewardDialog(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
        }
    }

    public void CreateItemRewardDialog(List<int> items)
    {
        ItemRewardDialog dialog = Instantiate<ItemRewardDialog>(itemRewardDialogPrefab, dialogParent);
        dialog.Create(items);


    }
    
}
