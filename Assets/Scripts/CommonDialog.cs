using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 汎用ダイアログ
/// 一旦タイトル、本文、閉じるボタンのみ配置
/// </summary>
public class CommonDialog : MonoBehaviour
{
    [SerializeField]
    private Text titleText;
    [SerializeField] 
    private Text messageText;

    /// <summary>
    /// ダイアログを表示
    /// </summary>
    /// <param name="title">タイトル</param>
    /// <param name="message">本文</param>
    public void ViewDialog(string title, string message/*, string okButtonText, string cancelButtonText, System.Action onOk, System.Action onCancel*/)
    {
        titleText.text = title;
        messageText.text = message;
    }

    // 仮、本来は管理クラスで管理される
    public void CloseDialog()
    {
        Destroy(gameObject);
    }
}
