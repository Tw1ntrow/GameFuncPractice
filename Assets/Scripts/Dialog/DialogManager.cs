using System;
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
    // ダイアログのプレハブ、本来は外からダウンロードする
    [SerializeField]
    private GameObject dialogPrefab;
    [SerializeField]
    private GameObject webViewPrefab;

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

    /// <summary>
    /// ダイアログを生成する
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="parameter"></param>
    public void CreateDialog<T>(DialogParameter parameter) where T : IDialog
    {
        GameObject prefab;

        // 本来は名前一致でサーバーからダウンロードする想定
        if (typeof(T) == typeof(CommonDialog))
        {
            prefab = dialogPrefab;
        }
        else if (typeof(T) == typeof(WebViewDialog))
        {
            prefab = webViewPrefab;
        }
        else
        {
            throw new ArgumentException($"Invalid dialog type: {typeof(T)}");
        }

        T dialogGO = Instantiate(prefab, dialogParent).GetComponent<T>();
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