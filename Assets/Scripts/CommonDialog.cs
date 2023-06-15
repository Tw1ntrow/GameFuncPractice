using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �ėp�_�C�A���O
/// ��U�^�C�g���A�{���A����{�^���̂ݔz�u
/// </summary>
public class CommonDialog : MonoBehaviour
{
    [SerializeField]
    private Text titleText;
    [SerializeField] 
    private Text messageText;

    /// <summary>
    /// �_�C�A���O��\��
    /// </summary>
    /// <param name="title">�^�C�g��</param>
    /// <param name="message">�{��</param>
    public void ViewDialog(string title, string message/*, string okButtonText, string cancelButtonText, System.Action onOk, System.Action onCancel*/)
    {
        titleText.text = title;
        messageText.text = message;
    }

    // ���A�{���͊Ǘ��N���X�ŊǗ������
    public void CloseDialog()
    {
        Destroy(gameObject);
    }
}
