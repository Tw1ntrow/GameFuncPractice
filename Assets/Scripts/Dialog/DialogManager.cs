using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ダイアログの生成と破棄を行う
/// </summary>
public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance { get; private set; }

    // 外から渡せばMonoBehaviourは不要だが、一旦これで
    [SerializeField]
    private Transform dialogParent;
    [SerializeField]
    private GameObject dialogPrefab;

    private List<IDialog> dialogList = new List<IDialog>();

    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // テスト用
    private void Start()
    {
        CreateDialog<CommonDialog>(new CommonDialogParameter("テスト", "本文"));
    }

    /// <summary>
    /// ダイアログを生成する
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="parameter"></param>
    public void CreateDialog<T>(DialogParameter parameter) where T : IDialog
    {
        T dialogGO = Instantiate(dialogPrefab, dialogParent).GetComponent<T>();
        dialogGO.ViewDialog(parameter);
        dialogGO.OnClickCloseButton += CloseDialog;
        dialogList.Add(dialogGO);
    }

    /// <summary>
    /// ダイアログを閉じる
    /// ダイアログ側で閉じるボタンが押された場合、コールバックを通じてこのイベントが発行される
    /// </summary>
    /// <param name="dialog"></param>
    public void CloseDialog(IDialog dialog) 
    {
        dialog.CloseDialog();
        // リストから引数のダイアログを削除
        dialogList.Remove(dialog);
    }
}