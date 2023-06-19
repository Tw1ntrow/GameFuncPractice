using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 汎用ダイアログ
/// 一旦タイトル、本文、閉じるボタンのみ配置
/// </summary>
public class CommonDialog : MonoBehaviour, IDialog
{
    [SerializeField]
    private Text titleText;
    [SerializeField]
    private Text messageText;

    [SerializeField]
    private Transform buttonParent;

    [SerializeField]
    private Button okButton;
    [SerializeField]
    private Button cancelButton;

    public Action<IDialog> OnClickCloseButton { get; set; }

    /// <summary>
    /// ダイアログを表示
    /// </summary>
    /// <param name="title">タイトル</param>
    /// <param name="message">本文</param>
    public void ViewDialog(DialogParameter parameter)
    {
        if (parameter is not CommonDialogParameter)
        {
            Debug.LogError("パラメーターが不適切です");
            return;
        }

        CommonDialogParameter param = parameter as CommonDialogParameter;
        titleText.text = param.title;
        messageText.text = param.message;

        // パラメーターが指定されてない場合はOKボタンのみ配置
        if(param.buttonParameters == null)
        {
            CreateButton(new ButtonParameter(ButtonType.Ok,"Ok",null));
        }
        else
        {
            foreach (var buttonParameter in param.buttonParameters)
            {
                CreateButton(buttonParameter);
            }
        }
    }

    // ボタンを生成
    private void CreateButton(ButtonParameter parameter)
    {
        Button button;
        switch (parameter.Type)
        {
            case ButtonType.Ok:
                button = Instantiate(okButton, buttonParent);
                break;
            case ButtonType.Cancel:
                button = Instantiate(cancelButton, buttonParent);
                break;
            default:
                button = Instantiate(okButton, buttonParent); // 仮でOKボタンを置いてる
                break;
        }
        button.GetComponentInChildren<Text>().text = parameter.ButtonText;

        if(parameter.onAction != null)
        {
            button.onClick.AddListener(new UnityEngine.Events.UnityAction(parameter.onAction));

        }
        // イベント処理を行った後ダイアログを閉じる
        button.onClick.AddListener(OnClickClose);
    }

    private void OnClickClose()
    {
        OnClickCloseButton?.Invoke(this);

    }

    public void CloseDialog()
    {
        // ダイアログ閉じた際の見た目処理
        Destroy(this.gameObject);
    }

}
