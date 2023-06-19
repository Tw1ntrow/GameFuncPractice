using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �ėp�_�C�A���O
/// ��U�^�C�g���A�{���A����{�^���̂ݔz�u
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
    /// �_�C�A���O��\��
    /// </summary>
    /// <param name="title">�^�C�g��</param>
    /// <param name="message">�{��</param>
    public void ViewDialog(DialogParameter parameter)
    {
        if (parameter is not CommonDialogParameter)
        {
            Debug.LogError("�p�����[�^�[���s�K�؂ł�");
            return;
        }

        CommonDialogParameter param = parameter as CommonDialogParameter;
        titleText.text = param.title;
        messageText.text = param.message;

        // �p�����[�^�[���w�肳��ĂȂ��ꍇ��OK�{�^���̂ݔz�u
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

    // �{�^���𐶐�
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
                button = Instantiate(okButton, buttonParent); // ����OK�{�^����u���Ă�
                break;
        }
        button.GetComponentInChildren<Text>().text = parameter.ButtonText;

        if(parameter.onAction != null)
        {
            button.onClick.AddListener(new UnityEngine.Events.UnityAction(parameter.onAction));

        }
        // �C�x���g�������s������_�C�A���O�����
        button.onClick.AddListener(OnClickClose);
    }

    private void OnClickClose()
    {
        OnClickCloseButton?.Invoke(this);

    }

    public void CloseDialog()
    {
        // �_�C�A���O�����ۂ̌����ڏ���
        Destroy(this.gameObject);
    }

}
