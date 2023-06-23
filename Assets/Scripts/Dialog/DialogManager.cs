using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �_�C�A���O�̐����Ɣj�����s��
/// </summary>
public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance { get; private set; }

    // �O����n����MonoBehaviour�͕s�v�����A��U�����
    [SerializeField]
    private Transform dialogParent;
    // �_�C�A���O�̃v���n�u�A�{���͊O����_�E�����[�h����
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

    // �e�X�g�p
    private void Start()
    {
        CreateDialog<WebViewDialog>(new WebViewDialogParameter("https://www.google.co.jp",new Vector2(1000,600)));
    }

    /// <summary>
    /// �_�C�A���O�𐶐�����
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="parameter"></param>
    public void CreateDialog<T>(DialogParameter parameter) where T : IDialog
    {
        // �{���̓v���n�u�̃N���X������ʂ̃_�C�A���O���_�E�����[�h����
        T dialogGO = Instantiate(webViewPrefab, dialogParent).GetComponent<T>();
        dialogGO.ViewDialog(parameter);
        dialogGO.OnClickCloseButton += CloseDialog;
        dialogList.Add(dialogGO);
    }

    /// <summary>
    /// �_�C�A���O�����
    /// �_�C�A���O���ŕ���{�^���������ꂽ�ꍇ�A�R�[���o�b�N��ʂ��Ă��̃C�x���g�����s�����
    /// </summary>
    /// <param name="dialog"></param>
    public void CloseDialog(IDialog dialog) 
    {
        dialog.CloseDialog();
        // ���X�g��������̃_�C�A���O���폜
        dialogList.Remove(dialog);
    }
}