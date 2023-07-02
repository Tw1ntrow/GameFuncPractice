using System;
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

    /// <summary>
    /// �_�C�A���O�𐶐�����
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="parameter"></param>
    public void CreateDialog<T>(DialogParameter parameter) where T : IDialog
    {
        GameObject prefab;

        // �{���͖��O��v�ŃT�[�o�[����_�E�����[�h����z��
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